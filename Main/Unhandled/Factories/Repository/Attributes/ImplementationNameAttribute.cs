using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unhandled.Factories.Repository.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ImplementationNameAttribute : Attribute
    {
        public ImplementationNameAttribute(string implementationName)
        {
            ImplementationName = implementationName;
        }

        public string ImplementationName { get; set; }
    }
}
