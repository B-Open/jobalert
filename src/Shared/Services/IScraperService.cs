using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Services
{
    public interface IScraperService
    {

        string Keyword { get; set; }

        string GetProviderName();

        Task<List<Job>> Scrape();


    }
}