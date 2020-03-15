using System.Collections.Generic;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;
using WooliesXChallenge.Services.ProductSortRules;

namespace WooliesXChallenge.Services
{
    //
    // Summary:
    //      This class is an implement of ISortManager, and is used to deal with any product sorting business logic
    //
    public class ProductSortManager : ISortManager<Product>
    {
        private readonly Dictionary<string, IComparer<Product>> _sortRules;

        public ProductSortManager()
        {
            _sortRules = new Dictionary<string, IComparer<Product>>();
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
            if(!string.IsNullOrEmpty(option) && _sortRules.ContainsKey(option.ToLower()))
            {
                products.Sort(_sortRules[sortOption]);
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
        public void RegisterSortRule(string option, IComparer<Product> rule)
        {
            if (!_sortRules.ContainsKey(option))
            {
                _sortRules.Add(option, rule);
            }
            else
            {
                _sortRules[option] = rule;
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
            _sortRules.Remove(option);
        }
    }
}
