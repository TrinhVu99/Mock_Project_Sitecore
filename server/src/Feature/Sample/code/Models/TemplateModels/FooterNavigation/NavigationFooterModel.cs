using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using MockProject.Foundation.GlassMapper;
using MockProject.Feature.Sample.Constants;

namespace MockProject.Feature.Sample.Models.TemplateModels.FooterNavigation
{
    [SitecoreType(TemplateId = Templates.Navigation.TemplateId)]
    public class NavigationFooterModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Navigation.Fields.Name)]
        public virtual string Name { get; set; }

        [SitecoreField(Templates.Navigation.Fields.Categories)]
        public virtual IEnumerable<CategoryModel> Categories { get; set; }
    }
}