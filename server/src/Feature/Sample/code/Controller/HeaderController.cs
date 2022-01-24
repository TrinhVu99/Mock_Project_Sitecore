using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels;

namespace MockProject.Feature.Sample.Controller
{
    public class HeaderController : BaseController
    {
        public HeaderController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetHeader()
        {
            var model = MvcContext.GetDataSourceItem<HeaderModel>();
            return View("~/Views/MockProject/Sample/Header.cshtml", model);
        }


    }
}