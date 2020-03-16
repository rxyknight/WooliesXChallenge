using System.Collections.Generic;
using WooliesXChallenge.Models;

namespace WooliesXChallenge.Services.ProductComparer
{
    //
    // Summary:
    //      "Descending" - Z - A sort on the Name
    public class ProductNameDescendingComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return y.Name.CompareTo(x.Name);
        }
    }
}
