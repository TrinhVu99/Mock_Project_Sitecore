using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockProject.Foundation.Layout.Pipelines.HandlePostRequest
{
    public class HandlePostRequestArgs: PipelineArgs
    {
        public string FormId { get; set; }
        public FormCollection FormData { get; set; }
        public ActionResult ActionResult { get; set; }
        public HandlePostRequestArgs()
        {

        }
        public HandlePostRequestArgs(FormCollection formData)
        {
            FormData = formData;
            FormId = formData["FormId"];
        }

    }
}