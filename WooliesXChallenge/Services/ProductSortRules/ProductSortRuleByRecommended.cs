using System.Collections.Generic;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Services.ProductSortRules
{
    //
    // Summary:
    //      "Recommended" - this will call the "shopperHistory" resource 
    //      to get a list of customers orders and needs to return based on popularity
    public class ProductSortRuleByRecommended : IComparer<Product>
    {
        private IDictionary<string, decimal> _polularTable;
        private readonly IPopularityService _popularityService;

        public ProductSortRuleByRecommended(IPopularityService popularityService)
        {
            _popularityService = popularityService;
            _polularTable = _popularityService.GetPolularityTable();
        }

        public int Compare(Product x, Product y)
        {
            if (null != _polularTable)
            {
                var px = _polularTable.ContainsKey(x.Name) ? _polularTable[x.Name] : 0;
                var py = _polularTable.ContainsKey(y.Name) ? _polularTable[y.Name] : 0;
                return py.CompareTo(px);
            }
            return 0;
        }
    }
}
