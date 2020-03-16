using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Services.ProductComparer
{
    public interface IComparerFactory<T>
    {
        IComparer<T> Create();
    }

    public class ProductNameAscendingComparerFactory : IComparerFactory<Product>
    {
        public IComparer<Product> Create()
        {
            return new ProductNameAscendingComparer();
        }
    }

    public class ProductNameDescendingComparerFactory : IComparerFactory<Product>
    {
        public IComparer<Product> Create()
        {
            return new ProductNameDescendingComparer();
        }
    }

    public class ProductPriceLowComparerFactory : IComparerFactory<Product>
    {
        public IComparer<Product> Create()
        {
            return new ProductPriceLowComparer();
        }
    }

    public class ProductPriceHighComparerFactory : IComparerFactory<Product>
    {
        public IComparer<Product> Create()
        {
            return new ProductPriceHighComparer();
        }
    }

    public class ProductRecommendedComparerFactory : IComparerFactory<Product>
    {
        private readonly IPopularityService _popularityService;
        public ProductRecommendedComparerFactory(IPopularityService popularityService)
        {
            _popularityService = popularityService;
        }
        public IComparer<Product> Create()
        {
            return new ProductRecommendedComparer(_popularityService);
        }
    }

}
