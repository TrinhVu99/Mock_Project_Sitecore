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
    [SitecoreType(TemplateId = Templates.Category.TemplateId)]
    public class CategoryModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Category.Fields.Name)]
        public virtual string Name { get; set; }
        
        [SitecoreField(Templates.Category.Fields.Description)]
        public virtual string Description { get; set; }
    }
}