using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unhandled.Configuration;

namespace Unhandled.Repository.Data
{
    internal static class DbFactory
    {
        internal static IUnandledDatabase CreateConnection(string spName)
        {
            IUnandledDatabase instance = null;

            switch (UnhandledConfiguration.Current.ConnectionMode)
            {

                case ConnectionMode.SqlServer:
                    instance = new AppDataConnection(spName, UnhandledConfiguration.Current.ConnectionStringName);
                    break;
                case ConnectionMode.LocalSql:
                default:
                    instance = new AppDataConnection(spName);
                    break;
            }

            return instance;
        }

        internal static void InitDatabase()
        {
            switch (UnhandledConfiguration.Current.ConnectionMode)
            {
                case ConnectionMode.SqlServer:
                    break;
                case ConnectionMode.LocalSql:
                default:
                    AppDataConnection.Init();
                    break;
            }

            AppDataConnection.Init();
        }
    }
}
