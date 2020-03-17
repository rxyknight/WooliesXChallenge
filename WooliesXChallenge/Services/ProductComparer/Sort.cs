using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Services.ProductComparer
{
    public interface ISort<T>
    {
        IComparer<T> CreateComparer();
    }

    public class ProductRecommendedSort : ISort<Product>
    {
        private readonly IPopularityService _popularityService;
        public ProductRecommendedSort(IPopularityService popularityService)
        {
            _popularityService = popularityService;
        }
        public IComparer<Product> CreateComparer()
        {
            return new ProductRecommendedComparer(_popularityService);
        }
    }

}
