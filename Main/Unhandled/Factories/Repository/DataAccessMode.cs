using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unhandled.Factories.Repository.Attributes;

namespace Unhandled.Factories.Repository
{

    public enum DataAccessMode
    {
        [ImplementationName("App_Data_Bin")]
        AppDataBin = 1,

        [ImplementationName("LocalSql")]
        LocalDb = 2
    }
}
