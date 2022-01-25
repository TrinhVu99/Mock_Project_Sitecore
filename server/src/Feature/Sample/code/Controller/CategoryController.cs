using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels;
using System.Linq;

namespace MockProject.Feature.Sample.Controller
{
    public class CategoryController : BaseController
    {
        public CategoryController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetTopNavigationBar()
        {
            var model = MvcContext.GetDataSourceItem<NavCategoryModel>();
            return View("~/Views/MockProject/Sample/GetTopNavigationBar.cshtml", model);
        }


    }
}