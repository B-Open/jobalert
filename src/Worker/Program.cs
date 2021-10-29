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
using Shared.Services;

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

            // set up database repositories
            var conn = new MySqlConnection(config.GetConnectionString("Default"));
            var jobRepository = new JobRepository(conn);
            var companyRepository = new CompanyRepository(conn);
            var jobService = new JobService(jobRepository, companyRepository);

            var scraper = new JobcenterScraperService();

            // setting the keyword  -- TODO: remove
            scraper.Keyword = "programmer";

            List<Job> jobs = await scraper.Scrape();

            // save to database
            try
            {
                await jobService.UpdateJobs(jobs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            // Just to check the scraped job 
            foreach (var job in jobs)
            {
                Console.WriteLine(JsonConvert.SerializeObject(job));
            }
        }
    }
}

