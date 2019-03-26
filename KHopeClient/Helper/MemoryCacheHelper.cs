using System;
using System.Runtime.Caching;

namespace KHopeClient
{
    public class MemoryCacheHelper
    {
        private static readonly Object _locker = new object();
        public static T GetCacheItem<T>(String key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            if (String.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Invalid cache key");

            if (cachePopulate == null)
                throw new ArgumentNullException("cachePopulate");


            if (MemoryCache.Default[key] == null)
            {
                lock (_locker)
                {
                    CacheItem item = new CacheItem(key, cachePopulate());
                    CacheItemPolicy policy = CreatePolicy(slidingExpiration, absoluteExpiration);
                    MemoryCache.Default.Add(item, policy);
                }
            }
            return (T)MemoryCache.Default[key];
        }

        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }
            policy.Priority = CacheItemPriority.Default;
            return policy;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Clear(string key)
        {
            MemoryCache.Default.Remove(key);
        }
    }
}
