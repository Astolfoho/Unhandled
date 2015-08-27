using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Unhandled.Helpers;

namespace Unhandled.Models
{
    [Serializable]
    [DataContract]
    public class UnhandledError
    {
        public UnhandledError()
        {

        }

        public UnhandledError(Exception ex)
        {
            Id = Guid.NewGuid();
            Message = ex.Message;
            StackTrace = ex.StackTrace;
            Type = ex.GetType().Name;
            Source = ex.Source;
            
            var frame = new StackTrace(ex, true).GetFrame(0);
            FileName = frame.GetFileName();
            LineNumber = frame.GetFileLineNumber();
            SourceCode = ExceptionHelper.GetSourceFileLines(FileName, Encoding.Default, Source, LineNumber);
        }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "stackTrace")]
        public string StackTrace { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "source")]
        public string Source { get; set; }

        [DataMember(Name = "lineNumber")]
        public int LineNumber { get; set; }

        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        [DataMember(Name = "sourceCode")]
        public string SourceCode { get; set; }
    }
}
    