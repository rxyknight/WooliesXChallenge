using System.Collections.Generic;
using WooliesXChallenge.Models;

namespace WooliesXChallenge.Services.ProductComparer
{
    //
    // Summary:
    //      "Low" - Low to High Price
    public class ProductPriceLowComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }
}
