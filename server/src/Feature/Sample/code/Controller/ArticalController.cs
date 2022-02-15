using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels;
using Glass.Mapper.Sc;

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
            var item = Sitecore.Context.Item;
            var context = new SitecoreService(Sitecore.Context.Database);
            var modelArtical = context.GetItem<ArticalModel>(item);
            var model = MvcContext.GetDataSourceItem<ArticalModel>();
            return View("~/Views/MockProject/Sample/ArticalViews/DetailArtical/DetailArticle.cshtml", modelArtical);
        }

        public ActionResult GetYouMayLike()
        {
            var model = MvcContext.GetDataSourceItem<ListArticalModel>();
            return View("~/Views/MockProject/Sample/ArticalViews/DetailArtical/YouMayLike.cshtml", model);
        }
    }
}