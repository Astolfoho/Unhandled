using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace Unhandled.Helpers
{
    public static class ServerHelper
    {
        public static string GetApplicationName() {
            ServerManager mgr = new ServerManager();
            String SiteName = HostingEnvironment.ApplicationHost.GetSiteName();
            Site currentSite = mgr.Sites[SiteName];

            //The following obtains the application name and application object
            //The application alias is just the application name with the "/" in front

            String ApplicationAlias = HostingEnvironment.ApplicationVirtualPath;
            String ApplicationName = ApplicationAlias.Substring(1);
            Application app = currentSite.Applications[ApplicationAlias];
            return string.Format("{0}/{1}", SiteName, app.Path);
        }


    }
}
