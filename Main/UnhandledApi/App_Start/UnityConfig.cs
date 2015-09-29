using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Reflection;
using UnhandledApi.Repositories.Interfaces;

namespace UnhandledApi
{
    public class UnityConfig
    {

        internal static void Configure(UnityContainer container)
        {
            container.RegisterTypes
                        (FromTypes(),
                        FromMachingInterfaces,
                        WithName.Default,
                        WithLifetime.ContainerControlled);
        }

        private static IEnumerable<Type> FromTypes()
        {
            var np = "UnhandledApi.Repositories.SqlServer";
            IEnumerable<Type> types = Assembly.GetExecutingAssembly()
                                                .GetTypes()
                                                .Where(w => w.Namespace == np && typeof(IRepository).IsAssignableFrom(w));

            return types;
        }

        private static IEnumerable<Type> FromMachingInterfaces(Type arg)
        {
           return arg.GetInterfaces().Where(w => w.FullName != typeof(IRepository).FullName);
        }
    }
}
