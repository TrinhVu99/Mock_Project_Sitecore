using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels;

namespace MockProject.Feature.Sample.Controller
{
    public class SlidebarController : BaseController
    {
        public SlidebarController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetBannerSlidebar()
        {
            var model = MvcContext.GetDataSourceItem<SlidebarModel>();
            return View("~/Views/MockProject/Sample/Slidebars/BannerSlidebar.cshtml", model);
        }


    }
}


