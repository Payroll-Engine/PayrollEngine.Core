using System.Collections.Generic;
using System.Text.Json;
using System;

namespace PayrollEngine.Data
{
    // duplicated in PayrollEngine.Client.Scripting.System.Data.DataRowExtensions
    /// <summary>Data row extension methods</summary>
    public static class DataRowExtensions
    {

        #region Row

        /// <summary>Get data row id</summary>
        /// <param name="dataRow">The data row</param>
        /// <returns>The data row id</returns>
        public static int GetRowId(this System.Data.DataRow dataRow) =>
            GetValue<int>(dataRow, "Id");

        /// <summary>Get data row name</summary>
        /// <param name="dataRow">The data row</param>
        /// <returns>The data row name</returns>
        public static string GetRowName(this System.Data.DataRow dataRow) =>
            GetValue<string>(dataRow, "Name");

        /// <summary>Get data row identifier</summary>
        /// <param name="dataRow">The data row</param>
        /// <returns>The data row identifier</returns>
        public static string GetRowIdentifier(this System.Data.DataRow dataRow) =>
            GetValue<string>(dataRow, "Identifier");

        /// <summary>Get data row object status</summary>
        /// <param name="dataRow">The data row</param>
        /// <returns>The data row object status</returns>
        public static ObjectStatus GetRowObjectStatus(this System.Data.DataRow dataRow) =>
            GetEnumValue(dataRow, "Status", ObjectStatus.Inactive);

        #endregion

        #region Values

        /// <summary>Get data row enum value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data row enum value</returns>
        public static T GetEnumValue<T>(this System.Data.DataRow dataRow, string column, T defaultValue = default)
            where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new PayrollException($"Invalid enum value type: {typeof(T)}");
            }
            var valueText = GetValue(dataRow, column, defaultValue.ToString());
            if (string.IsNullOrWhiteSpace(valueText) || !Enum.TryParse(valueText, true, out T enumValue))
            {
                return defaultValue;
            }
            return enumValue;
        }

        /// <summary>Get data row value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data row value</returns>
        public static T GetValue<T>(this System.Data.DataRow dataRow, string column, T defaultValue = default)
        {
            if (dataRow == null)
            {
                throw new ArgumentNullException(nameof(dataRow));
            }
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentException(nameof(column));
            }

            var value = dataRow[column];
            if (value is null or DBNull)
            {
                return defaultValue;
            }
            if (value is T typeValue)
            {
                return typeValue;
            }
            if (value is string stringValue)
            {
                // json escaping
                stringValue = stringValue.Trim('"');
                return (T)JsonSerializer.Deserialize(stringValue, typeof(T));
            }

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception exception)
            {
                throw new PayrollException($"Error in column {column}: convert value {value} to type {typeof(T)}", exception);
            }
        }

        /// <summary>Set data row value</summary>
        /// <remarks>Ensures the target column</remarks>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="value">The value to set</param>
        public static void SetValue<T>(this System.Data.DataRow dataRow, string column, T value) =>
            SetValue(dataRow, column, value, typeof(T));

        /// <summary>Set data row value</summary>
        /// <remarks>Ensures the target column</remarks>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="value">The value to set</param>
        /// <param name="type">The value type</param>
        public static void SetValue(this System.Data.DataRow dataRow, string column, object value, Type type = null)
        {
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentException(nameof(column));
            }

            type ??= typeof(string);
            dataRow.Table.EnsureColumn(column, type);
            dataRow[column] = value;
        }

        #endregion

        #region Payroll Value

        /// <summary>Get default payroll value type</summary>
        /// <param name="dataRow">The data row</param>
        /// <returns>The payroll value tye</returns>
        public static ValueType GetPayrollValueType(this System.Data.DataRow dataRow) =>
            GetPayrollValueType(dataRow, nameof(ValueType));

        /// <summary>Get payroll value type</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="defaultType">The default value type</param>
        /// <returns>The payroll value tye</returns>
        public static ValueType GetPayrollValueType(this System.Data.DataRow dataRow,
            string column, ValueType defaultType = ValueType.String) =>
            GetEnumValue(dataRow, column, defaultType);

        /// <summary>Get default payroll value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The payroll value</returns>
        public static T GetPayrollValue<T>(this System.Data.DataRow dataRow, T defaultValue = default) =>
            (T)GetPayrollValue(dataRow, (object)defaultValue);

        /// <summary>Get payroll value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="valueColumn">The value column name</param>
        /// <param name="valueTypeColumn">The value type column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The payroll value</returns>
        public static T GetPayrollValue<T>(this System.Data.DataRow dataRow,
            string valueColumn, string valueTypeColumn, T defaultValue = default) =>
            (T)GetPayrollValue(dataRow, valueColumn, valueTypeColumn, (object)defaultValue);

        /// <summary>Get default payroll value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The payroll value</returns>
        public static object GetPayrollValue(this System.Data.DataRow dataRow, object defaultValue = null) =>
            GetPayrollValue(dataRow, "Value", nameof(ValueType), defaultValue);

        /// <summary>Get payroll value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="valueColumn">The value column name</param>
        /// <param name="valueTypeColumn">The value type column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The payroll value</returns>
        public static object GetPayrollValue(this System.Data.DataRow dataRow,
            string valueColumn, string valueTypeColumn, object defaultValue = null) =>
            dataRow.GetJsonValue(valueColumn,
                GetPayrollValueType(dataRow, valueTypeColumn).GetDataType(), defaultValue);

        #endregion

        #region Collections

        /// <summary>Get data rows value</summary>
        /// <param name="dataRows">The data rows</param>
        /// <param name="column">The column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data rows value</returns>
        public static List<T> GetValues<T>(this IEnumerable<System.Data.DataRow> dataRows, string column, T defaultValue = default)
        {
            if (dataRows == null)
            {
                throw new ArgumentNullException(nameof(dataRows));
            }
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentException(nameof(column));
            }

            var values = new List<T>();
            foreach (System.Data.DataRow dataRow in dataRows)
            {
                values.Add(GetValue(dataRow, column, defaultValue));
            }
            return values;
        }

        /// <summary>Get data row JSON value as list</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <returns>The list</returns>
        public static List<T> GetListValue<T>(this System.Data.DataRow dataRow, string column)
        {
            if (dataRow == null)
            {
                throw new ArgumentNullException(nameof(dataRow));
            }
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentException(nameof(column));
            }

            var value = dataRow[column];
            if (value is null or DBNull)
            {
                return new();
            }
            if (value is IEnumerable<T> enumerable)
            {
                return new(enumerable);
            }
            if (value is string json)
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new();
                }
                return JsonSerializer.Deserialize<List<T>>(json);
            }

            throw new ArgumentException($"{value} from column {column} is not a JSON list", nameof(column));
        }

        /// <summary>Get data row JSON value as dictionary</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <returns>The dictionary</returns>
        public static Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(this System.Data.DataRow dataRow, string column)
        {
            if (dataRow == null)
            {
                throw new ArgumentNullException(nameof(dataRow));
            }
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentException(nameof(column));
            }

            var value = dataRow[column];
            return value switch
            {
                null or DBNull => new(),
                IDictionary<TKey, TValue> dictionary => new(dictionary),
                string json => string.IsNullOrWhiteSpace(json)
                    ? new()
                    : JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(json),
                _ => throw new ArgumentException($"{value} from column {column} is not a JSON dictionary", nameof(column))
            };
        }

        #endregion

    }
}
