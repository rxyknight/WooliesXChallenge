using System;
using System.Collections.Generic;
using WooliesXChallenge.Services.ProductComparer;

namespace WooliesXChallenge.Services.Interfaces
{
    //
    // Summary:
    //      This interface is used to deal with any sorting business logic
    //
    // Type parameters:
    //   T:
    //     The type of elements for sorting.
    public interface ISortManager<T>
    {
        //
        // Summary:
        //     Sort the elements in the list
        //
        // Parameters:
        //   t: the list
        //   
        //   option:
        //     The element in the list will sort according to the option
        //
        // Returns:
        //     The element list ordered by the option
        void ApplySort(List<T> t, string option);
        //
        // Summary:
        //     Register plain sorting logic
        //
        // Parameters:
        //   option:
        //     The name of sorting logic
        //
        //   rule: 
        //     The plain sorting logic, it must be Comparison<T>
        void RegisterPlainSorter(string option, Comparison<T> sorter);
        //
        // Summary:
        //     Register complex sorting logic
        //
        // Parameters:
        //   option:
        //     The name of sorting logic
        //
        //   rule: 
        //     The complex sorting logic, it must be ISort<T>
        void RegisterComplexSorter(string option, ISort<T> sorter);
        //
        // Summary:
        //     Unregister the sorting logic
        //
        // Parameters:
        //   option:
        //     The name of sorting logic to remove
        void UnregisterSortRule(string option);
    }
}
