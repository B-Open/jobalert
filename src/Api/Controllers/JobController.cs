using Shared.Models;
using Shared.Services.Scrapers;
using Shared.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IJobRepository _jobRepository;

        public JobController(ILogger<JobController> logger, IJobRepository jobRepository)
        {
            _logger = logger;
            _jobRepository = jobRepository;
        }

        [HttpGet("{search?}")]
        public async Task<IEnumerable<Job>> Get(string search = null)
        {
            _logger.LogInformation("jobsearch API invoked");

            ScraperServiceFactory factory = new ScraperServiceFactory();

            //TODO: prevent re-instatiating scraper object every request
            // factory.ScraperProvider = "dummy";
            factory.ScraperProvider = "jobcenter";

            IScraperService scraper = factory.Build();

            if (search != null)
            {
                _logger.LogInformation($"Searched keyword {search}");
                scraper.Keyword = search;
            }

            List<Job> scrapedJobs = await scraper.Scrape();

            return scrapedJobs;
        }

        [HttpGet("demo")]
        public async Task<IEnumerable<Job>> GetJobsAsync()
        {
            return await _jobRepository.Get();
        }
    }
}
