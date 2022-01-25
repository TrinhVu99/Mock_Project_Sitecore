using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using MockProject.Foundation.GlassMapper;
using MockProject.Feature.Sample.Constants;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;

namespace MockProject.Feature.Sample.Models.TemplateModels.Tag
{

    [SitecoreType(TemplateId = Templates.Tag.TemplateId)]
    public class TagModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Tag.Fields.Name)]
        public virtual string Name { get; set; }
    }
}