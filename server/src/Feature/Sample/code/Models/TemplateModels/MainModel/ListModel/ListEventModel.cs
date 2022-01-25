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
    [SitecoreType(TemplateId = Templates.ListEvent.TemplateId)]
    public class ListEventModel : GlassBase<Item>
    {
        [SitecoreField(Templates.ListEvent.Fields.Name)]
        public virtual string Name { get; set; }
        [SitecoreField(Templates.ListEvent.Fields.Events)]
        public virtual IEnumerable<EventModel> Events { get; set; }
    }
}