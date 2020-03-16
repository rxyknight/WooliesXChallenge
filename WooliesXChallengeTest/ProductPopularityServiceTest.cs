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
            var ap = popularityService.GetPopularityValueByName("Test Product A");
            Assert.Equal(6m, ap);
        }


    }
}
