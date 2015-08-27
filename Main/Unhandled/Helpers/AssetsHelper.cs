using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Unhandled.Helpers
{
    internal static class AssetsHelper
    {
        public static string GetEmbededResourceString(string resourceName, string resourceType)
        {
            var assembly = Assembly.Load("Unhandled");
            using (StreamReader stream =
                new StreamReader(assembly.GetManifestResourceStream(string.Concat("Unhandled.Assets.", resourceType, ".", resourceName, ".", resourceType))))
            {
                return stream.ReadToEnd();
            }
        }

        public static byte[] GetEmbededResourceBinary(string resourceName, string resourceType)
        {
            var assembly = Assembly.Load("Unhandled");
            using (Stream stream = assembly.GetManifestResourceStream(string.Concat("Unhandled.Assets.", resourceType, ".", resourceName, ".", resourceType)))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                return buffer;
            }
        }

        internal static string GetEmbededResourceSql(string resourceName)
        {
            return GetEmbededResourceString(resourceName, "sql");
        }
    }
}
