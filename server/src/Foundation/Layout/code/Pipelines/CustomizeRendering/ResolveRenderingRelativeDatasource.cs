//using Sitecore.Mvc.Analytics.Pipelines.Response.CustomizeRendering;
//using Sitecore.Pipelines;
//using Sitecore.Pipelines.ResolveRenderingDatasource;


//namespace MockProject.Foundation.Layout.Pipelines.CustomizeRendering
//{
//    public class ResolveRenderingRelativeDatasource : CustomizeRenderingProcessor
//    {
//        public override void Process(CustomizeRenderingArgs args)
//        {
//            var rendering = args.Rendering;
//            if (!rendering.DataSource.StartsWith("query:"))
//            {
//                return;
//            }
//            var renderingDatasourceArgs = new ResolveRenderingDatasourceArgs(rendering.DataSource);
//            CorePipeline.Run("resolveRenderingDatasource", renderingDatasourceArgs);
//            rendering.DataSource = renderingDatasourceArgs.Datasource;
//        }
//    }
//}