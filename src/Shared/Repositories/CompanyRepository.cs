using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shared.Models;

namespace Shared.Repositories {
	public class CompanyRepository : ICompanyRepository
	{
        private readonly IDbConnection _conn;
        public CompanyRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public async Task<Company> Get(int id)
        {
            var sql = "SELECT * FROM company WHERE id = ?";
            var company = (await _conn.QueryFirstOrDefaultAsync<Company>(sql, id));
            return company;
        }

        public async Task Insert(Company company)
        {
            var sql = @"
INSERT INTO company
  (name, provider_company_id)
VALUES
  (@name, @providercompanyid)";

            await _conn.ExecuteAsync(sql, company);
        }

        public Task Insert(IEnumerable<Company> company)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Company>> Get()
        {
            var sql = "SELECT * FROM company";
            var companies = (await _conn.QueryAsync<Company>(sql)).ToList();
            return companies;
        }
    }
}
