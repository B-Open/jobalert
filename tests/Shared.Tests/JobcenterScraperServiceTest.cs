using Xunit;
using Shared.Services.Scrapers;
using System;

namespace Shared.Tests
{
    public class JobcenterScraperServiceTest
    {
        [Theory]
        [InlineData("https://jobcentrebrunei.gov.bn/web/guest/view-job/-/jobs/9136763/despatcher--kb-office-", "9136763")]
        [InlineData("https://jobcentrebrunei.gov.bn/web/guest/view-job/-/jobs/9290481/it-programmer", "9290481")]
        public void TestGetProviderJobIdFromUrl(string url, string expected)
        {
            var result = JobcenterScraperService.GetProviderJobIdFromUrl(url);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("https://example.com/path/to/job/123/test")]
        public void TestGetProviderJobIdFromUrlThrowsArgumentException(string url)
        {
            Assert.Throws<ArgumentException>(() => JobcenterScraperService.GetProviderJobIdFromUrl(url));
        }

        [Theory]
        [InlineData("https://jobcentrebrunei.gov.bn/web/guest/view-employer/-/employer/4517018", "4517018")]
        [InlineData("https://jobcentrebrunei.gov.bn/web/guest/view-employer/-/employer/736512", "736512")]
        public void TestGetProviderCompanyIdFromUrl(string url, string expected)
        {
            var result = JobcenterScraperService.GetProviderCompanyIdFromUrl(url);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("https://example.com/path/to/job/123/test")]
        public void TestGetProviderCompanyIdFromUrlThrowsArgumentException(string url)
        {
            Assert.Throws<ArgumentException>(() => JobcenterScraperService.GetProviderCompanyIdFromUrl(url));
        }
    }
}
