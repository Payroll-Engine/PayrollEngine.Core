using System.Collections.Generic;
using System.Text.Json;
using System;

namespace PayrollEngine.Data
{
    // duplicated in PayrollEngine.Client.Scripting.System.Data.DataRowExtensions
    /// <summary>Data row attribute extension methods</summary>
    public static class DataRowAttributeExtensions
    {
        /// <summary>Get attributes column value as attribute dictionary</summary>
        /// <param name="dataRow">The data row</param>
        /// <returns>The attributes dictionary</returns>
        public static Dictionary<string, object> GetAttributes(this System.Data.DataRow dataRow) =>
            GetAttributes(dataRow, nameof(IAttributeObject.Attributes));

        /// <summary>Get data row json value as attribute dictionary</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <returns>The attributes dictionary</returns>
        public static Dictionary<string, object> GetAttributes(this System.Data.DataRow dataRow, string column) =>
            dataRow.GetDictionary<string, object>(column);

        /// <summary>Get value from attributes column</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="attribute">The attribute name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The attribute value</returns>
        public static T GetAttribute<T>(this System.Data.DataRow dataRow, string attribute, T defaultValue = default) =>
            GetAttribute(dataRow, nameof(IAttributeObject.Attributes), attribute, defaultValue);

        /// <summary>Get attribute from a data row json value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="attribute">The attribute name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The attribute value</returns>
        public static T GetAttribute<T>(this System.Data.DataRow dataRow, string column, string attribute, T defaultValue = default) =>
            (T)Convert.ChangeType(GetAttribute(dataRow, column, attribute, (object)defaultValue), typeof(T));

        /// <summary>Get value from attributes column</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="attribute">The attribute name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The attribute value</returns>
        public static object GetAttribute(this System.Data.DataRow dataRow, string attribute, object defaultValue = default) =>
            GetAttribute(dataRow, nameof(IAttributeObject.Attributes), attribute, defaultValue);

        /// <summary>Get attribute from a data row json value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="attribute">The attribute name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The attribute value</returns>
        public static object GetAttribute(this System.Data.DataRow dataRow, string column, string attribute, object defaultValue = default)
        {
            if (string.IsNullOrWhiteSpace(attribute))
            {
                throw new ArgumentException(nameof(attribute));
            }

            var attributes = GetAttributes(dataRow, column);
            if (!attributes.ContainsKey(attribute))
            {
                return defaultValue;
            }

            var value = attributes[attribute];
            if (value is JsonElement jsonElement)
            {
                value = jsonElement.GetValue();
            }
            return value ?? defaultValue;
        }

    }
}
