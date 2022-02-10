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

        public ActionResult GetEvent()
        {
            var model = MvcContext.GetDataSourceItem<EventModel>();
            return View("~/Views/MockProject/Sample/EventViews/EventDetail.cshtml", model);
        }

        public ActionResult GetUpcommingEventsBlock()
        {
            var model = MvcContext.GetDataSourceItem<ListEventModel>();
            return View("~/Views/MockProject/Sample/EventViews/UpcommingEventsBlock.cshtml", model);
        }

        public ActionResult GetRelatedEvents()
        {
            var model = MvcContext.GetDataSourceItem<ListEventModel>();
            return View("~/Views/MockProject/Sample/EventViews/RelatedEvents.cshtml", model);
        }

        public ActionResult GetEventLanding()
        {
            var model = MvcContext.GetDataSourceItem<EventModel>();
            return View("~/Views/MockProject/Sample/EventLanding/EventLading.cshtml", model);
        }

        public ActionResult GetLatestPosts()
        {
            var model = MvcContext.GetDataSourceItem<ListEventModel>();
            return View("~/Views/MockProject/Sample/EventLanding/LatestPosts.cshtml", model);
        }

        public ActionResult GetLatestEvents()
        {
            var model = MvcContext.GetDataSourceItem<ListEventModel>();
            return View("~/Views/MockProject/Sample/EventLanding/LatestEvents.cshtml", model);
        }
    }
}