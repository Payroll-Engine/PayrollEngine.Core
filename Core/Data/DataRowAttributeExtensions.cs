using System;
using System.Text.Json;
using System.Collections.Generic;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.System.Data.DataRowExtensions
/// <summary>Data row attribute extension methods</summary>
public static class DataRowAttributeExtensions
{
    /// <param name="dataRow">The data row</param>
    extension(System.Data.DataRow dataRow)
    {
        /// <summary>Get attributes column value as attribute dictionary</summary>
        /// <returns>The attributes dictionary</returns>
        public Dictionary<string, object> GetAttributes() =>
            dataRow.GetAttributes(nameof(IAttributeObject.Attributes));

        /// <summary>Get data row json value as attribute dictionary</summary>
        /// <param name="column">The column name</param>
        /// <returns>The attributes dictionary</returns>
        public Dictionary<string, object> GetAttributes(string column) =>
            dataRow.GetDictionary<string, object>(column);

        /// <summary>Get value from attributes column</summary>
        /// <param name="attribute">The attribute name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The attribute value</returns>
        public T GetAttribute<T>(string attribute, T defaultValue = default) =>
            dataRow.GetAttribute(nameof(IAttributeObject.Attributes), attribute, defaultValue);

        /// <summary>Get attribute from a data row json value</summary>
        /// <param name="column">The column name</param>
        /// <param name="attribute">The attribute name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The attribute value</returns>
        public T GetAttribute<T>(string column, string attribute, T defaultValue = default) =>
            (T)Convert.ChangeType(dataRow.GetAttribute(column, attribute, (object)defaultValue), typeof(T));

        /// <summary>Get value from attributes column</summary>
        /// <param name="attribute">The attribute name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The attribute value</returns>
        public object GetAttribute(string attribute, object defaultValue = null) => 
            dataRow.GetAttribute(nameof(IAttributeObject.Attributes), attribute, defaultValue);

        /// <summary>Get attribute from a data row json value</summary>
        /// <param name="column">The column name</param>
        /// <param name="attribute">The attribute name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The attribute value</returns>
        public object GetAttribute(string column, string attribute, object defaultValue = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(attribute);

            var attributes = dataRow.GetAttributes(column);
            if (!attributes.TryGetValue(attribute, out var value))
            {
                return defaultValue;
            }

            if (value is JsonElement jsonElement)
            {
                value = jsonElement.GetValue();
            }
            return value ?? defaultValue;
        }
    }
}