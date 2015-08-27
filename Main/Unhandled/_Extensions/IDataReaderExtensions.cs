using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Unhandled.Repository.Attributes;

namespace System
{
    internal static class IDataReaderExtensions
    {
        internal static void MapProperty(IDataReader reader, object instance, PropertyInfo pi)
        {

            if (pi.PropertyType == typeof(string))
            {
                pi.SetValue(instance, reader[pi.Name].ToString(), null);
            }
            else if (pi.PropertyType == typeof(int))
            {
                pi.SetValue(instance, (int)reader[pi.Name], null);
            }
            else if (pi.PropertyType == typeof(Guid))
            {
                pi.SetValue(instance, Guid.Parse(reader[pi.Name].ToString()), null);
            }
        }

        internal static T MapObject_internal<T>(this IDataReader reader) where T : class, new()
        {
            T instance = new T();
            foreach (var pi in typeof(T).GetProperties().Where(w => !w.GetCustomAttributes(typeof(DbIgnore), true).Any()))
            {
                MapProperty(reader, instance, pi);
            }

            return instance;
        }

        public static T MapObject<T>(this IDataReader reader) where T : class, new()
        {
            reader.Read();
            return MapObject_internal<T>(reader);
        }

        public static List<T> MapList<T>(this IDataReader reader) where T : class, new()
        {
            List<T> ret = new List<T>();

            while (reader.Read())
            {
                ret.Add(MapObject_internal<T>(reader));
            }

            return ret;
        }
    }
}
