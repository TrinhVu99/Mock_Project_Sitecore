using MockProject.Foundation.SitecoreExtensions.Helpers;
using MockProject.Foundation.SitecoreExtensions.Extensions;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.ContentSearch;
using Sitecore.Data;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Diagnostics;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class SiteExtensions
    {
        public static ISearchIndex GetSearchIndex(this string key, string siteName)
        {
            siteName = siteName ?? Context.Site.Name.ToLower();
            var index = (GetIndex(string.Format("{0}_{1}_{2}", siteName, ContextHelper.Database.Name, key)) ??
                         GetIndex(string.Format("{0}_{1}", Context.Site.Name.ToLower(), key))) ?? GetIndex(key);

            return index;
        }
        private static ISearchIndex GetIndex(string key)
        {
            return ContentSearchManager.GetIndex(key);
        }
        public static string ToSiteKey(this string key, SiteContext context)
        {
            return SetSiteKey(key, context);
        }
        public static string ToSiteKey(this string key)
        {
            return SetSiteKey(key, null);
        }
        private static string SetSiteKey(this string key, SiteContext context)
        {
            if (context != null)
            {
                return context.Name + "." + key;
            }
            return Context.Site.Name + "." + key;
        }

        public static string GetSiteName(this SiteContext context)
        {
            if (context == null)
            {
                Log.Error(string.Format("Unable to retreive SiteName for unknown reasons, Site: {0}, url{1}", Sitecore.Context.Site, HttpContext.Current.Request.Url), typeof(SiteExtensions));
                return "website_ipss";
            }
            else
            {
                return Sitecore.Context.Site.Name;
            }
        }

        public static string GetKey(this SiteContext context, string key)
        {
            return context != null ? context.Name + "." + key : key;
        }

        public static string GetSettingValue(this SiteContext context, [CallerMemberName] string key = "")
        {
            if (context == null)
            {
                return Sitecore.Configuration.Settings.GetSetting(key);
            }
            return Sitecore.Configuration.Settings.GetSetting(GetKey(context, key), null) ??
                   Sitecore.Configuration.Settings.GetSetting(key);
        }

        public static T GetSettingValue<T>(this SiteContext context, [CallerMemberName] string key = "")
        {
            var settingRawValue = context.GetSettingValue(key);
            return settingRawValue.Convert<T>();
        }

        public static ID GetSettingIDValue(this SiteContext context, [CallerMemberName] string key = "")
        {
            var settingRawValue = context.GetSettingValue(key);
            return ID.Parse(settingRawValue);
        }
        public static string GetPropertyValue(this SiteContext context, string key)
        {
            var property = context?.Properties[key];
            return property;
        }

        public static bool GetPropertyBoolValue(this SiteContext context, string key)
        {
            var property = context.GetPropertyValue(key);
            bool propertyBool;
            if (string.IsNullOrWhiteSpace(property))
            {
                return false;
            }

            return bool.TryParse(property, out propertyBool) && propertyBool;
        }

    }
}

