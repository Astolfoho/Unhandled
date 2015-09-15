using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Unhandled.Configuration
{
    public class UnhandledConfiguration : ConfigurationSection
    {

        private static Lazy<UnhandledConfiguration> _current = 
            new Lazy<UnhandledConfiguration>(_loadCurrentCofig);

        public static UnhandledConfiguration Current { get { return _loadCurrentCofig(); } }

        private static UnhandledConfiguration _loadCurrentCofig()
        {
            return (UnhandledConfiguration)ConfigurationManager.GetSection("UnhandledConfiguration");
        }



        [ConfigurationProperty("connectionMode", DefaultValue = ConnectionMode.LocalSql)]
        public ConnectionMode ConnectionMode
        {
            get
            {
                return (ConnectionMode)this["connectionMode"];
            }
            set
            {
                this["connectionMode"] = value;
            }
        }

        [ConfigurationProperty("connectionStringName")]
        public string ConnectionStringName
        {
            get
            {
                return (string)this["connectionStringName"];
            }
            set
            {
                this["connectionString"] = value;
            }
        }
    }

    public enum ConnectionMode
    {
        LocalSql = 0,
        SqlServer = 1
    }
}
