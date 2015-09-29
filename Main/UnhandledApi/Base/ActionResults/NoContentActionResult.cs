using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace UnhandledApi.Base.ActionResults
{
    public class NoContentActionResult : IHttpActionResult
    {
        private HttpRequestMessage _request;
        public NoContentActionResult(HttpRequestMessage request)
        {
            this._request = request;
        }

          

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var ret = this._request.CreateResponse(System.Net.HttpStatusCode.NoContent);
            return Task.FromResult(ret);
        }
    }
}
