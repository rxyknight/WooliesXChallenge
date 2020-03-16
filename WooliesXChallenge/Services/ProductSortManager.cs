using System.Collections.Generic;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;
using WooliesXChallenge.Services.ProductComparer;

namespace WooliesXChallenge.Services
{
    //
    // Summary:
    //      This class is an implement of ISortManager, and is used to deal with any product sorting business logic
    //
    public class ProductSortManager : ISortManager<Product>
    {
        private readonly Dictionary<string, IComparerFactory<Product>> _sorterDict;

        public ProductSortManager()
        {
            _sorterDict = new Dictionary<string, IComparerFactory<Product>>();
        }
        //
        // Summary:
        //     Sort the product in the list
        //
        // Parameters:
        //   products: the product list
        //   
        //   option:
        //     The product in the list will sort according to the option
        //
        // Returns:
        //     The product list ordered by the option
        public void ApplySort(List<Product> products, string option)
        {
            var sortOption = option.ToLower();
            if(!string.IsNullOrEmpty(option) && _sorterDict.ContainsKey(option.ToLower()))
            {
                products.Sort(_sorterDict[sortOption].Create().Compare);
            }
        }
        //
        // Summary:
        //     Register the sorting rule
        //
        // Parameters:
        //   option:
        //     The name of sorting rule
        //
        //   rule: 
        //     The sorting rule, it must be an implement of IComparer<Product>
        public void RegisterSortRule(string option, IComparerFactory<Product> sorter)
        {
            if (!_sorterDict.ContainsKey(option))
            {
                _sorterDict.Add(option, sorter);
            }
            else
            {
                _sorterDict[option] = sorter;
            }
        }
        //
        // Summary:
        //     Unregister the sorting rule
        //
        // Parameters:
        //   option:
        //     The name of sorting rule to remove
        public void UnregisterSortRule(string option)
        {
            _sorterDict.Remove(option);
        }
    }
}
