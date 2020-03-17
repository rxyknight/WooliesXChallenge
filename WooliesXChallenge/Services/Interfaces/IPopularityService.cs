using System.Collections.Generic;

namespace WooliesXChallenge.Services.Interfaces
{
    //
    // Summary:
    //      This interface is used to deal with any business logic related to popularity
    public interface IPopularityService
    {
        //
        // Summary:
        //     Get popularity value by name
        // Parameters:
        //   name: name
        //
        // Returns:
        //     A dictionary incidates the popularity
        decimal GetPopularityValueByName(string name);

        //
        // Summary:
        //     Get all popularity
        //
        // Returns:
        //     A dictionary that the key is the name, value is the corresponding popularity 
        IDictionary<string, decimal> GetPolularityTable();
    }
}
