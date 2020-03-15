using Microsoft.Extensions.Configuration;
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
            var popularityService = new ProductPopularityService(_configuration);
            var popularityTable = popularityService.GetPolularityTable();
            Assert.NotEmpty(popularityTable);
        }


    }
}
