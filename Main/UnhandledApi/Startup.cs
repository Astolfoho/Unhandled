﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Microsoft.Practices.Unity;
using UnhandledApi.Repositories.Interfaces;
using UnhandledApi.Services;

[assembly: OwinStartup(typeof(UnhandledApi.Startup))]

namespace UnhandledApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {      
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new UnityContainer();
            UnityConfig.Configure(container);

            var resolver = new UnityDependencyResolver(container);

            DependencyResolver.SetResolver(resolver);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }
    }
}
