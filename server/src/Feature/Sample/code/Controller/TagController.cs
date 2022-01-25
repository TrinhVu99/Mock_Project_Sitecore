using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels.Tag;

namespace MockProject.Feature.Sample.Controller
{
    public class TagController : BaseController
    {
        public TagController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetTagContent()
        {
            var model = MvcContext.GetDataSourceItem<ListTagsModel>();
            return View("~/Views/MockProject/Sample/Tag/Tag.cshtml", model);
        }
    }
}