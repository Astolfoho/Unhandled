using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Unhandled.Repository.UnhandledApi
{
    public static class HttpResponseMessageExtensions
    {

        public static T GetObject<T>(this HttpResponseMessage response){

            var contentType = response
                                .Headers
                                .FirstOrDefault(w => w.Key.ToLower() == "content-type")
                                .Value
                                .First();

            switch (contentType)
            {
                case "application/json":
                    return GetObjectFromJson<T>(response);
                default:
                    return GetObjectFromJson<T>(response);

            }

        }

        private static T GetObjectFromJson<T>(HttpResponseMessage response)
        {                
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            Stream responseData = null;
            response.Content.ReadAsStreamAsync().ContinueWith(c =>
            {
                responseData = c.Result;
            });

            return (T)serializer.ReadObject(responseData);               
          
        }

    }
}
