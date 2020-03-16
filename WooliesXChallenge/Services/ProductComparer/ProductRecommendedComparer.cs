using System.Collections.Generic;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Services.ProductComparer
{
    //
    // Summary:
    //      "Recommended" - this will call the "shopperHistory" resource 
    //      to get a list of customers orders and needs to return based on popularity
    public class ProductRecommendedComparer : IComparer<Product>
    {
        private readonly IPopularityService _popularityService;
        private Dictionary<string, decimal> _cache = new Dictionary<string, decimal>();

        public ProductRecommendedComparer(IPopularityService popularityService)
        {
            _popularityService = popularityService;
        }

        public int Compare(Product x, Product y)
        {
            if (!_cache.ContainsKey(x.Name))
            {
                _cache.Add(x.Name, _popularityService.GetPopularityValueByName(x.Name));
            }
            if (!_cache.ContainsKey(y.Name))
            {
                _cache.Add(y.Name, _popularityService.GetPopularityValueByName(y.Name));
            }
            return _cache[y.Name].CompareTo(_cache[x.Name]);
        }
    }
}
