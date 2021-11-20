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
                // get the company object
                var company = await _companyRepository.GetByProviderCompanyId(job.ProviderId, job.Company.ProviderCompanyId);

                // if company object is null, insert company
                if (company == null) {
                    await _companyRepository.Insert(job.Company);

                    // refetch the company
                    company = await _companyRepository.GetByProviderCompanyId(job.ProviderId, job.Company.ProviderCompanyId);
                }

                // assign the company id to the job
                job.CompanyId = company.Id;

                // check if the job exists
                if (await _jobRepository.JobExists(job.ProviderId, job.ProviderJobId, job.CompanyId)) continue;

                // insert job
                await _jobRepository.Insert(job);
            }
        }
    }
}