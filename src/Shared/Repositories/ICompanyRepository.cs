using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<Company>> Get();
        Task<Company> Get(int id);
        Task<Company> GetByProviderId(string id);
        Task Insert(Company company);
        Task Update(Company company);
        Task Insert(IEnumerable<Company> company);
    }
}
