using System;
using System.Globalization;
using System.Text.Json;

namespace PayrollEngine;

/// <summary>Convert a value</summary>
public static class ValueConvert
{
        
    /// <summary>Invert numeric json value</summary>
    /// <param name="json">The json to convert</param>
    /// <param name="valueType">The value type</param>
    /// <returns>Inverted json value</returns>
    public static string InvertValue(string json, ValueType valueType)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return json;
        }

        if (!TryToValue(json, valueType, out var value))
        {
            return null;
        }

        string outputValue;
        switch (valueType)
        {
            case ValueType.Integer:
                outputValue = ToJson((int)value * -1);
                break;
            case ValueType.NumericBoolean:
            case ValueType.Decimal:
            case ValueType.Money:
            case ValueType.Percent:
            case ValueType.Hour:
            case ValueType.Day:
            case ValueType.Week:
            case ValueType.Month:
            case ValueType.Year:
            case ValueType.Distance:
                outputValue = ToJson((decimal)value * -1);
                break;
            case ValueType.Boolean:
                outputValue = ToJson(!(bool)value);
                break;
            default:
                outputValue = json;
                break;
        }

        return outputValue;
    }

    #region Value to JSON string

    /// <summary>Try to convert value to JSON</summary>
    /// <param name="value">The value</param>
    /// <param name="json">The value as JSON text</param>
    /// <returns>True for a valid JSON conversion</returns>
    public static bool TryToJson(object value, out string json)
    {
        try
        {
            json = ToJson(value);
            return true;
        }
        catch
        {
            json = default;
            return false;
        }
    }

    /// <summary>Converts value to JSON</summary>
    /// <param name="value">The value</param>
    /// <returns>The JSON representation of the value</returns>
    public static string ToJson(object value) =>
        value != null ? JsonSerializer.Serialize(value) : null;

    #endregion

    #region JSON string to value

    /// <summary>Try to convert JSON string to value</summary>
    /// <param name="json">The JSON representation</param>
    /// <param name="valueType">The value type</param>
    /// <param name="value">The value</param>
    /// <returns>True for a valid JSON conversion</returns>
    public static bool TryToValue(string json, ValueType valueType, out object value)
    {
        try
        {
            value = ToValue(json, valueType);
            return true;
        }
        catch
        {
            value = default;
            return false;
        }
    }

    /// <summary>Convert JSON string to a number value</summary>
    /// <param name="json">The JSON representation</param>
    /// <param name="valueType">The value type</param>
    /// <returns>Numeric value or null</returns>
    public static decimal? ToNumber(string json, ValueType valueType)
    {
        if (!string.IsNullOrWhiteSpace(json) && valueType.IsNumber() &&
            TryToValue(json, ValueType.Decimal, out var value))
        {
            return (decimal)value;
        }
        return null;
    }

    /// <summary>Converts JSON string to value</summary>
    /// <param name="json">The JSON representation</param>
    /// <param name="valueType">The value type</param>
    /// <returns>The value</returns>
    public static object ToValue(string json, ValueType valueType)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        switch (valueType)
        {
            case ValueType.Integer:
                return ToInteger(json);

            case ValueType.NumericBoolean:
            case ValueType.Decimal:
            case ValueType.Money:
            case ValueType.Percent:
            case ValueType.Hour:
            case ValueType.Day:
            case ValueType.Week:
            case ValueType.Month:
            case ValueType.Year:
            case ValueType.Distance:
                return ToDecimal(json);

            case ValueType.String:
            case ValueType.WebResource:
                return ToString(json);

            case ValueType.Date:
            case ValueType.DateTime:
                return ToDateTime(json);

            case ValueType.Boolean:
                return ToBoolean(json);
            case ValueType.None:
                return null;
            default:
                throw new PayrollException($"unknown value type {valueType}");
        }
    }

    /// <summary>Converts a JSON string to an integer value</summary>
    /// <param name="json">The JSON representation</param>
    /// <returns>The integer value</returns>
    public static int ToInteger(string json) =>
        string.IsNullOrWhiteSpace(json) ? default : JsonSerializer.Deserialize<int>(json);

    /// <summary>Converts a JSON string to an decimal value</summary>
    /// <param name="json">The JSON representation</param>
    /// <returns>The decimal value</returns>
    public static decimal ToDecimal(string json) =>
        string.IsNullOrWhiteSpace(json) ? default : JsonSerializer.Deserialize<decimal>(json);

    /// <summary>Converts a JSON string to a string value</summary>
    /// <param name="json">The JSON representation</param>
    /// <returns>The string value</returns>
    public static string ToString(string json) =>
        string.IsNullOrWhiteSpace(json) ? default :
        json.StartsWith("\"") ? JsonSerializer.Deserialize<string>(json) : json;

    /// <summary>Converts a JSON string to a date time value</summary>
    /// <param name="json">The JSON representation</param>
    /// <returns>The date value</returns>
    public static DateTime ToDateTime(string json) =>
        string.IsNullOrWhiteSpace(json) ? default :
        json.StartsWith("\"") ? JsonSerializer.Deserialize<DateTime>(json) : DateTime.Parse(json, null, DateTimeStyles.AdjustToUniversal);

    /// <summary>Converts a JSON string to a boolean value</summary>
    /// <param name="json">The JSON representation</param>
    /// <returns>The boolean value</returns>
    public static bool ToBoolean(string json) =>
        !string.IsNullOrWhiteSpace(json) && JsonSerializer.Deserialize<bool>(json);

    #endregion

}