using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Unhandled.Base;
using Unhandled.Factories.Repository;
using Unhandled.Models;
using Unhandled.Repository.Data;
using Unhandled.Repository.Interfaces;

namespace Unhandled.HttpModules
{
    public class UnhandledModule : BaseModule
    {


        public override void Dispose()
        {
            
        }

        public override void Init(HttpApplication context)
        {
            DbFactory.InitDatabase();
            context.Error += context_Error;
        }

        void context_Error(object sender, EventArgs e)
        {
            var uh = new UnhandledError(HttpContext.Error);
            IUnhandledErrorRepository rep = RepositoryFactory.Instance.CreateInstance<IUnhandledErrorRepository>();
            uh = rep.Create(uh);

            foreach (var cookieKey in Request.Cookies.AllKeys)
            {
                UnhandledCookie sc = new UnhandledCookie(Request.Cookies[cookieKey]);
                sc.UnhandledErrorId = uh.Id;
                IUnhandledCookieRepository crep = RepositoryFactory.Instance.CreateInstance<IUnhandledCookieRepository>();
                crep.Create(sc);
            }

          
        }           
    }

    


   
}
