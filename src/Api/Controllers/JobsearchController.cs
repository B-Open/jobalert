using Shared;
using Shared.Models;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Repositories;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsearchController : ControllerBase
    {
        private readonly ILogger<JobsearchController> _logger;
        private readonly IJobRepository _jobRepository;

        public JobsearchController(ILogger<JobsearchController> logger, IJobRepository jobRepository)
        {
            _logger = logger;
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Job>> Get([FromQuery(Name = "search")] string search = "")
        {
            _logger.LogInformation("jobsearch API invoked");
            JobcenterScraperService scraper = new JobcenterScraperService();

            if (search != "")
            {
                _logger.LogInformation($"Searched keyword {search}");
                scraper.setKeyword(search);
            }

            List<Job> scrapedJobs = await scraper.scrape();

            return scrapedJobs;
        }

        [HttpGet("demo")]
        public async Task<IEnumerable<Job>> GetJobsAsync()
        {
            _logger.LogInformation("invoked demo");
            return await _jobRepository.GetAsync();
        }
    }
}
