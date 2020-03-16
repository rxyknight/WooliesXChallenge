using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WooliesXChallenge.Cache;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services;
using WooliesXChallenge.Services.ProductComparer;
using Xunit;

namespace WooliesXChallengeTest
{
    public class ProductSortManagerTest
    {
        private readonly IConfiguration _configuration;
        public ProductSortManagerTest()
        {
            _configuration = new ConfigurationBuilder()
                                .AddJsonFile("test-config.json")
                                .Build();
        }
        [Theory]
        [MemberData(nameof(Data))]
        public void ApplySortByLowTest(List<Product> products)
        {
            var productSortManager = new ProductSortManager();
            productSortManager.RegisterSortRule("low", new ProductPriceLowComparerFactory());

            productSortManager.ApplySort(products, "Low");
            Assert.Equal(5m, products[0].Price);
            Assert.Equal(10.99m, products[1].Price);
            Assert.Equal(99.99m, products[2].Price);
            Assert.Equal(101.99m, products[3].Price);
            Assert.Equal(999999999999m, products[4].Price);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void ApplySortByHighTest(List<Product> products)
        {
            var productSortManager = new ProductSortManager();
            productSortManager.RegisterSortRule("high", new ProductPriceHighComparerFactory());

            productSortManager.ApplySort(products, "High");
            Assert.Equal(999999999999m, products[0].Price);
            Assert.Equal(101.99m, products[1].Price);
            Assert.Equal(99.99m, products[2].Price);
            Assert.Equal(10.99m, products[3].Price);
            Assert.Equal(5m, products[4].Price);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void ApplySortByAscendingTest(List<Product> products)
        {
            var productSortManager = new ProductSortManager();
            productSortManager.RegisterSortRule("ascending", new ProductNameAscendingComparerFactory());

            productSortManager.ApplySort(products, "Ascending");
            Assert.Equal("Test Product A", products[0].Name);
            Assert.Equal("Test Product B", products[1].Name);
            Assert.Equal("Test Product C", products[2].Name);
            Assert.Equal("Test Product D", products[3].Name);
            Assert.Equal("Test Product F", products[4].Name);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void ApplySortByDescendingTest(List<Product> products)
        {
            var productSortManager = new ProductSortManager();
            productSortManager.RegisterSortRule("descending", new ProductNameDescendingComparerFactory());

            productSortManager.ApplySort(products, "Descending");
            Assert.Equal("Test Product F", products[0].Name);
            Assert.Equal("Test Product D", products[1].Name);
            Assert.Equal("Test Product C", products[2].Name);
            Assert.Equal("Test Product B", products[3].Name);
            Assert.Equal("Test Product A", products[4].Name);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void ApplySortByRecommendedTest(List<Product> products)
        {
            var cache = new ProductPopularityCache();
            cache.Refresh(new Dictionary<string, decimal>()
            {
                {"Test Product A", 1.0m },
                {"Test Product B", 2.0m },
                {"Test Product C", 3.0m },
                {"Test Product D", 4.0m },
                {"Test Product F", 5.0m },
            });
            var popularityService = new ProductPopularityService(_configuration, cache);
            var productSortManager = new ProductSortManager();
            productSortManager.RegisterSortRule("recommended", new ProductRecommendedComparerFactory(popularityService));

            productSortManager.ApplySort(products, "Recommended");
            Assert.Equal("Test Product F", products[0].Name);
            Assert.Equal("Test Product D", products[1].Name);
            Assert.Equal("Test Product C", products[2].Name);
            Assert.Equal("Test Product B", products[3].Name);
            Assert.Equal("Test Product A", products[4].Name);
        }

        public static IEnumerable<object[]> Data =>
           new List<object[]>
           {
                new object[] 
                {
                    new List<Product>()
                    {
                        new Product{ Name = "Test Product A", Price = 99.99m, Quantity = 0 },
                        new Product{ Name = "Test Product B", Price = 101.99m, Quantity = 0 },
                        new Product{ Name = "Test Product C", Price = 10.99m, Quantity = 0 },
                        new Product{ Name = "Test Product D", Price = 5m, Quantity = 0 },
                        new Product{ Name = "Test Product F", Price = 999999999999m, Quantity = 0 },
                    }
                },
           };
    }
}
