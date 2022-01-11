using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class TemplateExtensions
    {
        public static bool IsDerived(this Template template, ID templateId)
        {
            return template.ID == templateId ||
                   template.GetBaseTemplates().Any(baseTemplate => IsDerived(baseTemplate, templateId));
        }
    }
}
