using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Unhandled.Factories.Repository.Attributes;

namespace Unhandled.Factories.Repository
{
    internal class RepositoryFactory
    {
        #region Singleton
        private static Lazy<RepositoryFactory> _instance = new Lazy<RepositoryFactory>(() => new RepositoryFactory());
        public static RepositoryFactory Instance { get { return _instance.Value; } }
        #endregion

        #region Constructors
        private RepositoryFactory()
        {

        }
        #endregion


        private DataAccessMode _accessMode = DataAccessMode.LocalDb;

        private const string NAMESPACE_FORMAT = "Unhandled.Repository.{0}";
        private string _currentNamespace;


        public T CreateInstance<T>() where T:class {
            var np = GetNamespace();
            var assembly = Assembly.GetExecutingAssembly();
            var typeofT = typeof(T);
            var typeRet = assembly.GetTypes().Where(w => w.Namespace == np && typeofT.IsAssignableFrom(w)).First();
            return (T)Activator.CreateInstance(typeRet);
        }

        private string GetNamespace()
        {
            if (string.IsNullOrWhiteSpace(_currentNamespace))
            {
                var type = _accessMode.GetType();
                var memInfo = type.GetMember(_accessMode.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(ImplementationNameAttribute), false);
                var impleName = ((ImplementationNameAttribute)attributes[0]).ImplementationName;
                _currentNamespace = string.Format(NAMESPACE_FORMAT, impleName);
            }
            return _currentNamespace;
        }

    }
}
