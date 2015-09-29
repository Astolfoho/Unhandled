using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unhandled.Configuration;
using Unhandled.Helpers;
using Unhandled.Models;
using Unhandled.Repository.Interfaces;

namespace Unhandled.Repository.UnhandledApi
{
    public class UnhadledApplicationRepository : IUnhandledApplicationRepository
    {

        private HttpClient _client;

        public UnhadledApplicationRepository()
        {
            _client = new HttpClient(UnhandledConfiguration.Current.ApiUrl, "Applications");
        }

        public Application Create(Application app)
        {
            return _client.Post(app);
        }

        public Application GetByMachineNameAndApplicationName(string machineName, string applicationName)
        {


            return _client.Get<Application>(new KeyValuePair<string, string>("machineName", machineName),
                                             new KeyValuePair<string, string>("applicationName", applicationName));
        }

        public Application GetOrCreate()
        {
            var siteName = ServerHelper.GetApplicationName();
            var app = GetByMachineNameAndApplicationName(Environment.MachineName, siteName);

            if (app != null)
                return app;

            app = new Application
            {
                MachineName = Environment.MachineName,
                ApplicationName = siteName
            };

            app = Create(app);

            return app;
        }
    }
}
