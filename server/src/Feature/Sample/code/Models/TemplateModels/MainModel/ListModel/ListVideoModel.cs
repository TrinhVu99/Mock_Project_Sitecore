using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using MockProject.Foundation.GlassMapper;
using MockProject.Feature.Sample.Constants;


namespace MockProject.Feature.Sample.Models.TemplateModels.MainModel.ListModel
{
    [SitecoreType(TemplateId = Templates.ListArtical.TemplateId)]
    public class ListVideoModel : GlassBase<Item>
    {
        [SitecoreField(Templates.ListVideo.Fields.Name)]
        public virtual string Name { get; set; }
        [SitecoreField(Templates.ListVideo.Fields.Videos)]
        public virtual IEnumerable<VideoModel> Videos { get; set; }
    }
}