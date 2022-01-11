using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetPlaceholderRenderings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MockProject.Foundation.Layout.Pipelines.GetPlaceholderRenderings
{
    public class GetDynamicKeyAllowedRenderings : GetAllowedRenderings
    {
        //text that ends in a GUID
        public const string DynamicKeyRegex = @"(.+)_[\d\w]{8}\-([\d\w]{4}\-){3}[\d\w]{12}";

        public new void Process(GetPlaceholderRenderingsArgs args)
        {
            Assert.IsNotNull(args, "args");

            var placeholderKey = args.PlaceholderKey;
            var regex = new Regex(DynamicKeyRegex);
            var match = regex.Match(placeholderKey);
            if (match.Success && match.Groups.Count > 0)
            {
                placeholderKey = match.Groups[1].Value;
                if (placeholderKey.ToCharArray().Skip(placeholderKey.Length - 2).FirstOrDefault() == '_')
                {
                    placeholderKey = placeholderKey.Substring(0, placeholderKey.Length - 2);
                }
            }
            else
            {
                return;
            }

            // Same as Sitecore.Pipelines.GetPlaceholderRenderings.GetAllowedRenderings but with fake placeholderKey
            Item placeholderItem;
            if (ID.IsNullOrEmpty(args.DeviceId))
            {
                placeholderItem = Client.Page.GetPlaceholderItem(placeholderKey, args.ContentDatabase,
                    args.LayoutDefinition);
            }
            else
            {
                using (new DeviceSwitcher(args.DeviceId, args.ContentDatabase))
                {
                    placeholderItem = Client.Page.GetPlaceholderItem(placeholderKey, args.ContentDatabase,
                        args.LayoutDefinition);
                }
            }

            List<Item> collection = null;
            if (placeholderItem != null)
            {
                bool flag;
                args.HasPlaceholderSettings = true;
                collection = GetRenderings(placeholderItem, out flag);
                if (flag)
                {
                    args.Options.ShowTree = false;
                }
            }
            if (collection == null)
            {
                return;
            }
            if (args.PlaceholderRenderings == null)
            {
                args.PlaceholderRenderings = new List<Item>();
            }
            args.PlaceholderRenderings.AddRange(collection);
        }
    }
}