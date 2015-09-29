using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Unhandled.Models
{
    [DataContract]
    public class Application
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "machineName")]
        public string MachineName { get; set; }

        [DataMember(Name = "applicationName")]
        public string ApplicationName { get; set; }
    }
}
