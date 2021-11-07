using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Services.Scrapers
{
    public class DummyScraperService : IScraperService
    {

        public string Keyword { get; set; }
        public string ProviderName { get { return "dummy"; }}

        public async Task<List<Job>> Scrape()
        {
            List<Job> dummyJobs = new List<Job>();

            for (int i = 0; i < 20; i++)
            {
                dummyJobs.Add(new Job
                {
                    Id = i + 1,
                    Title = Faker.Name.FullName(),
                    Location = Faker.Address.City(),
                    Salary = $"B$ {Faker.RandomNumber.Next(1000, 2000)}",
                    Description = Faker.Company.BS(),
                });
            }

            return await Task.FromResult(dummyJobs);
        }



    }
}
