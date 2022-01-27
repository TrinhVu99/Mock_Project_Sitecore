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

    [SitecoreType(TemplateId = Templates.Social.TemplateId)]
    public class SocialModel : GlassBase<Item>
    {
        [SitecoreField(Templates.Social.Fields.LogoFooter)]
        public virtual Glass.Mapper.Sc.Fields.Image LogoFooter { get; set; }

        [SitecoreField(Templates.Social.Fields.ListSocial)]
        public virtual IEnumerable<ImageBaseModel> ListSocial { get; set; }
    }
}