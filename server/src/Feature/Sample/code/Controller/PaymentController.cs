using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels.FooterNavigation;

namespace MockProject.Feature.Sample.Controller
{
    public class PaymentController : BaseController
    {
        public PaymentController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetPaymentContent()
        {
            var model = MvcContext.GetDataSourceItem<PaymentModel>();
            return View("~/Views/MockProject/Sample/FooterViews/FooterPaymentContent.cshtml", model);
        }

        public ActionResult GetPayment()
        {
            var model = MvcContext.GetDataSourceItem<PaymentModel>();
            return View("~/Views/MockProject/Sample/FooterViews/FooterPayment.cshtml", model);
        }
    }
}