using System.Text.Json;
using System;

namespace PayrollEngine.Data
{
    // duplicated in PayrollEngine.Client.Scripting.System.Data.DataRowExtensions
    /// <summary>Data row json extension methods</summary>
    public static class DataRowJsonExtensions
    {
        /// <summary>Get data row json value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data row value</returns>
        public static T GetJsonValue<T>(this System.Data.DataRow dataRow, string column, object defaultValue = null) =>
            (T)GetJsonValue(dataRow, column, typeof(T), defaultValue);

        /// <summary>Get data row json value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="type">The value type</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data row value</returns>
        public static object GetJsonValue(this System.Data.DataRow dataRow, string column,
            Type type, object defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentException(null, nameof(column));
            }
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
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="value">The value to set</param>
        public static void SetJsonValue<T>(this System.Data.DataRow dataRow, string column, T value) =>
            SetJsonValue(dataRow, typeof(T), column, value);

        /// <summary>Set data row json value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="type">The value type</param>
        /// <param name="column">The column name</param>
        /// <param name="value">The value to set</param>
        public static void SetJsonValue(this System.Data.DataRow dataRow, Type type, string column, object value)
        {
            ArgumentNullException.ThrowIfNull(type);
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentException(null, nameof(column));
            }
            if (value == null)
            {
                return;
            }
            dataRow[column] = JsonSerializer.Serialize(value);
        }
    }
}
