using Glass.Mapper.Sc.Web.Mvc;
using System.Web.Mvc;
using MockProject.Foundation.Mvc.Controllers;
using MockProject.Feature.Sample.Models.TemplateModels.FooterNavigation;

namespace MockProject.Feature.Sample.Controller
{
    public class NavigationFooterController : BaseController
    {
        public NavigationFooterController(IMvcContext mvcContext) : base(mvcContext)
        {
        }

        public ActionResult GetNavigationContent()
        {
            var model = MvcContext.GetDataSourceItem<NavigationFooterModel>();
            return View("~/Views/MockProject/Sample/FooterViews/NavigationContent.cshtml", model);
        }

        public ActionResult GetNavigationContentAboutTerm()
        {
            var model = MvcContext.GetDataSourceItem<NavigationFooterModel>();
            return View("~/Views/MockProject/Sample/FooterViews/NavigationContentAboutTerm.cshtml", model);
        }

        public ActionResult GetNavigation()
        {
            var model = MvcContext.GetDataSourceItem<NavigationFooterModel>();
            return View("~/Views/MockProject/Sample/FooterViews/Navigation.cshtml", model);
        }
        public ActionResult GetSignUp()
        {
            var model = MvcContext.GetDataSourceItem<SignUpModel>();
            return View("~/Views/MockProject/Sample/FooterViews/FooterSignUp.cshtml", model);
        }

        public ActionResult GetSocial()
        {
            var model = MvcContext.GetDataSourceItem<SocialModel>();
            return View("~/Views/MockProject/Sample/FooterViews/NavigationSocial.cshtml", model);
        }
    }
}