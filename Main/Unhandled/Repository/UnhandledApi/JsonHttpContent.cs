using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Unhandled.Repository.UnhandledApi
{
    internal class JsonHttpContent : HttpContent
    {
        public JsonHttpContent(object content)
        {
            this.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            using (var ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(content.GetType());
                serializer.WriteObject(ms, content);
                Lenght = ms.Length;
                ms.Seek(0, SeekOrigin.Begin);

                using(var sr = new StreamReader(ms))
                {
                    this.Content = sr.ReadToEnd();
                }

            }

            
        }

        public string Content { get; set; }
        public long Lenght { get; set; }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            using (var sw = new StreamWriter(stream))
            {
                sw.Write(this.Content);
            }
            var t = new Task<Object>(() => null);

            return t;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = Lenght;
            return true;
        }
    }
}