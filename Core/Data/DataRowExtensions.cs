using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.System.Data.DataRowExtensions
/// <summary>Data row extension methods</summary>
public static class DataRowExtensions
{

    #region Row

    /// <param name="dataRow">The data row</param>
    extension(System.Data.DataRow dataRow)
    {
        /// <summary>Get data row id</summary>
        /// <returns>The data row id</returns>
        public int Id() => dataRow.GetValue<int>("Id");

        /// <summary>Get data row name</summary>
        /// <returns>The data row name</returns>
        public string Name() => dataRow.GetValue<string>("Name");

        /// <summary>Get data row identifier</summary>
        /// <returns>The data row identifier</returns>
        public string Identifier() => dataRow.GetValue<string>("Identifier");

        /// <summary>Get data row object status</summary>
        /// <returns>The data row object status</returns>
        public ObjectStatus ObjectStatus() => dataRow.GetEnumValue("Status", PayrollEngine.ObjectStatus.Inactive);
    }

    #endregion

    #region Values

    /// <param name="dataRow">The data row</param>
    extension(System.Data.DataRow dataRow)
    {
        /// <summary>Get data row enum value</summary>
        /// <param name="column">The column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data row enum value</returns>
        public T GetEnumValue<T>(string column, T defaultValue = default)
            where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new PayrollException($"Invalid enum value type: {typeof(T)}.");
            }
            var valueText = dataRow.GetValue(column, defaultValue.ToString());
            if (string.IsNullOrWhiteSpace(valueText) || !Enum.TryParse(valueText, true, out T enumValue))
            {
                return defaultValue;
            }
            return enumValue;
        }

        /// <summary>Get data row value</summary>
        /// <param name="column">The column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data row value</returns>
        public T GetValue<T>(string column, T defaultValue = default)
        {
            ArgumentNullException.ThrowIfNull(dataRow);
            ArgumentException.ThrowIfNullOrWhiteSpace(column);

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
                throw new PayrollException($"Error in column {column}: convert value {value} to type {typeof(T)}.", exception);
            }
        }

        /// <summary>Set data row value</summary>
        /// <remarks>Ensures the target column</remarks>
        /// <param name="column">The column name</param>
        /// <param name="value">The value to set</param>
        public void SetValue<T>(string column, T value) => dataRow.SetValue(column, value, typeof(T));

        /// <summary>Set data row value</summary>
        /// <remarks>Ensures the target column</remarks>
        /// <param name="column">The column name</param>
        /// <param name="value">The value to set</param>
        /// <param name="type">The value type</param>
        public void SetValue(string column, object value, Type type = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(column);

            type ??= typeof(string);
            dataRow.Table.EnsureColumn(column, type);
            dataRow[column] = value;
        }
    }

    #endregion

    #region Payroll Value

    /// <param name="dataRow">The data row</param>
    extension(System.Data.DataRow dataRow)
    {
        /// <summary>Get default payroll value type</summary>
        /// <returns>The payroll value tye</returns>
        public ValueType GetPayrollValueType() => 
            dataRow.GetPayrollValueType(nameof(ValueType));

        /// <summary>Get payroll value type</summary>
        /// <param name="column">The column name</param>
        /// <param name="defaultType">The default value type</param>
        /// <returns>The payroll value tye</returns>
        public ValueType GetPayrollValueType(string column, ValueType defaultType = ValueType.String) => 
            dataRow.GetEnumValue(column, defaultType);

        /// <summary>Get default payroll value</summary>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The payroll value</returns>
        public T GetPayrollValue<T>(T defaultValue = default) =>
            (T)dataRow.GetPayrollValue((object)defaultValue);

        /// <summary>Get payroll value</summary>
        /// <param name="valueColumn">The value column name</param>
        /// <param name="valueTypeColumn">The value type column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The payroll value</returns>
        public T GetPayrollValue<T>(string valueColumn, string valueTypeColumn, T defaultValue = default) =>
            (T)dataRow.GetPayrollValue(valueColumn, valueTypeColumn, (object)defaultValue);

        /// <summary>Get default payroll value</summary>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The payroll value</returns>
        public object GetPayrollValue(object defaultValue = null) =>
            dataRow.GetPayrollValue("Value", nameof(ValueType), defaultValue);

        /// <summary>Get payroll value</summary>
        /// <param name="valueColumn">The value column name</param>
        /// <param name="valueTypeColumn">The value type column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The payroll value</returns>
        public object GetPayrollValue(string valueColumn, string valueTypeColumn, object defaultValue = null) =>
            dataRow.GetJsonValue(valueColumn, dataRow.GetPayrollValueType(valueTypeColumn).GetSystemType(), defaultValue);
    }

    #endregion

    #region Collections

    /// <param name="dataRow">The data row</param>
    extension(System.Data.DataRow dataRow)
    {
        /// <summary>Get data row values as dictionary</summary>
        /// <returns>The data rows values as dictionary, key is the column name</returns>
        public Dictionary<string, object> AsDictionary()
        {
            ArgumentNullException.ThrowIfNull(dataRow);
            var values = new Dictionary<string, object>();
            foreach (System.Data.DataColumn column in dataRow.Table.Columns)
            {
                values.Add(column.ColumnName, dataRow.GetValue<object>(column.ColumnName));
            }
            return values;
        }

        /// <summary>Get data row as json</summary>
        /// <param name="namingPolicy">Naming policy (default: camel case)</param>
        /// <param name="ignoreNull">Ignore null values (default: true)</param>
        public string Json(JsonNamingPolicy namingPolicy = null,
            bool ignoreNull = true)
        {
            ArgumentNullException.ThrowIfNull(dataRow);
            return JsonSerializer.Serialize(dataRow.AsDictionary(), new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = namingPolicy ?? JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = namingPolicy ?? JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = ignoreNull ? JsonIgnoreCondition.WhenWritingNull : default
            });
        }
    }

    /// <summary>Get data rows value</summary>
    /// <param name="dataRows">The data rows</param>
    /// <param name="column">The column name</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The data rows value</returns>
    public static List<T> GetValues<T>(this IEnumerable<System.Data.DataRow> dataRows, string column, T defaultValue = default)
    {
        ArgumentNullException.ThrowIfNull(dataRows);
        ArgumentException.ThrowIfNullOrWhiteSpace(column);

        var values = new List<T>();
        foreach (System.Data.DataRow dataRow in dataRows)
        {
            values.Add(dataRow.GetValue(column, defaultValue));
        }
        return values;
    }

    /// <param name="dataRow">The data row</param>
    extension(System.Data.DataRow dataRow)
    {
        /// <summary>Get data row JSON value as list</summary>
        /// <param name="column">The column name</param>
        /// <returns>The list</returns>
        public List<T> GetListValue<T>(string column)
        {
            ArgumentNullException.ThrowIfNull(dataRow);
            ArgumentException.ThrowIfNullOrWhiteSpace(column);

            var value = dataRow[column];
            if (value is null or DBNull)
            {
                return [];
            }
            if (value is IEnumerable<T> enumerable)
            {
                return [.. enumerable];
            }
            if (value is string json)
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    return [];
                }
                return JsonSerializer.Deserialize<List<T>>(json);
            }

            throw new ArgumentException($"{value} from column {column} is not a JSON list.", nameof(column));
        }

        /// <summary>Get data row JSON value as dictionary</summary>
        /// <param name="column">The column name</param>
        /// <returns>Value as dictionary</returns>
        public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string column)
        {
            ArgumentNullException.ThrowIfNull(dataRow);
            ArgumentException.ThrowIfNullOrWhiteSpace(column);

            var value = dataRow[column];
            return value switch
            {
                null or DBNull => new(),
                IDictionary<TKey, TValue> dictionary => new(dictionary),
                string json => string.IsNullOrWhiteSpace(json)
                    ? new()
                    : JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(json),
                _ => throw new ArgumentException($"{value} from column {column} is not a JSON dictionary.", nameof(column))
            };
        }
    }

    #endregion

}