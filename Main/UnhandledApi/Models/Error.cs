using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhandledApi.Base.Models;

namespace UnhandledApi.Models
{
    [Table("Error", Schema = "unhandled")]
    public class Error : BaseModel
    {
       
        public Error()
        {

        }

        public Error(Error err, long childErrorId)
        {
            this.Message = err.Message;
            this.StackTrace = err.StackTrace;
            this.Type = err.Type;
            this.Source = err.Source;
            this.LineNumber = err.LineNumber;
            this.FileName = err.FileName;
            this.SourceCode = err.SourceCode;
            this.ParentErrorId = err.ParentErrorId;
            this.ApplicationId = err.ApplicationId;
            this.ChildError = childErrorId;
    }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string Type { get; set; }

        public string Source { get; set; }

        public int LineNumber { get; set; }

        public string FileName { get; set; }

        public string SourceCode { get; set; }

        public long? ParentErrorId { get; set; }

        public long ApplicationId { get; set; }

        [NotMapped]
        public long ChildError { get; private set; }
    }
}
