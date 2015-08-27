using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Unhandled.Api;
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
            UnhandledApi.Instance.WriteException(HttpContext.Error);
        }           
    }

    


   
}
