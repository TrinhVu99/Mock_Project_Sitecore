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
    public class AdvertismentRenderingModel : BaseRenderingModel<AdvertisementModel>
    {
        public override void Initialize(Rendering rendering)
        {
            base.Initialize(rendering);
            var getItemOptions = new GetItemByItemOptions()
            {
                Item = rendering.Item
            };
            Model = RequestContext.SitecoreService.GetItem<AdvertisementModel>(getItemOptions);
         
        }
    }
}