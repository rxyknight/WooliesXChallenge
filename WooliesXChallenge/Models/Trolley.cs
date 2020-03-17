using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXChallenge.Models
{
    public class Trolley
    {
        public List<ProductInfo> Products { get; set; }
        public List<Special> Specials { get; set; }
        public List<Item> Quantities { get; set; }
    }

    public class ProductInfo
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Special
    {
        [JsonProperty("quantities")]
        public List<Item> Items { get; set; }
        public decimal Total { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
    }

}
