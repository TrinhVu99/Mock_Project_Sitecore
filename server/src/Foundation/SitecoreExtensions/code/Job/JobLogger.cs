using System;
using System.Collections.Generic;
using System.Text;
using MockProject.Foundation.SitecoreExtensions.Interfaces;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using MockProject.Foundation.SitecoreExtensions.Extensions;


namespace MockProject.Foundation.SitecoreExtensions.Job
{
    public class JobLogger : IJobLogger
    {
        private static StringBuilder mLogContent = new StringBuilder();
        private static IJobLogger mInstance;
        private List<Exception> mErrors = new List<Exception>();
        public static IJobLogger Instance
        {
            get { return mInstance ?? (mInstance = new JobLogger()); }
        }

        public struct LogItemStatus
        {
            public static readonly string Warning = "Warning";
            public static readonly string Error = "Error";
            public static readonly string Success = "Success";
        }
        public List<Exception> PackageErrors
        {
            get { return mErrors; }
        }

        /// <summary>
        /// Showing fatal error message 
        /// </summary>
        /// <param name="pTargetItem">Item which has message field</param>
        /// <param name="pErrorItem">Item with error</param>
        /// <param name="pFieldID">Field ID</param>
        /// <param name="pMessage">Message</param>
        public void ShowErrorMessage(Item pTargetItem, ID pFieldID, string pMessage)
        {
            ShowErrorMessage(pTargetItem, pFieldID, pMessage, null);
        }
        public void ShowErrorMessage(Item pTargetItem, ID pFieldID, string pMessage, Item pErrorItem)
        {
            BeginLogging();
            Instance.AddLogItemOfProcess(pMessage, pErrorItem, LogItemStatus.Error);
            EndLogging(pTargetItem, pFieldID);
        }

        /// <summary>
        /// Begin logging. Creating head of log file
        /// </summary>
        public static void BeginLogging()
        {
            mLogContent = new StringBuilder();
            mLogContent.Append("<ol>");
        }

        /// <summary>
        /// Add process item into log . Creating body of log file
        /// </summary>
        /// <param name="pProcessItem"></param>
        public void AddLogItemOfProcess(string pProcessItem)
        {
            AddLogItemOfProcess(pProcessItem, null, "", null);
        }
        public void AddLogItemOfProcess(string pProcessItem, Item pItemll)
        {
            AddLogItemOfProcess(pProcessItem, pItemll, "", null);
        }
        public void AddLogItemOfProcess(string pProcessItem, Item pItem, string pStatus)
        {
            AddLogItemOfProcess(pProcessItem, pItem, pStatus, null);
        }
        public void AddLogItemOfProcess(string pProcessItem, Item pItem, string pStatus, Exception pStackTrace)
        {
            pProcessItem = pProcessItem + " on " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            if (pItem != null)
            {
                pProcessItem += "<br/>Item : " + pItem.Name + " : " + pItem.ID + " : " + pItem.Paths.ContentPath;
            }
            var pColor = string.IsNullOrEmpty(pStatus) ? "black" : GetLogColor(pStatus != LogItemStatus.Success);
            if (string.IsNullOrEmpty(pColor))
            {
                mLogContent.Append("<li>" + pProcessItem + "</li>");
            }
            else
            {
                mLogContent.Append("<li style=\"color:" + pColor + "\">" + pProcessItem + "</li>");
            }
            if (pStackTrace != null)
            {
                mLogContent.Append("<span style=\"color:black \">" + pStackTrace.StackTrace + "</span>");
            }
            if (pStatus == LogItemStatus.Error)
            {
                mErrors.Add(new Exception(pProcessItem, pStackTrace));
            }
        }

        public void AddLogItemInfoPopUpAndLog(string pMessage)
        {
            Sitecore.Diagnostics.Log.Info(pMessage, this);
            AddLogItemOfProcessIntoPopUp(pMessage);
        }

        /// <summary>
        /// Showing process item on sitecore popUp during creating package
        /// </summary>
        /// <param name="message"></param>
        public void AddLogItemOfProcessIntoPopUp(string pMessage)
        {
            Context.Job.LogInfoSafe(pMessage);
        }

        /// <summary>
        /// Loggin a Message to Maing messages stream (Job status popup)
        /// </summary>
        /// <param name="message"></param>
        public void LogAsInfo(string pMessage)
        {
            AddLogItemOfProcessIntoPopUp(pMessage);
        }

        /// <summary>
        /// End logging. Creating the end of log file and saving it.
        /// </summary>
        public static void EndLogging(Item pItemApplyTo, ID pFieldID)
        {
            mLogContent.Append("</ol>");
            pItemApplyTo.Editing.BeginEdit();
            pItemApplyTo.Fields[pFieldID].Value = mLogContent + "<hr/>" + pItemApplyTo.Fields[pFieldID].Value;
            pItemApplyTo.Editing.EndEdit();
        }
        public static void EndLogging()
        {
            mLogContent.Append("</ol>");
        }

        /// <summary>
        /// Addding exception into log file
        /// </summary>
        /// <param name="ex"></param>
        public void AddExceptionItemIntoLog(Exception pException)
        {
            if(pException!=null)
            {
                Sitecore.Diagnostics.Error.LogError(pException.Message);
                AddLogItemOfProcessIntoPopUp(pException.Message);
            }
        }

        /// <summary>
        /// Get color for important ProcessActions
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <returns></returns>
        private string GetLogColor(bool pIsError)
        {
            if (pIsError)
            {
                return "red";
            }
            else
            {
                return "green";
            }
        }
    }
}
