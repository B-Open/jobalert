using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Repositories
{
    public interface IJobRepository
    {
        void Insert(List<Job> jobs);
        Task<List<Job>> GetAsync();
        Job Get(int id);
    }
}
