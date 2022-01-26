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
    [SitecoreType(TemplateId = Templates.Section.TemplateId)]
    public class SectionModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Section.Fields.Name)]
        public virtual string Name { get; set; }

        [SitecoreField(Templates.Section.Fields.SectionComponents)]
        public virtual IEnumerable<object> SectionComponents { get; set; }

        [SitecoreField(Templates.Section.Fields.SubName)]
        public virtual string SubName { get; set; }
    }
}