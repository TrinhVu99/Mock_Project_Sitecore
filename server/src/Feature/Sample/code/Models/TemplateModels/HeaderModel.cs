using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using MockProject.Foundation.GlassMapper;
using MockProject.Feature.Sample.Constants;
namespace MockProject.Feature.Sample.Models.TemplateModels
{
    [SitecoreType(TemplateId = Templates.Header.TemplateId)]
    public class HeaderModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Header.Fields.Logo)]
        public virtual Glass.Mapper.Sc.Fields.Image Logo { get; set; }
        
        [SitecoreField(Templates.Header.Fields.Weather)]
        public virtual DateTime Weather { get; set; }

        [SitecoreField(Templates.Header.Fields.WebName)]
        public virtual string WebName { get; set; }

        [SitecoreField(Templates.Header.Fields.MainNavigation)]
        public virtual IEnumerable<CategoryModel> MainNavigation { get; set; }
    }
}