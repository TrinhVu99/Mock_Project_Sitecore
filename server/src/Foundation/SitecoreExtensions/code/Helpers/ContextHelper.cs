using Sitecore;
using Sitecore.Data;
using Sitecore.Globalization;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockProject.Foundation.SitecoreExtensions.Helpers
{
    public static class ContextHelper
    {
        
        public static Language Language
        {
            get { return Context.Site.EnablePreview ? Context.Language : Context.ContentLanguage; }
        }

        public static Database Database
        {
            get { return Context.Site.EnablePreview ? Context.Database : Context.ContentDatabase; }
        }
        public static string GetEditFrameButtonFolder(string shortPath, string siteName)
        {
            if (string.IsNullOrWhiteSpace(siteName))
            {
                siteName = Context.Site.Name;
            }
            return Settings.CustomEditFrameButtonFolderPath + siteName + "/" + shortPath;
        }
        public static string GetEditFrameButtonFolder(string shortPath)
        {
            return GetEditFrameButtonFolder(shortPath, "MockProject");
        }
   
    }
}
