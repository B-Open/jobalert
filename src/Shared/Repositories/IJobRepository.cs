using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Repositories
{
    public interface IJobRepository
    {
        Task<List<Job>> Get();
        Task<Job> Get(int id);
        Task<bool> JobExists(long providerId, string providerJobId, long providerCompanyId);
        Task Insert(Job job);
        Task Insert(IEnumerable<Job> jobs);
    }
}
