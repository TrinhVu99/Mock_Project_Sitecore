using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels;

namespace MockProject.Feature.Sample.Controller
{
    public class AdvertismentController : BaseController
    {
        public AdvertismentController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetAdvertisments()
        {
            var model = MvcContext.GetDataSourceItem<AdvertisementModel>();
            return View("~/Views/MockProject/Sample/Advertisement.cshtml", model);
        }


    }
}