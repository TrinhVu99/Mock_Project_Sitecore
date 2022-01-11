using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace MockProject.Foundation.SitecoreExtensions.Helpers
{
    public static class SitecoreItemHelper
    {
        private static readonly IList<string> SITES = new List<string>() 
        { 
            "shell", 
            "login", 
            "testing", 
            "admin", 
            "service", 
            "modules_shell", 
            "modules_website", 
            "scheduler", 
            "system", 
            "publisher" 
        };
        public static string GetItemFieldTextValue(string itemId, string fieldName)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                return string.Empty;
            }

            var itemObj = Sitecore.Context.Database.GetItem(new ID(itemId));

            if (itemObj == null)
            {
                return string.Empty;
            }

            return itemObj[fieldName];
        }

        public static string GetRenderingParameter(string parameterName)
        {
            string parameterValue = string.Empty;
            var renderingContext = Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull;
            if (renderingContext != null)
            {
                var parameters = renderingContext.Rendering.Parameters;
                parameterValue = parameters[parameterName];
            }
            return parameterValue;
        }

        public delegate bool ItemCondition(Item sitecoreItem);
        public static Item FindItemAncestor(Item targetItem, ItemCondition condition)
        {
            if (ID.IsNullOrEmpty(targetItem?.ParentID))
            {
                return null;
            }

            return condition(targetItem) ? targetItem.Parent : FindItemAncestor(targetItem.Parent, condition);

        }
        public static string FormatItemName(string name)
        {
            return MainUtil.EncodeName(name?.ToLower());
        }

        /// <summary>
        /// Resolves the configuration path based on the current site, or falls back to looking at some URL segments to determine the path.
        /// </summary>
        /// <returns>Configuration path</returns>
        public static string GetSiteStartPathWithFallback()
        {
            string lSiteName = Context.Site.Name.ToLower();

            if (SITES.Contains(lSiteName) && Context.Item != null)
            {
                var lSegments = Context.Item.Paths.FullPath.Split('/');
                var lSite = "/" + lSegments[1] + "/" + lSegments[2] + "/" + lSegments[3];
                return lSite;
            }
            return Context.Site.StartPath;
        }
    }
}
