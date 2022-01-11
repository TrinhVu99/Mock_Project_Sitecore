using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Jobs;
using Sitecore.Abstractions;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class JobExtentions
    {
        public static void LogInfoSafe(this BaseJobStatus pJobStatus, string pInfo)
        {
            if (!string.IsNullOrEmpty(pInfo) && pJobStatus != null)
            {
                var lInvalidChars = new char[] { '\r', '\n', '\'', '\"', '(', ')', '\\', '/', ':', '?', '`', '<', '>', '|', '[', ']', ',', '.', '!', '—' };
                pInfo = pInfo.RemoveChars(lInvalidChars).Trim();
                pInfo = HttpUtility.HtmlEncode(pInfo);
                if (!string.IsNullOrEmpty(pInfo))
                {
                    pJobStatus.LogInfo(pInfo);
                }
            }
        }

        public static void LogInfoSafe(this BaseJob pJob, string pInfo)
        {
            if (!string.IsNullOrEmpty(pInfo) && pJob != null)
            {
                Log.Info($"Job {pJob.Name}.{pJob.Status.Processed} logged: {pInfo}",pJob);
                pJob.Status.LogInfoSafe(pInfo);
                pJob.Status.Processed++;
            }
        }
    }
}