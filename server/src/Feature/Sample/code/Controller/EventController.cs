using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels;

namespace MockProject.Feature.Sample.Controller
{
    public class EventController : BaseController
    {
        public EventController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetUpcomingEvents()
        {
            var model = MvcContext.GetDataSourceItem<ListEventModel>();
            return View("~/Views/MockProject/Sample/EventViews/UpcomingEvents.cshtml", model);
        }

        public ActionResult GetAllEvent()
        {
            var model = MvcContext.GetDataSourceItem<ListEventModel>();
            return View("~/Views/MockProject/Sample/EventViews/AllEvents.cshtml", model);
        }
    }
}