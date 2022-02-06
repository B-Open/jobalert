using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<List<User>> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(User User)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(User User)
        {
            throw new System.NotImplementedException();
        }
    }
}
