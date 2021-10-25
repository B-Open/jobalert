using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Repositories
{
    public interface IJobRepository
    {
        Task Insert(List<Job> jobs);
        Task<List<Job>> Get();
        Task<Job> Get(int id);
    }
}
