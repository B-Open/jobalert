using System.Collections.Generic;
using System.Data;
using Shared.Models;
using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly IDbConnection _conn;
        private readonly IDbTransaction _trans;

        public ProviderRepository(IDbTransaction transaction)
        {
            _trans = transaction;
            _conn = transaction.Connection;
        }

        public async Task<Provider> GetByProviderName(string name)
        {
            var sql = "SELECT * FROM provider WHERE name = @name";

            return await _conn.QueryFirstAsync(sql); 
        }
    }
}
