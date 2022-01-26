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
    [SitecoreType(TemplateId = Templates.Event.TemplateId)]
    public class EventModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Event.Fields.StartTime)]
        public virtual DateTime StartTime { get; set; }

        [SitecoreField(Templates.Event.Fields.EndTime)]
        public virtual DateTime EndTime { get; set; }

        [SitecoreField(Templates.Event.Fields.Location)]
        public virtual string Location { get; set; }

        [SitecoreField(Templates.Event.Fields.TitleEntity)]
        public virtual string TitleEntity { get; set; }

        [SitecoreField(Templates.Event.Fields.Category)]
        public virtual string Category { get; set; }

        [SitecoreField(Templates.Event.Fields.Views)]
        public virtual int Views { get; set; }

        [SitecoreField(Templates.Event.Fields.Tags)]
        public virtual List<string> Tags { get; set; }

        [SitecoreField(Templates.Event.Fields.Content)]
        public virtual string Content { get; set; }

        [SitecoreField(Templates.Event.Fields.Likes)]
        public virtual int Likes { get; set; }

        [SitecoreField(Templates.Event.Fields.Author)]
        public virtual string Author { get; set; }

        [SitecoreField(Templates.Event.Fields.Image)]
        public virtual Glass.Mapper.Sc.Fields.Image Image { get; set; }

        [SitecoreField(Templates.Event.Fields.Link)]
        public virtual Glass.Mapper.Sc.Fields.Link Link { get; set; }
    }
}