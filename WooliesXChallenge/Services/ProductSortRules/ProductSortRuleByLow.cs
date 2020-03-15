using System.Collections.Generic;
using WooliesXChallenge.Models;

namespace WooliesXChallenge.Services.ProductSortRules
{
    //
    // Summary:
    //      "Low" - Low to High Price
    public class ProductSortRuleByLow : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }
}
