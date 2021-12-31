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

        public async Task<List<Job>> Get(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                search = "";
            }

            var sql = @"
                SELECT * FROM job
                WHERE
                    (@search = '' OR MATCH(title, description) AGAINST (@search))";
            var jobs = (await _conn.QueryAsync<Job>(sql, new { search }, _trans)).ToList();
            return jobs;
        }

        public async Task<Job> Get(int id)
        {
            var sql = "SELECT * FROM job WHERE id = @id";

            return await _conn.QueryFirstAsync<Job>(sql, new { id }, _trans);
        }

        public async Task<Job> GetByProviderJobId(string id)
        {
            var sql = "SELECT * FROM job WHERE provider_job_id = @id";

            return await _conn.QueryFirstOrDefaultAsync<Job>(sql, new { id }, _trans);
        }

        public async Task Insert(Job job)
        {
            var sql = @"
            INSERT INTO job
            (provider_id, provider_job_id, company_id, title, salary, salary_min, salary_max, salary_type, location, description)
            VALUES
            (@providerid, @providerjobid, @companyid, @title, @salary, @salarymin, @salarymax, @salarytype, @location, @description)";

            await _conn.ExecuteAsync(sql, job, _trans);
        }

        public async Task Update(Job job)
        {
            var sql = @"
            UPDATE job SET
                provider_id = @providerid,
                provider_job_id = @providerjobid,
                company_id = @companyid,
                title = @title,
                salary = @salary,
                salary_min = @salarymin,
                salary_max = @salarymax,
                salary_type = @salarytype,
                location = @location,
                description = @description
            WHERE id = @id";

            await _conn.ExecuteAsync(sql, job, _trans);
        }

        public Task Insert(IEnumerable<Job> jobs)
        {
            throw new NotImplementedException();
        }
    }
}
