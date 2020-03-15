using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;
using WooliesXChallenge.Services.ProductSortRules;

namespace WooliesXChallenge.Services
{
    public class ProductSortFactory
    {
        public static void Init(ISortManager<Product> sortManager, IPopularityService popularityService)
        {
            sortManager.RegisterSortRule("low", new ProductSortRuleByLow());
            sortManager.RegisterSortRule("high", new ProductSortRuleByHigh());
            sortManager.RegisterSortRule("ascending", new ProductSortRuleByAscending());
            sortManager.RegisterSortRule("descending", new ProductSortRuleByDescending());
            sortManager.RegisterSortRule("recommended", new ProductSortRuleByRecommended(popularityService));
        }
    }
}
