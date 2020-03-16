using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;
using WooliesXChallenge.Services.ProductComparer;

namespace WooliesXChallenge.Services
{
    public class ProductSortRegister
    {
        public static void RegisterAll(ISortManager<Product> sortManager, IPopularityService popularityService)
        {
            sortManager.RegisterPlainSorter("low", (x, y) =>x.Price.CompareTo(y.Price));
            sortManager.RegisterPlainSorter("high", (x, y) => y.Price.CompareTo(x.Price));
            sortManager.RegisterPlainSorter("ascending", (x, y) => x.Name.CompareTo(y.Name));
            sortManager.RegisterPlainSorter("descending", (x, y) => y.Name.CompareTo(x.Name));
            sortManager.RegisterComplexSorter("recommended", new ProductRecommendedComparerFactory(popularityService));
        }
    }
}
