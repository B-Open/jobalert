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
    public class JobsearchController : ControllerBase
    {
        private readonly ILogger<JobsearchController> _logger;

        public JobsearchController(ILogger<JobsearchController> logger)
        {
            _logger = logger;
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
    }
}
