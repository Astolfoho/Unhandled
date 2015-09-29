using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhandledApi.Base.Models;

namespace UnhandledApi.Models
{
    [Table("Application", Schema = "unhandled")]
    public class Application : BaseModel
    {

        public string MachineName { get; set; }

        public string ApplicationName { get; set; }
    }
}
