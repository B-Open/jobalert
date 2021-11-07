using Xunit;

namespace Shared.Tests
{
    public class UtilsTest
    {
        [Theory]
        [InlineData("1k", 1000)]
        [InlineData("10k", 10000)]
        [InlineData("5k", 5000)]
        [InlineData("9k", 9000)]
        [InlineData("23121k", 23121000)]
        [InlineData("1K", 1000)]
        [InlineData("10K", 10000)]
        [InlineData("5K", 5000)]
        [InlineData("9K", 9000)]
        [InlineData("23121K", 23121000)]
        public void TestConvertKToThousand(string number, decimal expected)
        {
            var result = Utils.ConvertKToThousand(number);
            Assert.Equal(result, expected);
        }
    }
}
