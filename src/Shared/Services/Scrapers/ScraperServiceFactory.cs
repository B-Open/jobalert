using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Services.Scrapers
{
    public class ScraperServiceFactory
    {

        public string ScraperProvider { get; set; }

        public IScraperService Build()
        {
            if (this.ScraperProvider.ToLower() == "jobcenter") {
                return new JobcenterScraperService();
            }

            throw new ArgumentException($"Invalid Scraper Provider: {this.ScraperProvider}");
        }
    }
}