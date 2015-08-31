using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Unhandled.Helpers;
using Unhandled.Repository.Attributes;
namespace Unhandled.Repository.Data
{
    internal class AppDataConnection : IUnandledDatabase
    {

        private SqlConnection _conn;
        private SqlCommand _command;

        static AppDataConnection(){
            
        }

        public AppDataConnection(string spName)
        {
            if (_conn == null || _conn.State == ConnectionState.Closed)
            {
                _conn = LocalDBHelper.GetLocalDB(LocalDBHelper.DB_DEFAULT_NAME, false);
            }
            _command = _conn.CreateCommand();
            _command.CommandText = spName;
            _command.CommandType = CommandType.StoredProcedure;
        }

        internal static void Init()
        {
                bool exists = false;
                SqlConnection conn = LocalDBHelper.GetLocalDB(LocalDBHelper.DB_DEFAULT_NAME, out exists, false);
                if (!exists)
                {            
                    var initCommands = AssetsHelper.GetEmbededResourceSql("initDatabase");

                    foreach (var initCommand in initCommands.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var command = conn.CreateCommand();
                        command.CommandText = initCommand;
                        command.ExecuteNonQuery();
                    }

                    
                }
                conn.Close();
        }

        public void EnsureParameter(string name, object value)
        {
            var par = _command.CreateParameter();

            par.ParameterName = "@"+name;
            par.Value = value;

            if (par.Value == null || par.Value.Equals(DateTime.MinValue))
            {
                par.Value = DBNull.Value;
            }
            else
            {
                var valueType = value.GetType();

                if (valueType == typeof(string))
                {
                    par.SqlDbType = SqlDbType.VarChar;
                }
                else if (valueType == typeof(long))
                {
                    par.SqlDbType = SqlDbType.BigInt;
                }
                else if (valueType == typeof(int))
                {
                    par.SqlDbType = SqlDbType.Int;
                }
                else if (valueType == typeof(Guid))
                {
                    par.SqlDbType = SqlDbType.UniqueIdentifier;
                }
            }
            _command.Parameters.Add(par);
        }

        public void EnsureParameter(object value)
        {
            var properties = value.GetType().GetProperties();
            foreach (var prop in properties.Where(w => !w.GetCustomAttributes(typeof(DbIgnore), true).Any()))
            {
                var val = prop.GetValue(value, null);
                var name = prop.Name;
                this.EnsureParameter(name, val);
            }
        }

        public System.Data.IDataReader ExecuteReader()
        {
            return _command.ExecuteReader();
        }

        public int ExecuteNonQuery()
        {
            return _command.ExecuteNonQuery();
        }

        public T ExecuteScalar<T>()
        {
            return (T)_command.ExecuteScalar();
        }

        public void Dispose()
        {
            if (_conn.State != ConnectionState.Closed)
            {
                _conn.Close();
            }
        }
    }
}
