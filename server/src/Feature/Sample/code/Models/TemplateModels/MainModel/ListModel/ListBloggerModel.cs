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
    [SitecoreType(TemplateId = Templates.ListBlogger.TemplateId)]
    public class ListBloggerModel : GlassBase<Item>
    {
        [SitecoreField(Templates.ListBlogger.Fields.Name)]
        public virtual string Name { get; set; }
        [SitecoreField(Templates.ListBlogger.Fields.Bloggers)]
        public virtual IEnumerable<BloggerModel> Bloggers { get; set; }
    }
}