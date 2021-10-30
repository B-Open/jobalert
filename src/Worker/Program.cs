using Shared.Models;
using Shared.Services.Scrapers;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Shared.Repositories;
using Microsoft.Extensions.Configuration;

namespace Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            IConfiguration config = builder.Build();

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            var conn = new MySqlConnection(config.GetConnectionString("Default"));
            var jobRepository = new JobRepository(conn);

            var scraper = new JobcenterScraperService();

            // setting the keyword
            scraper.Keyword = "programmer";

            List<Job> jobs = await scraper.Scrape();

            // Just to check the scraped job 
            foreach (var job in jobs)
            {
                Console.WriteLine(JsonConvert.SerializeObject(job));
                // TODO: add more info
                // TODO: insert in batches
                // TODO: detect duplicates
                try
                {
                    await jobRepository.Insert(job);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}

