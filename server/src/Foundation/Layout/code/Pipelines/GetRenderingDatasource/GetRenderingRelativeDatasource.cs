using Sitecore.Pipelines.GetRenderingDatasource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
namespace MockProject.Foundation.Layout.Pipelines.GetRenderingDatasource
{
    public class GetRenderingRelativeDatasource
    {
        public void Process(GetRenderingDatasourceArgs args)
        {
            //args.CurrentDatasource = args.ContextItemPath.TransformRelativeSourcePath(args.CurrentDatasource);
            if (!string.IsNullOrEmpty(args.CurrentDatasource) && args.CurrentDatasource.StartsWith("query:"))
            {
                Item queryItem = Sitecore.Context.Item.Axes.SelectSingleItem(args.CurrentDatasource.Substring(6));
                if (queryItem != null)
                {
                    args.CurrentDatasource = queryItem.Paths.FullPath;
                }
            }

        }
    }
}