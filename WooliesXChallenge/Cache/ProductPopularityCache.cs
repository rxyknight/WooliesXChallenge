using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXChallenge.Cache
{
    //
    // Summary:
    //      This is a cache layer's interface, the popularity data can be stored into the cache 
    //      to improve the query performance.
    public interface IProductPopularityCache
    {
        //
        // Summary:
        //     Get popularity by name
        //
        // Parameters:
        //   key:
        //     The product's name
        //
        // Returns:
        //     The product's popularity
        decimal Get(string key);
        //
        // Summary:
        //     Refresh the cache
        //
        // Parameters:
        //   newData:
        //     The new cache data
        //
        void Refresh(IDictionary<string, decimal> newData);
    }

    public class ProductPopularityCache : IProductPopularityCache
    {
        private volatile ConcurrentDictionary<string, decimal> _cache = new ConcurrentDictionary<string, decimal>();
        public decimal Get(string key)
        {
            decimal value;
            if(_cache.TryGetValue(key, out value))
            {
                return value;
            }
            return -1;
        }

        public void Refresh(IDictionary<string, decimal> newData)
        {
            _cache = new ConcurrentDictionary<string, decimal>(newData);
        }
    }
}
