using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using Unhandled.Helpers;

namespace Unhandled.Repository.UnhandledApi
{
    internal static class HttpResponseExtensions
    {
        private static readonly string[] _unsuportedFormatTypes = { "sql" };

    

        public static void WriteObjectAsJson<T>(this WebRequest request, T obj)
        {

            using(Stream ms = request.GetRequestStream())
	        {
		        DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(ms,obj);
                request.ContentType = "application/json";
	        }
        }

        public static T ReadObjectFromJson<T>(this WebResponse request)
        {     
            using (Stream ms = request.GetResponseStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }

        public static T ReadObjectByContentType<T>(this WebResponse request)
        {
            switch (request.ContentType)
            {
                case "application/json":
                default:
                    return request.ReadObjectFromJson<T>();
            }
        }
    }
}
