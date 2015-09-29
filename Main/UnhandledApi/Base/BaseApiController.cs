using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using UnhandledApi.Base.ActionResults;
using UnhandledApi.Base.Attributes;
using UnhandledApi.Base.Models;

namespace UnhandledApi.Base
{
    public class BaseApiController : ApiController
    {
        private string _defaultRouteName;
        protected string DefaultRouteName
        {
            get
            {
                if(string.IsNullOrWhiteSpace(this._defaultRouteName))
                {
                    var ra = this.GetType().GetCustomAttributes(typeof(DefaultRouteNameAttribute), true).FirstOrDefault() as DefaultRouteNameAttribute;

                    if(ra != null)
                    {
                        this._defaultRouteName = ra.Name;
                    }

                }

                return this._defaultRouteName;
            }
        }

        protected IHttpActionResult Deleted()
        {
            return new NoContentActionResult(this.Request);
        }

        protected IHttpActionResult Created<T>(T content) where T: BaseModel
        {
            return CreatedAtRoute(this.DefaultRouteName, new { id = content.Id }, content);
        }

    }
}
