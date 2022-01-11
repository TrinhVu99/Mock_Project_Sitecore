using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class HttpRequestExtension
    {
        public static NameValueCollection GetFormData(this Page handler)
        {
          return handler?.Request?.Form ?? new NameValueCollection();
        }
    }
}