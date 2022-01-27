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

        public ActionResult GetLotterySlidebar()
        {
            var model = MvcContext.GetDataSourceItem<SlidebarModel>();
            return View("~/Views/MockProject/Sample/Slidebars/LotterySlidebar.cshtml", model);
        }

        public ActionResult GetAdvertisementHorizontalSlidebar()
        {
            var model = MvcContext.GetDataSourceItem<SlidebarModel>();
            return View("~/Views/MockProject/Sample/Slidebars/AdvertisementHorizontal.cshtml", model);
        }

        public ActionResult GetAdvertisementBlockSlidebar()
        {
            var model = MvcContext.GetDataSourceItem<SlidebarModel>();
            return View("~/Views/MockProject/Sample/Slidebars/AdvertisementBlock.cshtml", model);
        }
    }
}


