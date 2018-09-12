using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Geo.Localization.Data.Utils
{
    internal class Mapper
    {
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            var list = new List<T>();
            var obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (var prop in obj.GetType().GetProperties())
                    if (IsSimpleType(prop.PropertyType))
                    {
                        if (!Equals(dr[prop.Name], DBNull.Value))
                            prop.SetValue(obj, dr[prop.Name], null);
                    }
                    else
                    {
                        if (prop.PropertyType.IsClass)
                        {
                            var instance = Activator.CreateInstance(prop.PropertyType);
                            prop.SetValue(obj, instance, null);
                            //Type t = typeof(instance.GetType().BaseType);
                        }
                    }
                list.Add(obj);
            }
            return list;
        }


        public static bool IsSimpleType(Type type)
        {
            return
                type.IsPrimitive ||
                new[]
                {
                    typeof(Enum),
                    typeof(string),
                    typeof(char),
                    typeof(Guid),

                    typeof(bool),
                    typeof(byte),
                    typeof(short),
                    typeof(int),
                    typeof(long),
                    typeof(float),
                    typeof(double),
                    typeof(decimal),

                    typeof(sbyte),
                    typeof(ushort),
                    typeof(uint),
                    typeof(ulong),

                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                IsSimpleType(type.GetGenericArguments()[0])
                ;
        }

        private static void callCalss()
        {
            var name = "Foo";
            var property = "Bar";
            var value = "Baz";

            // Get the type contained in the name string
            var type = Type.GetType(name, true);

            // create an instance of that type
            var instance = Activator.CreateInstance(type);

            // Get a property on the type that is stored in the 
            // property string
            var prop = type.GetProperty(property);

            // Set the value of the given property on the given instance
            prop.SetValue(instance, value, null);

            // at this stage instance.Bar will equal to the value
            Console.WriteLine(((Foo) instance).Bar);
        }


        public class Foo
        {
            public string Bar { get; set; }
        }
    }
}