using Shared.Models;
using System;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Faker;

namespace Shared.Services.Scrapers
{
    public class DummyScraperService: IScraperService
    {

        public string Keyword { get; set; }


        public string GetProviderName()
        {
            return "dummy";
        }

        public async Task<List<Job>> Scrape()
        {
            List<Job> dummyJobs = new List<Job>();

            for (int i = 0; i < 20; i++)
            {
                dummyJobs.Add(new Job{
                    Id = i + 1,
                    Name = Faker.Name.FullName(),
                    CompanyName = Faker.Company.Name(),
                    Location = Faker.Address.City(),
                    Salary = $"B$ {Faker.RandomNumber.Next(1000, 2000)}",
                    JobDescription = Faker.Company.BS(),
                });
            }

            // I don't know how to wrap Task into this variable
            return dummyJobs;
        }

    

    }
}