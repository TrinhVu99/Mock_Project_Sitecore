using MockProject.Foundation.Mvc.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockProject.Foundation.Mvc.Processors
{
    public class RenderViewProcessor
    {
        public virtual void Process(RenderViewPipelineArgs args)
        {
            if (args.IsDone)
            {
                args.AbortPipeline();
            }
        }
    }
}