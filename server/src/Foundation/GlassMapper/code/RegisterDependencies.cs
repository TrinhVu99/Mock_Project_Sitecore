using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc;
using Microsoft.Extensions.DependencyInjection;
using Glass.Mapper.Sc.Web.Mvc;
using Glass.Mapper.Sc.Web;
using Sitecore.Data;
using Glass.Mapper.Sc.Web.WebForms;
using MockProject.Foundation.DependencyInjection;

namespace MockProject.Foundation.GlassMapper
{
    public class RegisterDependencies : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ISitecoreService>(_ => new SitecoreService(Sitecore.Context.Database));
            serviceCollection.AddScoped<IMvcContext>(_ => new MvcContext(_.GetService<ISitecoreService>()));
            serviceCollection.AddScoped<IRequestContext>(_ => new RequestContext(_.GetService<ISitecoreService>()));
            serviceCollection.AddScoped<IWebFormsContext>(_ => new WebFormsContext(_.GetService<ISitecoreService>()));
        }
    }  
}