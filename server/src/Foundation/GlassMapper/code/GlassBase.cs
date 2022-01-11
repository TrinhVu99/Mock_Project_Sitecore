using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration;
using Sitecore.Globalization;
using Sitecore.Data;

namespace MockProject.Foundation.GlassMapper
{
    public class GlassBase<T> : IGlassBase<T>
    {
        [SitecoreItem]
        [JsonIgnore]
        public virtual Item Current { get; private set; }

        [SitecoreId]
        [JsonIgnore]
        public virtual ID Id { get; private set; }

        [SitecoreInfo(SitecoreInfoType.DisplayName)]
        public virtual string DisplayName { get; private set; }

        [SitecoreInfo(SitecoreInfoType.Language)]
        public virtual Language Language { get; private set; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        public virtual int Version { get; private set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        [JsonIgnore]
        public virtual string Url { get; private set; }

        [SitecoreChildren(InferType = true)]
        public virtual IEnumerable<T> Children { get; private set; }
    }
    public interface IGlassBase<T>
    {
        [SitecoreItem]
        [JsonIgnore]
        Item Current { get; }

        [SitecoreId]
        [JsonIgnore]
        ID Id { get; }

        [SitecoreInfo(SitecoreInfoType.DisplayName)]
        string DisplayName { get; }

        [SitecoreInfo(SitecoreInfoType.Language)]
        Language Language { get; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        int Version { get; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        [JsonIgnore]
        string Url { get;}

        [SitecoreChildren(InferType = true)]
        IEnumerable<T> Children { get; }
    }
}