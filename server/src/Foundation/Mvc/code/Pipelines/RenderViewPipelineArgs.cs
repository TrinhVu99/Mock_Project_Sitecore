using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.Pipelines;

namespace MockProject.Foundation.Mvc.Pipelines
{
    public class RenderViewPipelineArgs : PipelineArgs
    {
        private readonly Item _dataItem;
        private readonly HtmlHelper _htmlHelper;

        private readonly Dictionary<string, object> _parameters;

        public RenderViewPipelineArgs(Item dataItem, HtmlHelper htmlHelper) : this(dataItem, htmlHelper, null)
        {

        }
        public RenderViewPipelineArgs(Item dataItem, HtmlHelper htmlHelper, object parameters)
        {
            _dataItem = dataItem;
            _htmlHelper = htmlHelper;
            _parameters = GetParametersFromObject(parameters);
        }
        public bool IsDone { get; set; }
        public Item DataItem
        {
            get
            {
                return _dataItem;
            }
        }
        public HtmlHelper HtmlHelper
        {
            get
            {
                return _htmlHelper;
            }
        }
        public Dictionary<string, object> Parameters
        {
            get
            {
                return _parameters;
            }
        }
        public HtmlString ViewResult { get; set; }
        private Dictionary<string, object> GetParametersFromObject(object parameters)
        {
            var result = new Dictionary<string, object>();
            if (parameters != null)
            {
                var props = parameters.GetType().GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    result.Add(prop.Name, prop.GetValue(parameters));
                }
            }
            return result;
        }
    }
}