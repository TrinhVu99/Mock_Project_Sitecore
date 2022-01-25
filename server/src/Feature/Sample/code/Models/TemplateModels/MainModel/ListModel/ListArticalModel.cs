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
    [SitecoreType(TemplateId = Templates.ListArtical.TemplateId)]
    public class ListArticalModel : GlassBase<Item>
    {
        [SitecoreField(Templates.ListArtical.Fields.Name)]
        public virtual string Name { get; set; }
        [SitecoreField(Templates.ListArtical.Fields.Articals)]
        public virtual IEnumerable<ArticalModel> Articals { get; set; }
    }
}