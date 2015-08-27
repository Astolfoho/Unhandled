using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Routing;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(test.Startup))]

namespace test
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfiguraRoutes(RouteTable.Routes); 
            

        }

        private void ConfiguraRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
