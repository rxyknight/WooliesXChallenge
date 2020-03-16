using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXChallenge.Cache
{
    public interface IProductPopularityCache
    {
        decimal Get(string key);
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
