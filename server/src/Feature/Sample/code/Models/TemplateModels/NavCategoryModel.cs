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
    [SitecoreType(TemplateId = Templates.Header.TemplateId)]
    public class NavCategoryModel : GlassBase<Item>
    {
        [SitecoreField(Templates.NavCategory.Fields.Name)]
        public virtual string WebName { get; set; }
        
        [SitecoreField(Templates.NavCategory.Fields.Categories)]
        public virtual IEnumerable<CategoryModel> Categories { get; set; }
    }
}