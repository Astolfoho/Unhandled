using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace UnhandledApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ErrorsRoute",
                routeTemplate: "api/Errors/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Errors" }
            );

            config.Routes.MapHttpRoute(
                name: "CookiesRoute",
                routeTemplate: "api/Errors/{idError}/Cookies/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Cookies" }
            );
        }
    }
}
