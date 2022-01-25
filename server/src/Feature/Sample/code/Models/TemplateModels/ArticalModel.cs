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
    [SitecoreType(TemplateId = Templates.Artical.TemplateId)]
    public class ArticalModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Artical.Fields.Name)]
        public virtual string Name { get; set; }

        [SitecoreField(Templates.Artical.Fields.Description)]
        public virtual string Description { get; set; }

        [SitecoreField(Templates.Artical.Fields.Images)]
        public virtual IEnumerable<ImageBaseModel> Images { get; set; }

        [SitecoreField(Templates.Artical.Fields.Title)]
        public virtual string Title { get; set; }

        [SitecoreField(Templates.Artical.Fields.Category)]
        public virtual string Category { get; set; }

        [SitecoreField(Templates.Artical.Fields.Views)]
        public virtual int Views { get; set; }

        [SitecoreField(Templates.Artical.Fields.Tags)]
        public virtual List<string> Tags { get; set; }

        [SitecoreField(Templates.Artical.Fields.Content)]
        public virtual string Content { get; set; }

        [SitecoreField(Templates.Artical.Fields.Likes)]
        public virtual int Likes { get; set; }

        [SitecoreField(Templates.Artical.Fields.Author)]
        public virtual string Author { get; set; }

        [SitecoreField(Templates.Artical.Fields.Image)]
        public virtual Glass.Mapper.Sc.Fields.Image Image { get; set; }

        [SitecoreField(Templates.Artical.Fields.Link)]
        public virtual Glass.Mapper.Sc.Fields.Link Link { get; set; }
    }
}