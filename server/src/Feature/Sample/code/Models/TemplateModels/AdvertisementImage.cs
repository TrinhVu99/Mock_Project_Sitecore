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
    [SitecoreType(TemplateId = Templates.AdvertisementImage.TemplateId)]
    public class AdvertisementImage : GlassBase<Item>
    {
        [SitecoreField(Templates.AdvertisementImage.Fields.Image)]
        public virtual Glass.Mapper.Sc.Fields.Image Image { get; set; }
        [SitecoreField(Templates.AdvertisementImage.Fields.Link)]
        public virtual Glass.Mapper.Sc.Fields.Link Link { get; set; }
    }
}