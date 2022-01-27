using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels.MainModel.ListModel;
using MockProject.Feature.Sample.Models.TemplateModels;

namespace MockProject.Feature.Sample.Controller
{
    public class VideoController : BaseController
    {
        public VideoController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetVideoToday()
        {
            var model = MvcContext.GetDataSourceItem<ListVideoModel>();
            return View("~/Views/MockProject/Sample/VideoViews/VideoToday.cshtml", model);
        }

        public ActionResult GetPodcast()
        {
            var model = MvcContext.GetDataSourceItem<ListVideoModel>();
            return View("~/Views/MockProject/Sample/VideoViews/Podcast.cshtml", model);
        }

        public ActionResult GetImage()
        {
            var model = MvcContext.GetDataSourceItem<ImageBaseModel>();
            return View("~/Views/MockProject/Sample/VideoViews/ImageView.cshtml", model);
        }
    }
}