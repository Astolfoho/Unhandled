using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using Unhandled.Helpers;
using Unhandled.Models;
using Unhandled.Repository.Data;
using Unhandled.Repository.Interfaces;

namespace Unhandled.Repository.SqlServer
{
    public class UnhandledApplicationRepository : IUnhandledApplicationRepository
    {
        public Application Create(Application app)
        {
            using (var db = DbFactory.CreateConnection("unhandled.ApplicationRepository_Create"))
            {
                db.EnsureParameter(app);
                app.Id = db.ExecuteScalar<long>();
            }
            return app;
        }

        public Application GetByMachineNameAndApplicationName(string machineName, string siteName)
        {
            using (var db = DbFactory.CreateConnection("unhandled.ApplicationRepository_GetByMachineNameAndApplicationName"))
            {
                db.EnsureParameter("MachineName", machineName);
                db.EnsureParameter("ApplicationName", siteName);
                using(IDataReader reader = db.ExecuteReader())
                {
                    return reader.MapObject<Application>();
                }
            }
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
