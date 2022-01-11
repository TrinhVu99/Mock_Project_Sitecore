using Sitecore.Diagnostics;
using Sitecore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Kernel;
using Sitecore.Publishing;
using System.Security.Policy;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Data.Events;

namespace MockProject.Foundation.Caching.Cache
{
    public class ApplicationCacheClearer
    {
        public void ClearCache()
        {
            Log.Info("ApplicationCacheClearer clearing.", this);
            ApplicationCacheManager.ClearCaches();
            Log.Info("ApplicationCacheClearer done.", this);
        }

        public void OnPublishEnd(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, nameof(sender));
            Assert.ArgumentNotNull(args, nameof(args));

            SitecoreEventArgs sitecoreArgs = args as SitecoreEventArgs;
            if (sitecoreArgs == null)
            {
                return;
            }

            Sitecore.Publishing.Publisher publisher = sitecoreArgs.Parameters[0] as Sitecore.Publishing.Publisher;
            if (publisher == null)
            {
                return;
            }

            // TOTO: need to optimize cache
            ClearCache();

        }

        public void OnPublishEndRemote(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, nameof(sender));
            Assert.ArgumentNotNull(args, nameof(args));

            PublishEndRemoteEventArgs sitecoreArgs = args as PublishEndRemoteEventArgs;
            if (sitecoreArgs == null)
            {
                return;
            }

            ClearCache();
        }
    }
}