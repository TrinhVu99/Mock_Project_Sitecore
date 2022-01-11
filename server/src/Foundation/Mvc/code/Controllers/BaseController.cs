using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockProject.Foundation.Mvc.Controllers
{

    public abstract class BaseController : SitecoreController
    {
        public BaseController(IMvcContext mvcContext)
        {
            MvcContext = mvcContext;
        }

        protected IMvcContext MvcContext;

        #region Helpers

        public string PartialViewResultToString(PartialViewResult partialViewResult)
        {
            return PartialViewResultToString(partialViewResult, this);
        }
        private string PartialViewResultToString(PartialViewResult partialViewResult, Controller controller)
        {
            StringWriter sw = new StringWriter();

            if (partialViewResult.View == null)
            {
                var result = partialViewResult.ViewEngineCollection.FindPartialView(controller.ControllerContext, partialViewResult.ViewName);
                if (result.View == null)
                {
                    throw new InvalidOperationException(
                                   "Unable to find view. Searched in: " +
                                   string.Join(",", result.SearchedLocations));
                }
                partialViewResult.View = result.View;
            }

            ViewContext viewContext = new ViewContext(controller.ControllerContext
                                        , partialViewResult.View
                                        , partialViewResult.ViewData
                                        , partialViewResult.TempData
                                        , sw);
            partialViewResult.View.Render(viewContext, sw);
            return sw.ToString();
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion
    }
}