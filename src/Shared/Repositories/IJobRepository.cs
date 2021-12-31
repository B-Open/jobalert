using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Repositories
{
    public interface IJobRepository
    {
        Task<List<Job>> Get(string search);
        Task<Job> Get(int id);
        Task Insert(Job job);
        Task Insert(IEnumerable<Job> jobs);
        Task<Job> GetByProviderJobId(string providerJobId);
        Task Update(Job job);
    }
}
