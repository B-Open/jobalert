using System.Collections.Generic;
using System.Data;
using Shared.Models;
using Dapper;
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

        public async Task<List<Job>> GetAsync()
        {
            var sql = "SELECT id, title FROM job";
            var jobs = (await _conn.QueryAsync<Job>(sql)).ToList();
            return jobs;
        }

        public Task<Job> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(List<Job> jobs)
        {
            throw new System.NotImplementedException();
        }
    }
}