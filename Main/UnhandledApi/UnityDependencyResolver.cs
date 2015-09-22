using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UnhandledApi
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private UnityContainer _container;

        public UnityDependencyResolver(UnityContainer container)
        {
            this._container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

    }
}
