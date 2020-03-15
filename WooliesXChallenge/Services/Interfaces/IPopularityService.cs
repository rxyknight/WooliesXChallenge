using System.Collections.Generic;

namespace WooliesXChallenge.Services.Interfaces
{
    //
    // Summary:
    //      This interface is used to deal with any business logic related to popularity, for example, product popularity
    public interface IPopularityService
    {
        //
        // Summary:
        //     Get popularity table
        //
        // Returns:
        //     A dictionary the key is the name, for example (product name), and the value is its weight representing
        //     its popularity
        Dictionary<string, decimal> GetPolularityTable();
    }
}
