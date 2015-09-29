using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace UnhandledApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            //config.MapHttpAttributeRoutes();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ApplicationsRoute",
                routeTemplate: "api/Applications/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Applications" }
            );

            //config.Routes.MapHttpRoute(
            //    name: "ErrorsOnApplicationRoute",
            //    routeTemplate: "api/Applications/{applicationId}/Errors/{id}",
            //    defaults: new { id = RouteParameter.Optional, controller = "Errors", action = "GetByAppId" }
            //);

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
