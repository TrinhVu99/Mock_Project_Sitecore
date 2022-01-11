using System.Web;
using System.Web.Optimization;

namespace MockProject.Foundation.SitecoreExtensions.Helpers
{
    public class BundleHelper
    {
        public static IHtmlString RenderScript(string name)
        {
            var path = string.Format("~/bundles/script/{0}", name);
            return Scripts.Render(path);
        }

        public static IHtmlString RenderCss(string name)
        {
            var path = string.Format("~/bundles/style/{0}", name);
            return Styles.Render(path);
        }
    }
}