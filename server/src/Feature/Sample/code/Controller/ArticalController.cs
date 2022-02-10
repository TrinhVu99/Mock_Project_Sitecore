using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels;

namespace MockProject.Feature.Sample.Controller
{
    public class ArticalController : BaseController
    {
        public ArticalController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetTrendingPost()
        {
            var model = MvcContext.GetDataSourceItem<ListArticalModel>();
            return View("~/Views/MockProject/Sample/ArticalViews/TrendingPost.cshtml", model);
        }

        public ActionResult GetBlogOfTheDay()
        {
            var model = MvcContext.GetDataSourceItem<ListArticalModel>();
            return View("~/Views/MockProject/Sample/ArticalViews/BlogOfTheDay.cshtml", model);
        }

        public ActionResult GetTopArticals()
        {
            var model = MvcContext.GetDataSourceItem<ListArticalModel>();
            return View("~/Views/MockProject/Sample/ArticalViews/TopArticals.cshtml", model);
        }
        public ActionResult GetArtical()
        {
            var model = MvcContext.GetDataSourceItem<ArticalModel>();
            return View("~/Views/MockProject/Sample/ArticalViews/DetailArtical/DetailArticle.cshtml", model);
        }

        public ActionResult GetYouMayLike()
        {
            var model = MvcContext.GetDataSourceItem<ListArticalModel>();
            return View("~/Views/MockProject/Sample/ArticalViews/DetailArtical/YouMayLike.cshtml", model);
        }
    }
}