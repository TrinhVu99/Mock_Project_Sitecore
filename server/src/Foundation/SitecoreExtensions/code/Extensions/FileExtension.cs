using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class FileExtension
    {
        public static string RewriteFilePath(this string pFilePath)
        {
            const string lMatchString = "\\Media\\Filefolder\\";
            if (pFilePath == null || pFilePath.IndexOf(lMatchString) < 0)
            {
                return pFilePath;
            }
            return string.Format("{0}\\{1}", Sitecore.Configuration.Settings.Media.FileFolder,
                                 pFilePath.Substring(pFilePath.IndexOf(lMatchString) + lMatchString.Length));
        }
    }
}