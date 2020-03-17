using System;
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
        private readonly Dictionary<string, Comparison<Product>> _plainSorterDict;
        private readonly Dictionary<string, ISort<Product>> _complexSorterDict;

        public ProductSortManager()
        {
            _plainSorterDict = new Dictionary<string, Comparison<Product>>();
            _complexSorterDict = new Dictionary<string, ISort<Product>>();
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
            if(!string.IsNullOrEmpty(option))
            {
                if (_plainSorterDict.ContainsKey(sortOption))
                {
                    products.Sort(_plainSorterDict[sortOption]);
                    return;
                }
                else if(_complexSorterDict.ContainsKey(sortOption))
                {
                    products.Sort(_complexSorterDict[sortOption].CreateComparer().Compare);
                }
            }
        }

        public void RegisterComplexSorter(string option, ISort<Product> sorter)
        {
            if (_plainSorterDict.ContainsKey(option))
            {
                throw new Exception($"[{option}] has already exist in _plainSorterDict");
            }
            if (!_complexSorterDict.ContainsKey(option))
            {
                _complexSorterDict.Add(option, sorter);
            }
            else
            {
                _complexSorterDict[option] = sorter;
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
        public void RegisterPlainSorter(string option, Comparison<Product> sorter)
        {
            if (_complexSorterDict.ContainsKey(option))
            {
                throw new Exception($"[{option}] has already exist in _complexSorterDict");
            }
            if (!_plainSorterDict.ContainsKey(option))
            {
                _plainSorterDict.Add(option, sorter);
            }
            else
            {
                _plainSorterDict[option] = sorter;
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
            if (_plainSorterDict.ContainsKey(option))
            {
                _plainSorterDict.Remove(option);
            }
            else
            {
                _complexSorterDict.Remove(option);
            }
        }
    }
}
