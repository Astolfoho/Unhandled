using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Unhandled.Factories.Repository;
using Unhandled.Models;
using Unhandled.Repository.Interfaces;

namespace Unhandled.Api
{
    public class UnhandledApi
    {
        private UnhandledApi()
        {

        }

        private static Lazy<UnhandledApi> _instance = new Lazy<UnhandledApi>(loadInstance);
        public static UnhandledApi Instance { get { return _instance.Value; } }


        private static UnhandledApi loadInstance()
        {
            return new UnhandledApi();
        }


        public void WriteException(Exception ex)
        {
            var uh = new UnhandledError(ex);
            IUnhandledErrorRepository rep = RepositoryFactory.Instance.CreateInstance<IUnhandledErrorRepository>();
            uh = rep.Create(uh);

            var inner = ex.InnerException;

            while(inner != null)
            {
                var uhInner = new UnhandledError(inner);
                uhInner.ParentErrorId = uh.Id;
                rep.Create(uhInner);
                inner = inner.InnerException;
            }

            var currentRequest = HttpContext.Current.Request;

            foreach (var cookieKey in currentRequest.Cookies.AllKeys)
            {
                UnhandledCookie sc = new UnhandledCookie(currentRequest.Cookies[cookieKey]);
                sc.UnhandledErrorId = uh.Id;
                IUnhandledCookieRepository crep = RepositoryFactory.Instance.CreateInstance<IUnhandledCookieRepository>();
                crep.Create(sc);
            }
        }

    }
}
