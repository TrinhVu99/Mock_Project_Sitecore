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

    [SitecoreType(TemplateId = Templates.SignUp.TemplateId)]
    public class SignUpModel : GlassBase<Item>
    {
        [SitecoreField(Templates.SignUp.Fields.Name)]
        public virtual string Name { get; set; }

        [SitecoreField(Templates.SignUp.Fields.SubName)]
        public virtual string SubName { get; set; }

        [SitecoreField(Templates.SignUp.Fields.Slogan)]
        public virtual string Slogan { get; set; }
    }
}