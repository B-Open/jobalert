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
        public void TestGetJobProviderIdFromUrl(string url, string expected)
        {
            var result = JobcenterScraperService.GetJobProviderIdFromUrl(url);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("https://example.com/path/to/job/123/test")]
        public void TestGetJobProviderIdFromUrlThrowsArgumentException(string url)
        {
            Assert.Throws<ArgumentException>(() => JobcenterScraperService.GetJobProviderIdFromUrl(url));
        }
    }
}
