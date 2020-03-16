using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Services
{
    //
    // Summary:
    //      This class is an implement of IPopularityService,
    //      is used to deal with any business logic related to product popularity
    public class ProductPopularityService : IPopularityService, IDisposable
    {

        private readonly IConfiguration _configuration;
        private readonly Timer _scheduler;
        private volatile IDictionary<string, decimal> _productPopularityTableCache;

        public ProductPopularityService(IConfiguration configuration)
        {
            _configuration = configuration;
            _productPopularityTableCache = GetPolularityTable();
            _scheduler = new Timer(FetchData, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(3600));
        }


        private void FetchData(object state)
        {
            Console.WriteLine($"Start fetching data..");
            _productPopularityTableCache = GetPolularityTable();
        }


        public decimal GetPopularityValueByName(string name)
        {
            if (_productPopularityTableCache.ContainsKey(name))
            {
                return _productPopularityTableCache[name];
            }
            return -1;
        }
        //
        // Summary:
        //     Get product popularity table
        //
        // Returns:
        //     A dictionary the key is product name, and the value is its weight representing
        //     its popularity, the product popularity comes from the shopper history, and the 
        //     total sales quantity is the weight.
        private IDictionary<string, decimal> GetPolularityTable()
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

        public void Dispose()
        {
            _scheduler?.Dispose();
        }

    }
}
