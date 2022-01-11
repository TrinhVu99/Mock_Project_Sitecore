using Sitecore.Data;
using Sitecore.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
namespace MockProject.Foundation.Mvc.Extensions
{
    public static class SitecoreHelperExtensions
    {
        public static HtmlString Field(this SitecoreHelper helper, ID fieldID)
        {
            return helper.Field(fieldID.ToString());
        }

        public static HtmlString Field(this SitecoreHelper helper, ID fieldID, object parameters)
        {
            return helper.Field(fieldID.ToString(), parameters);
        }

        public static HtmlString Field(this SitecoreHelper helper, ID fieldID, Item item)
        {
            return helper.Field(fieldID.ToString(), item);
        }

       
    }
}