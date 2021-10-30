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
        private readonly IDbTransaction _trans;

        public JobRepository(IDbTransaction transaction)
        {
            _trans = transaction;
            _conn = transaction.Connection;
        }

        public async Task<List<Job>> Get()
        {
            var sql = "SELECT * FROM job";
            var jobs = (await _conn.QueryAsync<Job>(sql, null, _trans)).ToList();
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

            await _conn.ExecuteAsync(sql, job, _trans);
        }

        public Task Insert(IEnumerable<Job> jobs)
        {
            throw new NotImplementedException();
        }
    }
}
