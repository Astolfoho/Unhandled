using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Unhandled.Models
{
    [Serializable]
    [DataContract]
    public class Cookie
    {

        public Cookie()
        {

        }

        public Cookie(System.Web.HttpCookie httpCookie)
        {
            this.Name = httpCookie.Name;
            this.Path = httpCookie.Path;
            this.Expires = httpCookie.Expires;
            this.Domain = httpCookie.Domain;
            this.Secure = httpCookie.Secure;
            this.Value = httpCookie.Value;

        }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "errorId")]
        public long ErrorId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "expires")]
        public DateTime Expires { get; set; }

        [DataMember(Name = "domain")]
        public string Domain { get; set; }

        [DataMember(Name = "secure")]
        public bool Secure { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        
    }
}
