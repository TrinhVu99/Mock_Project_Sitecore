using Sitecore;
using Sitecore.Caching;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockProject.Foundation.Caching.Cache
{
    public static class ApplicationCacheManager
    {
        private static readonly Dictionary<string, ApplicationCache> mCacheHandles = new Dictionary<string, ApplicationCache>(13);
        private static readonly object mCacheHandlesLock = new object();
        private static readonly long mDefaultCacheSize = StringUtil.ParseSizeString("200MB");
        private static string GetCacheName(string cacheName)
        {
            string lResult = string.Empty;

            if (Sitecore.Context.Site != null)
            {
                lResult = string.Format("{0}.{1}", cacheName, Sitecore.Context.Site.Name);
            }
            else
            {
                lResult = cacheName;
            }
            return lResult;
        }
        private static ApplicationCache GetCache(string cacheName)
        {
            ApplicationCache cache = null;
            if (mCacheHandles.ContainsKey(cacheName))
            {
                cache = mCacheHandles[cacheName];
            }
            if (cache == null)
            {
                lock (mCacheHandlesLock)
                {
                    try
                    {
                        cache = new ApplicationCache(cacheName, mDefaultCacheSize);                      
                        mCacheHandles.Add(cacheName, cache);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message ?? string.Empty, "ApplicationCacheManager");
                    }
                }

            }
            return cache;
        }
        public static T GetValue<T>(string cacheName,string key)
        {
            var lCacheName = GetCacheName(cacheName);
            return GetCache(lCacheName).GetCacheObject<T>(key);
        }
        public static void SetValue<T>(string cacheName,string key, T cacheObject, TimeSpan slidingExpiration)
        {
            var lCacheName = GetCacheName(cacheName);
            GetCache(lCacheName).AddCacheObject(key, cacheObject, slidingExpiration);
        }
        public static void SetValue<T>(string cacheName, string key, T cacheObject)
        {
            var lCacheName = GetCacheName(cacheName);
            GetCache(lCacheName).AddCacheObject(key, cacheObject);
        }

        public static void ClearCaches()
        {
            foreach (var lCache in mCacheHandles)
            {
                lock (mCacheHandlesLock)
                {
                    try
                    {
                        lCache.Value.Clear();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message ?? string.Empty, "ApplicationCacheManager");
                    }
                }
            }
        }
    }
}