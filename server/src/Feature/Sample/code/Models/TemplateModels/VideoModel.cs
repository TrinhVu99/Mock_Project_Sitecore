using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using MockProject.Foundation.GlassMapper;
using MockProject.Feature.Sample.Constants;
using System.Linq;

namespace MockProject.Feature.Sample.Models.TemplateModels
{
    [SitecoreType(TemplateId = Templates.Artical.TemplateId)]
    public class VideoModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Video.Fields.LinkVideo)]
        public virtual string LinkVideo { get; set; }

        [SitecoreField(Templates.Video.Fields.TitleEntity)]
        public virtual string TitleEntity { get; set; }

        [SitecoreField(Templates.Video.Fields.Category)]
        public virtual CategoryModel Category { get; set; }

        [SitecoreField(Templates.Video.Fields.Views)]
        public virtual int Views { get; set; }

        [SitecoreField(Templates.Video.Fields.Tags)]
        public virtual List<string> Tags { get; set; }

        [SitecoreField(Templates.Video.Fields.Content)]
        public virtual string Content { get; set; }

        [SitecoreField(Templates.Video.Fields.Likes)]
        public virtual int Likes { get; set; }

        [SitecoreField(Templates.Video.Fields.Author)]
        public virtual string Author { get; set; }

        [SitecoreField(Templates.Video.Fields.Image)]
        public virtual Glass.Mapper.Sc.Fields.Image Image { get; set; }

        [SitecoreField(Templates.Video.Fields.Link)]
        public virtual Glass.Mapper.Sc.Fields.Link Link { get; set; }
    }
}