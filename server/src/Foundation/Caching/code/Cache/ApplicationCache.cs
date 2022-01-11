
using Sitecore;
using Sitecore.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockProject.Foundation.Caching.Cache
{

    public class ApplicationCache : CustomCache
    {
        public ApplicationCache(string name, long maxSize)
            : base(name, maxSize)
        {
        }
        public void AddCacheObject<T>(string key, T cacheObject)
        {
            InnerCache.Add(key, cacheObject);
        }
        public void AddCacheObject<T>(string key, T cacheObject, TimeSpan slidingExpiration)
        {
            InnerCache.Add(key, cacheObject, slidingExpiration);
        }

        public T GetCacheObject<T>(string key)
        {
            return (T)(InnerCache.ContainsKey(key) ? InnerCache[key] : null);
        }

        public new string GetString(string key)
        {
            return base.GetString(key);
        }
        public new void SetString(string key, string value)
        {
            base.SetString(key, value);
        }
        public static class Expiration
        {
            public static readonly TimeSpan TenMinutes = new TimeSpan(0, 10, 0);
            public static readonly TimeSpan HalfHour = new TimeSpan(0, 30, 0);
            public static readonly TimeSpan OneHour = new TimeSpan(1, 0, 0);
        }
    }

}