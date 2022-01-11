using MockProject.Foundation.SitecoreExtensions.Extensions;
using Sitecore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockProject.Foundation.SitecoreExtensions
{
    public static class Settings
    {
        public static string CustomEditFrameButtonFolderPath => Context.Site.GetSettingValue();
       // public static string CustomEditFrameButtonFolderPath = "/sitecore/content/Applications/WebEdit/Custom Edit Frame Buttons/";
    }
}
