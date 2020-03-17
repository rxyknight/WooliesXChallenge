using Newtonsoft.Json.Linq;
using WooliesXChallenge.Models;

namespace WooliesXChallenge.Services.Interfaces
{
    //
    // Summary:
    //      This interface is used to deal with any business logic related to trolley
    public interface ITrolleyService
    {
        //
        // Summary:
        //     Calculate the lowest possible total price in the trolley
        //
        // Parameters:
        //   request:
        //     All the trolley information which includes lists of prices, specials and quantities,
        //     this is in JSON format
        //
        // Returns:
        //     The lowest possible total price
        decimal CalculateTrolleyTotal(JToken request);
        decimal CalculateTrolleyTotalLocal(Trolley trolley);
    }
}
