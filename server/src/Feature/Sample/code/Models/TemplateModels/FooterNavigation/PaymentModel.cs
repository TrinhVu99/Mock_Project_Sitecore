using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using MockProject.Foundation.GlassMapper;
using MockProject.Feature.Sample.Constants;

namespace MockProject.Feature.Sample.Models.TemplateModels.FooterNavigation
{

    [SitecoreType(TemplateId = Templates.FooterPayment.TemplateId)]
    public class PaymentModel : GlassBase<Item>
    {
        [SitecoreField(Templates.FooterPayment.Fields.Name)]
        public virtual string Name { get; set; }

        [SitecoreField(Templates.FooterPayment.Fields.Image)]
        public virtual Glass.Mapper.Sc.Fields.Image Image { get; set; }

        [SitecoreField(Templates.FooterPayment.Fields.Alt)]
        public virtual string Alt { get; set; }
    }
}