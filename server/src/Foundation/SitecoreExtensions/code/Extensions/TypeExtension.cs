using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Provider;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Globalization;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class TypeExtensions
    {
        public static int UnixTime(this DateTime time)
        {
            var timestamp = time.ToUniversalTime() - new DateTime(1970, 1, 1);
            return (int)timestamp.TotalSeconds;
        }

        /// <summary>
        /// Verify if current T object is different to default(T). If yes then return current object, otherwise return defaultValue parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TDefault"></typeparam>
        /// <param name="current"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetValidatedValue<T, TDefault>(this T current, TDefault defaultValue) where TDefault : T
        {
            if (EqualityComparer<T>.Default.Equals(current, default(T)) || current as string == string.Empty)
            {
                return defaultValue;
            }

            return current;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }

        public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> list)
        {
            return list == null || !list.Any();
        }

        public static string GetChangedLanguageUrl(this Uri uri, Language language)
        {
            return uri.GetChangedLanguageUrl(language.Name);
        }

        public static string GetChangedLanguageUrl(this Uri uri, string language)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["sc_lang"] = language;
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        public static T Convert<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                // Cast ConvertFromString(string text) : object to (T)
                return (T)converter.ConvertFromString(input);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }

        public static string RemoveEndSlash(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            if (input.EndsWith("/"))
            {
                input = input.Substring(0, input.Length - 1);
            }
            return input;
        }

        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static byte[] ReadFully(this Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
