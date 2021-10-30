using Shared.Models;
using Shared.Services.Scrapers;
using Shared.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IJobRepository _jobRepository;
        private readonly IDbTransaction _transaction;

        public JobController(ILogger<JobController> logger, IDbTransaction transaction, IJobRepository jobRepository)
        {
            _logger = logger;
            _transaction = transaction;
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
            _logger.LogInformation("company API invoked");

            IEnumerable<Job> jobs;
            try
            {
                jobs = await _jobRepository.Get();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            _transaction.Commit();

            return jobs;
        }
    }
}
