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
        //     Calculate the lowest possible total price in the trolley, the calculation is processed remotely
        //
        // Parameters:
        //   request:
        //     All the trolley information which includes lists of prices, specials and quantities,
        //     this is in JSON format
        //
        // Returns:
        //     The lowest possible total price
        decimal CalculateTrolleyTotal(JToken request);

        //
        // Summary:
        //     Calculate the lowest possible total price in the trolley, the calculation is processed locally 
        //
        // Parameters:
        //   request:
        //     All the trolley information which includes lists of prices, specials and quantities
        //
        // Returns:
        //     The lowest possible total price
        decimal CalculateTrolleyTotalLocal(Trolley trolley);
    }
}
