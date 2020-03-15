using System;
using System.Collections.Generic;
using WooliesXChallenge.Models;

namespace WooliesXChallenge.Services.Interfaces
{
    //
    // Summary:
    //      This interface is used to deal with any business logic related to product
    public interface IProductService
    {
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
        List<Product> GetAll(string sortOption);
    }
}
