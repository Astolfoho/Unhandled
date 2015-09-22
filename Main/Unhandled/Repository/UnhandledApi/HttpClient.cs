using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using NetHttpClient = System.Net.Http.HttpClient;

namespace Unhandled.Repository.UnhandledApi
{
    public class HttpClient
    {
        private string _apiUrl;



        public HttpClient(string apiUrl)
        {
            this._apiUrl = apiUrl;
        }

        public T Post<T>(T data) where T : class
        {
            T ret = null;
            using (var client = new NetHttpClient())
            {
                client.SetDefaultHeaders();
                var content = new JsonHttpContent<T>(data);
                HttpResponseMessage response = null;
                client.PostAsync(_apiUrl, content).ContinueWith((c) =>
                {
                    response = c.Result;
                });
                ret = response.GetObject<T>();
            }
            return ret;
        }

        public T Get<T>(long id = 0) where T : class
        {
            T ret = null;
            using (var client = new NetHttpClient())
            {
                client.SetDefaultHeaders();
                HttpResponseMessage response = null;
                var url = _apiUrl;
                if (id > 0)
                {
                    url = string.Concat(url.TrimEnd('/'), "/", id);
                }

                client.GetAsync(url).ContinueWith((c) =>
                {
                    response = c.Result;
                });
                ret = response.GetObject<T>();
            }
            return ret;
        }

        public void Delete<T>(int id = 0) where T : class
        {
            using (var client = new NetHttpClient())
            {
                client.SetDefaultHeaders();
                client.DeleteAsync(string.Concat(_apiUrl.TrimEnd('/'), "/", id)).Start();
            }
        }

    }

    public static class HttpClientExtensions
    {
        public static void SetDefaultHeaders(this NetHttpClient client)
        {
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("x-machine-name", Environment.MachineName);
            client.DefaultRequestHeaders.Add("x-site-name", HostingEnvironment.SiteName);
        }
    }
}
