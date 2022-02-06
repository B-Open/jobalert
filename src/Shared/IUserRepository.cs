using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> Get();
        Task<User> Get(int id);
        Task Insert(User User);
        Task Update(User User);
    }
}
