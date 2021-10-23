using Shared;
using Shared.Models;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public JobController(ILogger<JobController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{search?}")]
        public async Task<IEnumerable<Job>> Get(string search = null)
        {
            _logger.LogInformation("jobsearch API invoked");
            
            ScraperServiceFactory factory = new ScraperServiceFactory();
            
            factory.ScraperProvider = "jobcenter";

            IScraperService scraper = factory.build();

            if (search != null)
            {
                _logger.LogInformation($"Searched keyword {search}");
                scraper.Keyword = search;
            } 

            List<Job> scrapedJobs = await scraper.scrape();

            return scrapedJobs;
        }
    }
}
