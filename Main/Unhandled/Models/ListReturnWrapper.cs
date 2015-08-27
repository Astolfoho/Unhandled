using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Unhandled.Models
{
    [DataContract]
    [Serializable]
    public class ListReturnWrapper<TData>
    {
        public ListReturnWrapper(TData data)
        {
            this.Data = data;
        }

        [DataMember(Name = "d")]
        public TData Data { get; private set; }

    }
}
