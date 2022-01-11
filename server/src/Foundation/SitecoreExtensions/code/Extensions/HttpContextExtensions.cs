using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Get current site home url
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string HomeUrl(this HttpContext context)
        {
            return string.Format("{0}://{1}{2}", context.Request.Url.Scheme, context.Request.Url.Host,
                context.Request.Url.IsDefaultPort ? string.Empty : context.Request.Url.Port.ToString());
        }

        /// <summary>
        /// Add cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void AddCookies(this HttpContext context, string name, object value)
        {
            if (context.Response.Cookies[name] == null || !context.Response.Cookies[name].HasKeys)
            {
                context.Response.Cookies.Add(new HttpCookie(name, JsonConvert.SerializeObject(value)));
            }
            else
            {
                context.Response.Cookies[name].Value = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// Remove cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        public static void DeleteCookies(this HttpContext context, string name)
        {
            if (context.Response.Cookies[name] == null || !context.Response.Cookies[name].HasKeys)
            {
                context.Response.Cookies.Add(new HttpCookie(name)
                {
                    Expires = DateTime.Now.AddDays(-1)
                });
            }
            else
            {
                context.Response.Cookies[name].Expires = DateTime.Now.AddDays(-1);
            }
        }

        /// <summary>
        /// Get cookie and cast to specific type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetCookies<T>(this HttpContext context, string name)
        {
            return context.Request.Cookies[name] == null || !context.Request.Cookies[name].HasKeys
                ? default(T)
                : JsonConvert.DeserializeObject<T>(context.Response.Cookies[name].Value);
        }
    }
}
