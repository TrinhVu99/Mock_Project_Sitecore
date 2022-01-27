using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using MockProject.Foundation.GlassMapper;
using MockProject.Feature.Sample.Constants;

namespace MockProject.Feature.Sample.Models.TemplateModels.Tag
{
    [SitecoreType(TemplateId = Templates.Navigation.TemplateId)]
    public class ListTagsModel : GlassBase<Item>
    {
        [SitecoreField(Templates.ListTag.Fields.Name)]
        public virtual string Name { get; set; }

        [SitecoreField(Templates.ListTag.Fields.Tags)]
        public virtual IEnumerable<TagModel> Tags { get; set; }
    }
}