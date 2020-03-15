using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Services
{
    //
    // Summary:
    //      This class is an implement of IProductService, and it contains business logic related to product in details
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly ISortManager<Product> _sortManager;
        public ProductService(IConfiguration configuration, ISortManager<Product> sortManager)
        {
            _configuration = configuration;
            _sortManager = sortManager;
        }
        //
        // Summary:
        //     Get all the products
        //
        // Parameters:
        //   sortOption:
        //     The product list will sort according to the sortOption
        //
        // Returns:
        //     The product list ordered by the sortOption
        public List<Product> GetAll(string sortOption)
        {
            // Prepare the request
            var client = new RestClient(_configuration["Resource:ProudctAPI"]);
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
                throw new Exception("Call product API error");
            }
            var products = JsonConvert.DeserializeObject<List<Product>>(response.Content);

            // Sort the product list
            _sortManager.ApplySort(products, sortOption);

            return products;
        }
    }
}
