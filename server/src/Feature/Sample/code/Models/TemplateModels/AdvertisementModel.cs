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
    public class AdvertisementModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Advertisement.Fields.Time)]
        public virtual string Time { get; set; }
        [SitecoreField(Templates.Advertisement.Fields.Items)]
        public virtual IEnumerable<AdvertisementImage> Items { get; set; }
    }
}