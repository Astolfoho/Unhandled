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
            var value = reader[pi.Name];

            if (value == DBNull.Value)
                return;

            if (pi.PropertyType == typeof(string))
            {
                pi.SetValue(instance, value.ToString(), null);
            }
            else if (pi.PropertyType == typeof(long) || pi.PropertyType == typeof(long?))
            {
                pi.SetValue(instance, (long)value, null);
            }
            else if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(int?))
            {
                pi.SetValue(instance, (int)value, null);
            }
            else if (pi.PropertyType == typeof(Guid) || pi.PropertyType == typeof(Guid?))
            {
                pi.SetValue(instance, Guid.Parse(value.ToString()), null);
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
