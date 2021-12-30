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

            var companyMap = new Dictionary<string, Company>();

            // insert jobs
            foreach (var job in jobs)
            {
                // get existing
                var oldJob = await _jobRepository.GetByProviderJobId(job.ProviderJobId);

                // if not exist insert
                if (oldJob != null)
                {
                    await _jobRepository.Update(job);
                }
                else
                {
                    await _jobRepository.Insert(job);
                }

                // add company id to hash map to be processed later
                if (!companyMap.ContainsKey(job.Company.ProviderCompanyId))
                {
                    companyMap.Add(job.Company.ProviderCompanyId, job.Company);
                }
            }

            // TODO: insert companies if not exist
            foreach (var company in companyMap)
            {

            }
        }
    }
}
