using Shared.Models;
using Shared.Services.Scrapers;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            JobcenterScraperService scraper = new JobcenterScraperService();

            // setting the keyword
            scraper.Keyword = "programmer";

            List<Job> jobs = await scraper.Scrape();

            // Just to check the scraped job 
            foreach (var job in jobs)
            {
                Console.WriteLine(JsonConvert.SerializeObject(job));
            }
        }
    }
}
