using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unhandled.Repository.Data
{
    internal static class DbFactory
    {
        internal static IUnandledDatabase CreateConnection(string spName)
        {
            return new AppDataConnection(spName);
        }

        internal static void InitDatabase()
        {
            AppDataConnection.Init();
        }
    }
}
