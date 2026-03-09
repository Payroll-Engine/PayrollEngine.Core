using System;
using System.Text.Json;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.System.Data.DataRowExtensions
/// <summary>Data row json extension methods</summary>
public static class DataRowJsonExtensions
{
    /// <param name="dataRow">The data row</param>
    extension(System.Data.DataRow dataRow)
    {
        /// <summary>Get data row json value</summary>
        /// <param name="column">The column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data row value</returns>
        public T GetJsonValue<T>(string column, object defaultValue = null) =>
            (T)dataRow.GetJsonValue(column, typeof(T), defaultValue);

        /// <summary>Get data row json value</summary>
        /// <param name="column">The column name</param>
        /// <param name="type">The value type</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data row value</returns>
        public object GetJsonValue(string column,
            Type type, object defaultValue = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(column);
            ArgumentNullException.ThrowIfNull(type);

            if (dataRow[column] is not string json)
            {
                return defaultValue;
            }
            if (type == typeof(string) && !json.StartsWith('"'))
            {
                return json;
            }
            return string.IsNullOrWhiteSpace(json) ? defaultValue :
                JsonSerializer.Deserialize(json, type);
        }

        /// <summary>Set data row json value</summary>
        /// <param name="column">The column name</param>
        /// <param name="value">The value to set</param>
        public void SetJsonValue<T>(string column, T value) => dataRow.SetJsonValue(typeof(T), column, value);

        /// <summary>Set data row json value</summary>
        /// <param name="type">The value type</param>
        /// <param name="column">The column name</param>
        /// <param name="value">The value to set</param>
        public void SetJsonValue(Type type, string column, object value)
        {
            ArgumentNullException.ThrowIfNull(type);
            ArgumentException.ThrowIfNullOrWhiteSpace(column);
            if (value == null)
            {
                return;
            }
            dataRow[column] = JsonSerializer.Serialize(value);
        }
    }
}