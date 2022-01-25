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
    [SitecoreType(TemplateId = Templates.Blogger.TemplateId)]
    public class BloggerModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Blogger.Fields.Name)]
        public virtual string Name { get; set; }

        [SitecoreField(Templates.Blogger.Fields.Text)]
        public virtual string Text { get; set; }

        [SitecoreField(Templates.Blogger.Fields.Image)]
        public virtual Glass.Mapper.Sc.Fields.Image Image { get; set; }
    }
}