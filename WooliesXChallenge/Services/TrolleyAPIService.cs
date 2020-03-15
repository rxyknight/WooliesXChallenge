using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Services
{
    //
    // Summary:
    //      This class is an implement of ITrolleyService, and it contains the business logic related to trolley in details
    public class TrolleyAPIService : ITrolleyService
    {
        private readonly IConfiguration _configuration;
        public TrolleyAPIService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
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
        public decimal CalculateTrolleyTotal(JToken request)
        {
            // Prepare the request
            var client = new RestClient(_configuration["Resource:TrolleyCalculatorAPI"]);
            var res = new RestRequest(Method.POST);
            res.AddQueryParameter("token", _configuration["Token"]);
            res.AddHeader("Content-Type", "application/json");
            res.AddParameter("application/json", request.ToString(), ParameterType.RequestBody);
            IRestResponse response;
            try
            {
                // Call API
                response = client.Execute(res);
            }
            catch (Exception)
            {
                throw new Exception("Call trolleyCalculator API error");
            }
            return decimal.Parse(response.Content);
        }
    }
}
