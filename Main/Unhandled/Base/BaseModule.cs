using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Unhandled.Base
{
    public abstract class BaseModule : IHttpModule
    {
        protected HttpRequest Request { get { return HttpContext.Current.Request; } }
        protected HttpResponse Response { get { return HttpContext.Current.Response; } }
        protected HttpContext HttpContext { get { return HttpContext.Current; } }


        public abstract void Dispose();
        public abstract void Init(HttpApplication context);
    }
}
