using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PayrollEngine
{
    /// <summary>Type tools</summary>
    public static class TypeTool
    {
        /// <summary>Get the public instance properties</summary>
        /// <param name="type">The type</param>
        /// <returns>The public type properties</returns>
        public static List<PropertyInfo> GetInstanceProperties(Type type) =>
        type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

        /// <summary>Get the public type properties (instance and interfaces)</summary>
        /// <param name="type">The type</param>
        /// <returns>The public type properties</returns>
        public static List<PropertyInfo> GetTypeProperties(Type type)
        {
            var properties = GetInstanceProperties(type);
            foreach (var @interface in type.GetInterfaces())
            {
                properties.AddRange(@interface.GetProperties(BindingFlags.Public | BindingFlags.Instance));
            }
            return properties;
        }
    }
}
