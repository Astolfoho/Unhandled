using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Unhandled.Repository.UnhandledApi
{
    internal class JsonHttpContent<T> : HttpContent
    {
        public JsonHttpContent(T data)
        {
            this.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            Data = Data;
        }

        public T Data { get; set; }
        public long DataLength { get; set; }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return new Task(() =>
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(Data.GetType());
                serializer.WriteObject(stream, Data);
                DataLength = stream.Length;
            });   
        }

        protected override bool TryComputeLength(out long length)
        {
            length = DataLength;
            return true;
        }
    }
}