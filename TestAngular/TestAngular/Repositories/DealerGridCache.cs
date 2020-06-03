using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using TestAngular.Domain;

namespace TestAngular.Repositories
{
    public class DealerGridCache
    {
        private static MemoryCache _cache = MemoryCache.Default;

        public static void StoreItems<T>(IEnumerable<T> items, string key)
        {
            var itemPolicy = new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(60)
            };

            _cache.Add(key, items, itemPolicy);
        }

        public static List<T> RetrieveItems<T>(string key)
        {
            if (_cache.Contains(key))
                return _cache.Get(key) as List<T>;
            else
                return null;
        }
    }
}
