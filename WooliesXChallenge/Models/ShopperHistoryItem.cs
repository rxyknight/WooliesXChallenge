using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXChallenge.Models
{
    public class ShopperHistoryItem
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
    }
}
