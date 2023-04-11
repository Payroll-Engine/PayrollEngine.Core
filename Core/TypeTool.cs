using System;
using System.Reflection;

namespace PayrollEngine
{
    /// <summary>Type tools</summary>
    public static class TypeTool
    {
        /// <summary>Get the public type properties</summary>
        /// <param name="type">The type</param>
        /// <returns>The public type properties</returns>
        public static PropertyInfo[] GetInstanceProperties(Type type) =>
            type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }
}
