using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shared.Models;

namespace Shared.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbConnection _conn;
        private readonly IDbTransaction _trans;

        public CompanyRepository(IDbTransaction transaction)
        {
            _trans = transaction;
            _conn = transaction.Connection;
        }

        public async Task<Company> Get(int id)
        {
            var sql = "SELECT * FROM company WHERE id = ?";
            var company = (await _conn.QueryFirstOrDefaultAsync<Company>(sql, new { id }, _trans));
            return company;
        }

        public async Task Insert(Company company)
        {
            var sql = @"
INSERT INTO company
  (name, provider_company_id, provider_id)
VALUES
  (@name, @providercompanyid, @providerid)";

            await _conn.ExecuteAsync(sql, company, _trans);
        }

        public Task Insert(IEnumerable<Company> company)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Company>> Get()
        {
            var sql = "SELECT * FROM company";
            var companies = (await _conn.QueryAsync<Company>(sql, null, _trans)).ToList();
            return companies;
        }

        public async Task<Company> GetByProviderId(string id)
        {
            var sql = "SELECT * FROM company WHERE provider_company_id = @id";

            return await _conn.QueryFirstOrDefaultAsync<Company>(sql, new { id }, _trans);
        }

        public async Task Update(Company company)
        {
            var sql = @"
            UPDATE company SET
                name = @name,
                provider_id = @providerid,
                provider_company_id = @providercompanyid
            WHERE id = @id";

            await _conn.ExecuteAsync(sql, company, _trans);
        }
    }
}
