using Shared;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Worker.Services;
using Newtonsoft.Json;

namespace Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            JobcenterScraperService scraper = new JobcenterScraperService();

            List<Job> jobs = await scraper.scrape();

            // Just to check the scraped job 
            foreach (var job in jobs)
            {
                Console.WriteLine(JsonConvert.SerializeObject(job));
            }
        }
    }
}
