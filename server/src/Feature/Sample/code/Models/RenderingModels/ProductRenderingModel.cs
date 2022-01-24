using MockProject.Feature.Sample.Models.TemplateModels;
using MockProject.Foundation.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Mvc.Presentation;
using Glass.Mapper.Sc;
namespace MockProject.Feature.Sample.Models.RenderingModels
{
    public class ProductRenderingModel : BaseRenderingModel<ProductModel>
    {
        public override void Initialize(Rendering rendering)
        {
            base.Initialize(rendering);
            var getItemOptions = new GetItemByItemOptions()
            {
                Item = rendering.Item
            };
            Model = RequestContext.SitecoreService.GetItem<ProductModel>(getItemOptions);

        }
    }
}