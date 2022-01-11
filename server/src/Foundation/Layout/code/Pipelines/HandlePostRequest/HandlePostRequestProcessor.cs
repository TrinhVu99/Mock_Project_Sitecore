using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockProject.Foundation.Layout.Pipelines.HandlePostRequest
{
    public abstract class HandlePostRequestProcessor
    {
        public abstract void Process(HandlePostRequestArgs args);
    }
}