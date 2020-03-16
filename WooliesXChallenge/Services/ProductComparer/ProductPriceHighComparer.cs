using System;
using System.Collections.Generic;
using WooliesXChallenge.Models;

namespace WooliesXChallenge.Services.ProductComparer
{
    //
    // Summary:
    //      "High" - High to Low Price
    public class ProductPriceHighComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return y.Price.CompareTo(x.Price);
        }
    }
}
