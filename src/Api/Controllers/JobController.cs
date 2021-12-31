using Shared.Models;
using Shared.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;

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

        [HttpGet()]
        public async Task<IEnumerable<Job>> GetJobsAsync([FromQuery] string search)
        {
            _logger.LogInformation("jobs API invoked");

            IEnumerable<Job> jobs;
            try
            {
                jobs = await _jobRepository.Get(search);
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            _transaction.Commit();

            return jobs;
        }

        [HttpGet("{id}")]
        public async Task<Job> GetJobByIdAsync(int id)
        {
            _logger.LogInformation("Job ID API invoked");

            Job job;
            try
            {
                job = await _jobRepository.Get(id);
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            _transaction.Commit();

            return job;
        }
    }
}
