using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Repositories
{
    public interface IProviderRepository
    {
        Task<Provider> GetByProviderName(string name);
    }
}
