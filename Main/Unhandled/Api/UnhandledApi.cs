using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Unhandled.Factories.Repository;
using Unhandled.Models;
using Unhandled.Repository.Data;
using Unhandled.Repository.Interfaces;

namespace Unhandled.Api
{
    public class UnhandledApi
    {
        private UnhandledApi()
        {
            var rep = RepositoryFactory.Instance.CreateInstance<IUnhandledApplicationRepository>();
            CurrentApplication = rep.GetOrCreate();
        }

        private static UnhandledApi _instance;
        public static UnhandledApi Instance { get { return _instance; } }


        static UnhandledApi()
        {
            _instance = new UnhandledApi();
        }

        public Application CurrentApplication { get; set; }

        public void WriteException(Exception ex)
        {
            var uh = new Error(ex);
            uh.ApplicationId = this.CurrentApplication.Id;
            IUnhandledErrorRepository rep = RepositoryFactory.Instance.CreateInstance<IUnhandledErrorRepository>();
            uh = rep.Create(uh);

            var inner = ex.InnerException;

            while(inner != null)
            {
                var uhInner = new Error(inner);
                uhInner.ParentErrorId = uh.Id;
                rep.Create(uhInner);
                inner = inner.InnerException;
            }

            var currentRequest = HttpContext.Current.Request;

            foreach (var cookieKey in currentRequest.Cookies.AllKeys)
            {
                Cookie sc = new Cookie(currentRequest.Cookies[cookieKey]);
                sc.ErrorId = uh.Id;
                IUnhandledCookieRepository crep = RepositoryFactory.Instance.CreateInstance<IUnhandledCookieRepository>();
                crep.Create(sc);
            }
        }

    }
}
