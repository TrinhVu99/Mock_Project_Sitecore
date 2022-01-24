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
    [SitecoreType(TemplateId = Templates.Advertisement.TemplateId)]
    public class SlidebarModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Slidebar.Fields.Images)]
        public virtual IEnumerable<ImageBaseModel> Images { get; set; }
    }
}