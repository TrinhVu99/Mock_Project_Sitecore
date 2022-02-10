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

        [SitecoreField(Templates.Blogger.Fields.TitleEntity)]
        public virtual string TitleEntity { get; set; }

        [SitecoreField(Templates.Blogger.Fields.Image)]
        public virtual Glass.Mapper.Sc.Fields.Image Image { get; set; }

        [SitecoreField(Templates.Blogger.Fields.Category)]
        public virtual CategoryModel Category { get; set; }

        [SitecoreField(Templates.Blogger.Fields.Views)]
        public virtual int Views { get; set; }

        [SitecoreField(Templates.Blogger.Fields.Tags)]
        public virtual List<string> Tags { get; set; }

        [SitecoreField(Templates.Blogger.Fields.Content)]
        public virtual string Content { get; set; }

        [SitecoreField(Templates.Blogger.Fields.Likes)]
        public virtual int Likes { get; set; }

        [SitecoreField(Templates.Blogger.Fields.Author)]
        public virtual string Author { get; set; }

        [SitecoreField(Templates.Blogger.Fields.Link)]
        public virtual Glass.Mapper.Sc.Fields.Link Link { get; set; }

        [SitecoreField(Templates.Blogger.Fields.AuthorImage)]
        public virtual Glass.Mapper.Sc.Fields.Image AuthorImage { get; set; }
    }
}