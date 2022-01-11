using Sitecore.Pipelines.ResolveRenderingDatasource;
using Sitecore.Data.Items;

namespace MockProject.Foundation.Layout.Pipelines.ResolveRenderingDatasource
{
    public class RelativeDatasourceResolver
    {

        public void Process(ResolveRenderingDatasourceArgs args)
        {
            if (!args.Datasource.StartsWith("query:"))
            {
                return;
            }

            //args.Datasource = Sitecore.Context.Item.TransformRelativeSourcePath(args.Datasource);
            Item queryItem = Sitecore.Context.Item.Axes.SelectSingleItem(args.Datasource.Substring(6));
            if (queryItem != null)
            {
                args.Datasource = queryItem.Paths.FullPath;
            }
        }
    }
}