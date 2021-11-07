using Shared.Models;
using Shared.Enums;
using System;
using System.Web;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shared.Services.Scrapers
{
    public class JobcenterScraperService : IScraperService
    {

        public string Keyword { get; set; }
        public string ProviderName { get { return "jobcenter"; }}

        private string _jobcenterUrl = "https://jobcentrebrunei.gov.bn";

        public async Task<List<Job>> Scrape()
        {
            var htmlDoc = new HtmlDocument();
            string content = await this.fetchWebsite();
            htmlDoc.LoadHtml(content);

            var jobPostings = htmlDoc.QuerySelectorAll(".list-group-item.list-group-item-flex");

            List<Job> scrapedJobs = new List<Job>();

            IDictionary<string, SalaryType> salaryTypes = new Dictionary<string, SalaryType>();

            salaryTypes["monthly"] = SalaryType.Monthly;
            salaryTypes["daily"] = SalaryType.Daily;

            foreach (var jobPosting in jobPostings)
            {
                // TODO: queue jobs and run in parallel
                var job = await getJob(jobPosting, salaryTypes);
                scrapedJobs.Add(job);
            }

            return scrapedJobs;
        }

        private async Task<Job> getJob(HtmlNode jobPosting, IDictionary<string, SalaryType> salaryTypes)
        {
            // get job name
            var name = jobPosting.QuerySelector("h4").InnerText;
            var companyName = jobPosting.QuerySelector("p a").InnerText; // TODO: change to use company object

            var lis = jobPosting.QuerySelectorAll("ul li");

            // get salary
            var salary = lis[0].InnerText;
            // TODO: change to regex
            var salaryArray = salary.Split(' ');
            var salaryType = salaryArray[2]; // get the last array value
            var salaryRangeArray = (salaryArray[1]).Split("-"); // get the second last array value
            var salaryMin = salaryRangeArray[0];
            var salaryMax = salaryRangeArray[1];

            // get location
            var locationLi = lis[1];
            var location = HttpUtility.HtmlDecode(locationLi.InnerText);

            // get job url
            var jobUrl = jobPosting.QuerySelector(".jp_job_post_right_cont a").Attributes["href"].Value;
            jobUrl = $"{this._jobcenterUrl}{jobUrl}";

            // need to go to each of the job and scraped its content
            var jobDescription = await this.scrapeJobDescription(jobUrl);

            // TODO: need some logic to handle adding company
            Job job = new Job
            {
                Title = name,
                Salary = salary,
                SalaryType = salaryTypes[salaryType.ToLower()],
                SalaryMin = Utils.ConvertKToThousand(salaryMin),
                SalaryMax = Utils.ConvertKToThousand(salaryMax),
                Location = location,
                Description = jobDescription,
                ProviderJobId = "",
            };

            return job;
        }


        private async Task<string> fetchWebsite(string url = null)
        {
            // TODO: add retry logic
            using var httpClient = new HttpClient();

            if (String.IsNullOrWhiteSpace(url))
            {
                url = $"{this._jobcenterUrl}/web/guest/search-job";

                if (!String.IsNullOrWhiteSpace(this.Keyword))
                {
                    url = $"{url}?q={this.Keyword}";
                }
            }


            HttpResponseMessage response = await httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string htmlContent = await response.Content.ReadAsStringAsync();

            return htmlContent;
        }

        private async Task<string> scrapeJobDescription(string jobUrl)
        {
            var jobPosting = new HtmlDocument();
            string content = await this.fetchWebsite(jobUrl);
            jobPosting.LoadHtml(content);

            var jobDescriptionNode = jobPosting.QuerySelector(".job-viewer-wrapper-content");
            jobDescriptionNode.QuerySelector(".job-title").Remove();
            jobDescriptionNode.QuerySelector(".jp_job_res .other-details").Remove();

            // remove all the unnecessary classes and styles
            foreach (var eachNode in jobDescriptionNode.Descendants().Where(x => x.NodeType == HtmlNodeType.Element))
            {
                eachNode.Attributes.RemoveAll();
            }

            string jobDescription = HttpUtility.HtmlDecode(jobDescriptionNode.InnerHtml);

            // we can do above or
            // string jobDescription = jobDescriptionNode.InnerText;

            return jobDescription;
        }
    }
}
