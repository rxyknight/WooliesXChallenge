using Microsoft.Extensions.Configuration;
using WooliesXChallenge.Cache;
using WooliesXChallenge.Services;
using Xunit;

namespace WooliesXChallengeTest
{
    public class ProductPopularityServiceTest
    {
        private readonly IConfiguration _configuration;
        public ProductPopularityServiceTest()
        {
            _configuration = new ConfigurationBuilder()
                                .AddJsonFile("test-config.json")
                                .Build();
        }

        [Fact]
        public void GetPolularityTable()
        {
            var popularityService = new ProductPopularityService(_configuration, new ProductPopularityCache());
            var table = popularityService.GetPolularityTable();
            Assert.NotNull(table);
            Assert.NotEmpty(table);
        }


    }
}
