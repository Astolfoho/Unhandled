using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace Unhandled.Helpers
{
    internal static class ExceptionHelper 
    {
        //protected string _virtualPath;
        //protected string _physicalPath;
        //protected string _sourceCode;
        //protected int _line;

        // Number of lines before and after the error lines included in the report
        private const int errorRange = 2;

        /*
         * Return the text of the error line in the source file, with a few
         * lines around it.  It is returned in HTML format.
         */
        public static string GetSourceFileLines(string fileName, Encoding encoding, string sourceCode, int lineNumber)
        {

            // Don't show any source file if the user doesn't have access to it (ASURT 122430)
            //if (fileName != null) // todo review this 
            //{
            //    return "No relevante source code"; //todo add resource
            //}

            if (lineNumber <= 0)
            {
                return "No relevante source code"; //todo add resource
            }

            StringBuilder sb = new StringBuilder();
            TextReader reader = null;

            // Check if it's an http line pragma, from which we can get a VirtualPath
            string virtualPath = GetVirtualPathFromHttpLinePragma(fileName);

            // If we got a virtual path, open a TextReader from it
            if (virtualPath != null)
            {
                Stream stream = VirtualPathProvider.OpenFile(virtualPath);
                if (stream != null)
                    reader = ReaderFromStream(stream, System.Web.VirtualPath.Create(virtualPath));
            }

            try
            {
                // Otherwise, open the physical file
                if (reader == null && fileName != null)
                    reader = new StreamReader(fileName, encoding, true, 4096);
            }
            catch { }

            if (reader == null)
            {
                if (sourceCode == null)
                    return "SR.GetString(SR.WithFile_No_Relevant_Line)"; //todo;

                // Can't open the file?  Use the dynamically generated content...
                reader = new StringReader(sourceCode);
            }

            try
            {
                bool fFoundLine = false;

                for (int i = 1; ; i++)
                {
                    // Get the current line from the source file
                    string sourceLine = reader.ReadLine();
                    if (sourceLine == null)
                        break;

                    // If it's the error line, make it red
                    if (i == lineNumber)
                        sb.Append("<font color=red>");

                    // Is it in the range we want to display
                    if (i >= lineNumber - errorRange && i <= lineNumber + errorRange)
                    {
                        fFoundLine = true;
                        String linestr = i.ToString("G", CultureInfo.CurrentCulture);

                        sb.Append(linestr); //todo
                        if (linestr.Length < 3)
                            sb.Append(' ', 3 - linestr.Length);
                        sb.Append(HttpUtility.HtmlEncode(sourceLine));

                        if (i != lineNumber + errorRange)
                            sb.Append("\r\n");
                    }

                    if (i == lineNumber)
                        sb.Append("</font>");

                    if (i > lineNumber + errorRange)
                        break;
                }

                if (!fFoundLine)
                    return "SR.GetString(SR.WithFile_No_Relevant_Line)"; //todo
            }
            finally
            {
                // Make sure we always close the reader
                reader.Close();
            }

            return sb.ToString();
        }

        internal static string GetVirtualPathFromHttpLinePragma(string linePragma)
        {

            if (String.IsNullOrEmpty(linePragma))
                return null;

            try
            {
                Uri uri = new Uri(linePragma);
                if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
                    return uri.LocalPath;
            }
            catch { }

            return null;
        }

        //private string GetSourceFileLines()
        //{
        //    return GetSourceFileLines(_physicalPath, SourceFileEncoding, _sourceCode, _line);
        //}

        //internal void FormatterWithFileInfo(string virtualPath, string physicalPath,
        //    string sourceCode, int line)
        //{

        //    _virtualPath = virtualPath;
        //    _physicalPath = physicalPath;

        //    if (sourceCode == null && _physicalPath == null && _virtualPath != null)
        //    {

        //        // Make sure _virtualPath is really a virtual path.  Sometimes,
        //        // it can actually be a physical path, in which case we keep
        //        // it as is.
        //        if (UrlPath.IsValidVirtualPathWithoutProtocol(_virtualPath))
        //            _physicalPath = HostingEnvironment.MapPath(_virtualPath);
        //        else
        //            _physicalPath = _virtualPath;
        //    }

        //    _sourceCode = sourceCode;
        //    _line = line;
        //}

                /*
         * Return a reader which holds the contents of a file.  If a configPath is passed
         * in, try to get a encoding for it
         */
        private static StreamReader ReaderFromStream(Stream stream, VirtualPath vp)
        {

            // Check if a file encoding is specified in the config
            Encoding fileEncoding = GetEncoding(vp);

            // Create a reader on the file, using the encoding
            return new StreamReader(stream, fileEncoding,
                true /*detectEncodingFromByteOrderMarks*/, 4096);
        }

        private static Encoding GetEncoding(VirtualPath vp)
        {

            var config = ConfigurationManager.OpenExeConfiguration(vp.AppRelativeVirtualPathString);

            var glob = (GlobalizationSection)ConfigurationManager.GetSection("Globalization");
            


            // Check if a file encoding is specified in the config
            Encoding fileEncoding = null;
            fileEncoding = glob.FileEncoding;

            // If not, use the default encoding
            if (fileEncoding == null)
                fileEncoding = Encoding.Default;

            return fileEncoding;
        }


        //private static Lazy<GlobalizationSection> _lazyGlobalization = new Lazy<GlobalizationSection>(loadGlobalization);
        //private static GlobalizationSection Globalization { get { return _lazyGlobalization.Value; } }



        //private static GlobalizationSection loadGlobalization()
        //{
        //    return (GlobalizationSection)ConfigurationManager.GetSection("Globalization");
        //}
    
        //public static bool IsTextRightToLeft { get; set; }
        //public static char[] BeginLeftToRightTag { get; set; }
    }
}
