using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;
using WooliesXChallenge.Services.ProductComparer;

namespace WooliesXChallenge.Services
{
    public class ProductSortRegister
    {
        public static void RegisterAll(ISortManager<Product> sortManager, IPopularityService popularityService)
        {
            sortManager.RegisterSortRule("low", new ProductPriceLowComparerFactory());
            sortManager.RegisterSortRule("high", new ProductPriceHighComparerFactory());
            sortManager.RegisterSortRule("ascending", new ProductNameAscendingComparerFactory());
            sortManager.RegisterSortRule("descending", new ProductNameDescendingComparerFactory());
            sortManager.RegisterSortRule("recommended", new ProductRecommendedComparerFactory(popularityService));
        }
    }
}
