using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockProject.Foundation.Layout.Pipelines.HandlePostRequest
{
    public static class HandlePostRequestPipeline
    {
        public static ActionResult HandlePostRequest(FormCollection formdata)
        {
            var args = new HandlePostRequestArgs(formdata);
            CorePipeline.Run("MockProjectRequest.HandlePostRequest", args);
            return args.ActionResult;
        }
    }
}