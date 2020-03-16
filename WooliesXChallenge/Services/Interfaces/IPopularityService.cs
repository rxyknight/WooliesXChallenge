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
        //     Get popularity table by name
        //
        // Returns:
        //     The value incidates the popularity
        decimal GetPopularityValueByName(string name);
    }
}
