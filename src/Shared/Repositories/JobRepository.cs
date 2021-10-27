using System.Collections.Generic;
using System.Data;
using Shared.Models;
using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IDbConnection _conn;
        public JobRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public async Task<List<Job>> Get()
        {
            var sql = "SELECT * FROM job";
            var jobs = (await _conn.QueryAsync<Job>(sql)).ToList();
            return jobs;
        }

        public Task<Job> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Insert(Job job)
        {
            var sql = @"
INSERT INTO job
  (provider_id, provider_job_id, company_id, title, salary, salary_min, salary_max, location, description)
VALUES
  (@providerid, @providerjobid, @companyid, @title, @salary, @salarymin, @salarymax, @location, @description)";

            await _conn.ExecuteAsync(sql, job);
        }

        public Task Insert(IEnumerable<Job> jobs)
        {
            throw new NotImplementedException();
        }
    }
}
