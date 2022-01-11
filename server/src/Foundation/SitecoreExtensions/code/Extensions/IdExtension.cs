using Sitecore.Data;
using System.Collections.Generic;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class IdExtension
    {
        public static string ToShortStringID(this string idStr)
        {
            idStr = idStr != null ? idStr.ToLower().Replace("{", "")
                            .Replace("}", "").Replace("-", "") : idStr;
            return idStr;
        }
        public static string ToShortStringID(this ID id)
        {
            return id.ToString().ToShortStringID();
        }
        public static string[] ToShortStringIDs(this string[] idsStr)
        {
            var result = new List<string>();
            if (idsStr != null && idsStr.Length != 0)
            {
                foreach (var item in idsStr)
                {
                    var id = item.ToShortStringID();
                    result.Add(id);
                }
            }
            return result.ToArray();
        }

        public static bool IsIDNullOrEmpty(this string id)
        {
            return string.IsNullOrEmpty(id) || id.Equals("{00000000-0000-0000-0000-000000000000}");
        }
    }
}
