using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Hosting;
using NetHttpClient = System.Net.Http.HttpClient;

namespace Unhandled.Repository.UnhandledApi
{
    public class HttpClient
    {
        private string _apiUrl;

        public HttpClient(string apiUrl, string resourceName)
        {
            this._apiUrl = string.Format("{0}/{1}",apiUrl.TrimEnd('/'),resourceName.TrimStart('/'));
        }

        //public T Post<T>(T data) where T : class
        //{
        //    T ret = null;
        //    using (var client = new NetHttpClient())
        //    {
        //        client.SetDefaultHeaders();
        //        var content = new JsonHttpContent(data);
        //        HttpResponseMessage response = null;
        //        var t = client.PostAsync(_apiUrl, content)
        //            .ContinueWith((c) =>
        //            {
        //                c.Result.EnsureSuccessStatusCode();
        //                response = c.Result;
        //            });
        //        t.Wait(3000);

        //        ret = response.GetObject<T>();
        //    }
        //    return ret;
        //}

        public T Post<T>(T data) where T : class
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_apiUrl);
            request.Method = "POST";
            request.WriteObjectAsJson(data);            
            request.Accept = "application/json";
            request.Headers.Add("x-machine-name", Environment.MachineName);
            request.Headers.Add("x-site-name", HostingEnvironment.SiteName);
            System.Net.WebResponse response = request.GetResponse();    
            return response.ReadObjectByContentType<T>();
        }

        public T Get<T>(params KeyValuePair<string, string>[] queryString) where T : class
        {
            T ret = null;
            using (var client = new NetHttpClient())
            {
                client.SetDefaultHeaders();
                HttpResponseMessage response = null;

                var qs = queryString
                        .Select(s => string.Format("{0}={1}", s.Key, HttpUtility.UrlEncode(s.Value)))
                        .Aggregate((a, b) => string.Format("{0}&{1}", a, b));

                var url = _apiUrl + "?" +qs;

         

                client.GetAsync(url).ContinueWith((c) =>
                {
                    response = c.Result;
                }).Wait();
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
                }).Wait(); ;
                ret = response.GetObject<T>();
            }
            return ret;
        }

        public T Get<T>(string url) where T : class
        {
            T ret = null;
            using (var client = new NetHttpClient())
            {
                client.SetDefaultHeaders();
                HttpResponseMessage response = null;
                client.GetAsync(url).ContinueWith((c) =>
                {
                    response = c.Result;
                }).Wait(); ;
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
            client.DefaultRequestHeaders.Add("x-machine-name", Environment.MachineName);
            client.DefaultRequestHeaders.Add("x-site-name", HostingEnvironment.SiteName);
        }
    }
}
