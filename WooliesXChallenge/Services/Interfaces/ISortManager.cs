using System.Collections.Generic;

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
        //     Register the sorting rule
        //
        // Parameters:
        //   option:
        //     The name of sorting rule
        //
        //   rule: 
        //     The sorting rule, it must be an implement of IComparer<T>
        void RegisterSortRule(string option, IComparer<T> sortRule);
        //
        // Summary:
        //     Unregister the sorting rule
        //
        // Parameters:
        //   option:
        //     The name of sorting rule to remove
        void UnregisterSortRule(string option);
    }
}
