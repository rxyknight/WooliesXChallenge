using System.Collections.Generic;
using WooliesXChallenge.Models;

namespace WooliesXChallenge.Services.ProductSortRules
{
    //
    // Summary:
    //      "High" - High to Low Price
    public class ProductSortRuleByHigh : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return y.Price.CompareTo(x.Price);
        }
    }
}
