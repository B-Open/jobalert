using Shared;
using System;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Worker.Services
{
    class JobcenterScraperService
    {

        public async Task<List<Job>> scrape()
        {
            var htmlDoc = new HtmlDocument();
            string content = await this.fetchWebsite();
            htmlDoc.LoadHtml(content);

            var jobPostings = htmlDoc.QuerySelectorAll(".list-group-item.list-group-item-flex");

            List<Job> scrapedJobs = new List<Job>();

            foreach (var jobPosting in jobPostings)
            {
                var name = jobPosting.QuerySelector("h4").InnerText;

                var companyName = jobPosting.QuerySelector("p a").InnerText;

                var lis = jobPosting.QuerySelectorAll("ul li");

                var salary = lis[0].InnerText;

                var location_li = lis[1];

                var location = location_li.InnerText.Replace("&nbsp; ", "");

                // need to go to each of the job and scraped its content
                var jobDescription = "";

                Job job = new Job {
                    Name = name,
                    CompanyName = companyName,
                    Salary = salary,
                    Location = location,
                    JobDescription = jobDescription,
                };

                scrapedJobs.Add(job);

            }

            return scrapedJobs;
        }


        private async Task<string> fetchWebsite()
        {
            using var httpClient = new HttpClient();
            
            string url = "https://jobcentrebrunei.gov.bn/web/guest/search-job?q=programmer";
            
            HttpResponseMessage response = await httpClient.GetAsync(url);

            var code = response.EnsureSuccessStatusCode();

            string htmlContent = await response.Content.ReadAsStringAsync(); 

            return htmlContent;
        }
    }
}