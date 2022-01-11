using System;
using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MockProject.Foundation.SitecoreExtensions.Interfaces
{
    public interface IJobLogger
    {
        void AddLogItemOfProcess(string pProcessItem);
        void AddLogItemOfProcess(string pProcessItem, Item pItemll);
        void AddLogItemOfProcess(string pProcessItem, Item pItem, string pStatus);
        void AddLogItemOfProcess(string pProcessItem, Item pItem, string pStatus, Exception pStackTrace);
        void AddLogItemInfoPopUpAndLog(string pMessage);
        void AddLogItemOfProcessIntoPopUp(string pMessage);
        void AddExceptionItemIntoLog(Exception pException);
        void LogAsInfo(string pMessage);
        void ShowErrorMessage(Item pTargetItem, ID pFieldID, string pMessage);
        void ShowErrorMessage(Item pTargetItem, ID pFieldID, string pMessage, Item pErrorItem);
        List<Exception> PackageErrors { get; }
    }
}
