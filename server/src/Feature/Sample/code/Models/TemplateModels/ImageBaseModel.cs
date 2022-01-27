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
    [SitecoreType(TemplateId = Templates.ImageBase.TemplateId)]
    public class ImageBaseModel : GlassBase<Item>
    {
        [SitecoreField(Templates.ImageBase.Fields.Image)]
        public virtual Glass.Mapper.Sc.Fields.Image Image { get; set; }
        [SitecoreField(Templates.ImageBase.Fields.Link)]
        public virtual Link Link { get; set; }
        [SitecoreField(Templates.ImageBase.Fields.Alt)]
        public virtual string Alt { get; set; }

        [SitecoreField(Templates.ImageBase.Fields.LinkYoutube)]
        public virtual string LinkYoutube { get; set; }
    }
}