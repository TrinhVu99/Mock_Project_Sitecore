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
    [SitecoreType(TemplateId = Templates.Product.TemplateId)]
    public class ProductModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Product.Fields.Name)]
        public virtual string Name { get; set; }
        [SitecoreField(Templates.Product.Fields.Image)]
        public virtual Glass.Mapper.Sc.Fields.Image Image { get; set; }
    }
}