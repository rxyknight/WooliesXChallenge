using System.Collections.Generic;
using WooliesXChallenge.Models;

namespace WooliesXChallenge.Services.ProductComparer
{
    //
    // Summary:
    //      "Ascending" - A - Z sort on the Name
    public class ProductNameAscendingComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
