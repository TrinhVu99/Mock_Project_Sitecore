using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels;

namespace MockProject.Feature.Sample.Controller
{
    public class SectionController : BaseController
    {
        public SectionController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetHeroSection()
        {
            var model = MvcContext.GetDataSourceItem<SectionModel>();
            return View("~/Views/MockProject/Sample/SectionViews/HeroSection.cshtml", model);
        }

        public ActionResult GetBloggerSection()
        {
            var model = MvcContext.GetDataSourceItem<SectionModel>();
            return View("~/Views/MockProject/Sample/SectionViews/BloggerSection.cshtml", model);
        }

        public ActionResult GetVideoSection()
        {
            var model = MvcContext.GetDataSourceItem<SectionModel>();
            return View("~/Views/MockProject/Sample/SectionViews/VideoSection.cshtml", model);
        }

        public ActionResult GetUpcomingSection()
        {
            var model = MvcContext.GetDataSourceItem<SectionModel>();
            return View("~/Views/MockProject/Sample/SectionViews/UpcomingSection.cshtml", model);
        }

        public ActionResult GetAdvertisingSection()
        {
            var model = MvcContext.GetDataSourceItem<SectionModel>();
            return View("~/Views/MockProject/Sample/SectionViews/AdvertisingSection.cshtml", model);
        }
    }
}