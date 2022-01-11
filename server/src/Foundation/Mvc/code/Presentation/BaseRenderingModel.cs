using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.DependencyInjection;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using System.Reflection;

namespace MockProject.Foundation.Mvc.Presentation
{
    public class BaseRenderingModel<T> : RenderingModel
    {
        public BaseRenderingModel()
        {
            RequestContext = (IRequestContext)ServiceLocator.ServiceProvider.GetService(typeof(IRequestContext));
            SitecoreService = RequestContext.SitecoreService;
        }
       
        private Item _currentItem;
        public virtual void Initialize(Item currentItem)
        {
            _currentItem = currentItem;
            Initialize(RenderingContext.Current.Rendering);
        }       
        public override Item Item
        {
            get
            {
                if (_currentItem == null)
                {
                    return base.Item;
                }
                return _currentItem;
            }
        }
        protected IRequestContext RequestContext;
        protected ISitecoreService SitecoreService;
        public T Model { get; set; }
        public virtual void SetParametersToModel(Dictionary<string,object> parameters)
        {
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    PropertyInfo propertyInfo = this.GetType().GetProperty(parameter.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(this, parameter.Value);
                    }
                }
            }
        }
    }
}