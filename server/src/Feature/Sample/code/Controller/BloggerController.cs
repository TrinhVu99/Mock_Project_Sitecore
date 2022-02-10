using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels;

namespace MockProject.Feature.Sample.Controller
{
    public class BloggerController : BaseController
    {
        public BloggerController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetBloggersRabbit()
        {
            var model = MvcContext.GetDataSourceItem<ListBloggerModel>();
            return View("~/Views/MockProject/Sample/BloggerViews/BloggerRabbit.cshtml", model);
        }
    }
}