using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockProject.Foundation.SitecoreExtensions.Helpers
{
    public static class ExceptionHelper
    {
        public static string Details(this Exception ex)
        {
            var msg = new List<string>();

            do
            {
                msg.Add(ex.Message);
                msg.Add(ex.StackTrace);
                msg.Add(string.Empty);

                ex = ex.InnerException;
            }
            while (ex != null);

            return string.Join(Environment.NewLine, msg);
        }
    }
}
