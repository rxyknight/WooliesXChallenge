using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using WooliesXChallenge.Cache;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Services
{
    //
    // Summary:
    //      This class is an implement of IPopularityService,
    //      is used to deal with any business logic related to product popularity
    public class ProductPopularityService : IPopularityService
    {

        private readonly IConfiguration _configuration;
        private readonly IProductPopularityCache _popularityCache;

        public ProductPopularityService(IConfiguration configuration, IProductPopularityCache popularityCache)
        {
            _configuration = configuration;
            _popularityCache = popularityCache;
        }


        public decimal GetPopularityValueByName(string name)
        {
            return _popularityCache.Get(name);
        }
        //
        // Summary:
        //     Get product popularity table
        //
        // Returns:
        //     A dictionary the key is product name, and the value is its weight representing
        //     its popularity, the product popularity comes from the shopper history, and the 
        //     total sales quantity is the weight.
        public IDictionary<string, decimal> GetPolularityTable()
        {
            // Prepare the request
            var client = new RestClient(_configuration["Resource:ShopperHistoryAPI"]);
            var request = new RestRequest(Method.GET);
            request.AddQueryParameter("token", _configuration["Token"]);
            IRestResponse response;
            try
            {
                // Call the API
                response = client.Execute(request);
            }
            catch (Exception)
            {
                throw new Exception("Call shopper history API error");
            }
            
            var shopperHistory = JsonConvert.DeserializeObject<List<ShopperHistoryItem>>(response.Content);
            // Generate the popularity table according to the shopper history
            var popularityTable = new Dictionary<string, decimal>();
            foreach(var sh in shopperHistory)
            {
                foreach(var pro in sh.Products)
                {
                    if (!popularityTable.ContainsKey(pro.Name))
                    {
                        popularityTable.Add(pro.Name, pro.Quantity);
                    }
                    else
                    {
                        popularityTable[pro.Name] += pro.Quantity;
                    }
                }
            }
            return popularityTable;
        }
    }
}
