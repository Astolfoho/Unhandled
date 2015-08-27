using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Unhandled.Repository.Data
{
    internal interface IUnandledDatabase : IDisposable
    {
        IDataReader ExecuteReader();
        int ExecuteNonQuery();
        T ExecuteScalar<T>();
        void EnsureParameter(object Value);
        void EnsureParameter(string Name, object Value);  
    }
}
