using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using Unhandled.Helpers;

namespace System.Web
{
    internal static class HttpResponseExtensions
    {
        private static readonly string[] _unsuportedFormatTypes = { "sql" };

        public static void WriteEmbededFile(this HttpResponse response, string resourceName, string resourceType)
        {

            if (_unsuportedFormatTypes.Any(a => a.Equals(resourceType, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new Exception("Unsuported resource type");
            }

            response.BinaryWrite(AssetsHelper.GetEmbededResourceBinary(resourceName, resourceType));
        }

        public static void WriteObjectAsJson<T>(this HttpResponse response, T obj)
        {

            using(MemoryStream ms = new MemoryStream())
	        {
		        DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(ms,obj);

                using(StreamReader sr = new StreamReader(ms))
                {
                    ms.Position = 0;
                    response.Write(sr.ReadToEnd());
                }
	        }
        }

        public static void RespondObjectAsJson<T>(this HttpResponse response, T obj)
        {
            WriteObjectAsJson(response, obj);
            SetContentTypeByExtesion(response, "json");
            response.End();
        }

        public static void SetContentTypeByExtesion(this HttpResponse response, string extension)
        {
            var contentType = string.Empty;

            switch (extension)
            {
                case "html":
                    contentType = "text/html";
                    break;

                case "js":
                    contentType = "text/javascript";
                    break;

                case "css":
                    contentType = "text/css";
                    break;


                case "json":
                    contentType = "application/json";
                    break;


                case "svg":
                    contentType = "image/svg+xml";
                    break;



                case "ttf":
                    contentType = "font/ttf";
                    break;

                case "woff":
                    contentType = "font/woff";
                    break;

                case "woff2":
                    contentType = "font/woff2";
                    break;

                case "eot":
                    contentType = "font/eot";
                    break;


            }

            response.ContentType = contentType;
        }
    }
}
