using MockProject.Foundation.SitecoreExtensions.Extensions;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using System;
using System.Linq;

namespace MockProject.Foundation.SitecoreExtensions
{
    public static class Config
    {
        /// <summary>
        /// for commands and cms: master
        /// for intranet en front end:web
        /// </summary>
        /// <returns></returns>
        public static Database GetDatabase()
        {
            string lDatabaseName = "web";
            if (Factory.GetDatabaseNames().Contains("master") && Context.Database == null || !Context.Database.Name.EqualsIgnoreCase("web")) //this is a command
            {
                lDatabaseName = "master";
            }
            return Factory.GetDatabase(lDatabaseName);
        }
    }
}
