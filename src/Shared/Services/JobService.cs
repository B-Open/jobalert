using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;
using Shared.Repositories;

namespace Shared.Services
{
    public class JobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;

        public JobService(IJobRepository jobRepository, ICompanyRepository companyRepository)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// Updates the database with jobs and associated companies.
        /// </summary>
        public async Task UpdateJobs(List<Job> jobs)
        {
            if (jobs is null)
            {
                throw new ArgumentNullException(nameof(jobs));
            }

            // TODO: add logic to update if exist, insert if new
            // insert jobs
            foreach (var job in jobs)
            {
                await _jobRepository.Insert(job);
            }

            // TODO: insert companies if not exist
        }
    }
}