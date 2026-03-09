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
    /// <param name="culture">The culture</param>
    /// <returns>Inverted json value</returns>
    public static string InvertValue(string json, ValueType valueType, CultureInfo culture)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return json;
        }

        if (!TryToValue(json, valueType, culture, out var value))
        {
            return null;
        }

        return valueType.GetSystemType().Name switch
        {
            nameof(Int32) => ToJson((int)value * -1),
            nameof(Decimal) => ToJson((decimal)value * -1),
            nameof(Boolean) => ToJson(!(bool)value),
            _ => json
        };
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
            json = null;
            return false;
        }
    }

    /// <summary>Converts value to JSON</summary>
    /// <param name="value">The value</param>
    /// <returns>The JSON representation of the value</returns>
    public static string ToJson(object value) =>
        value != null ? JsonSerializer.Serialize(value, value.GetType()) : null;

    #endregion

    #region JSON string to value

    /// <summary>Try to convert JSON string to value</summary>
    /// <param name="json">The JSON representation</param>
    /// <param name="valueType">The value type</param>
    /// <param name="culture">The culture</param>
    /// <param name="value">The value</param>
    /// <returns>True for a valid JSON conversion</returns>
    public static bool TryToValue(string json, ValueType valueType, CultureInfo culture, out object value)
    {
        try
        {
            value = ToValue(json, valueType, culture);
            return true;
        }
        catch
        {
            value = null;
            return false;
        }
    }

    /// <summary>Convert JSON string to a number value</summary>
    /// <param name="json">The JSON representation</param>
    /// <param name="valueType">The value type</param>
    /// <param name="culture">The culture</param>
    /// <returns>Numeric value or null</returns>
    public static decimal? ToNumber(string json, ValueType valueType, CultureInfo culture)
    {
        if (!string.IsNullOrWhiteSpace(json) && valueType.IsNumber() &&
            TryToValue(json, ValueType.Decimal, culture, out var value))
        {
            return (decimal)value;
        }
        return null;
    }

    /// <summary>Converts JSON string to value</summary>
    /// <param name="json">The JSON representation</param>
    /// <param name="valueType">The value type</param>
    /// <param name="culture">The date culture</param>
    /// <returns>The value</returns>
    public static object ToValue(string json, ValueType valueType, CultureInfo culture)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        return valueType.GetSystemType().Name switch
        {
            nameof(Int32) => ToInteger(json),
            nameof(Decimal) => ToDecimal(json),
            nameof(String) => ToString(json),
            nameof(DateTime) => ToDateTime(json, culture),
            nameof(Boolean) => ToBoolean(json),
            _ => null
        };
    }

    /// <summary>Converts a JSON string to an integer value</summary>
    /// <param name="json">The JSON representation</param>
    /// <returns>The integer value</returns>
    public static int ToInteger(string json)
    {
        return string.IsNullOrWhiteSpace(json) ? 0 : JsonSerializer.Deserialize<int>(json);
    }

    /// <summary>Converts a JSON string to an decimal value</summary>
    /// <param name="json">The JSON representation</param>
    /// <returns>The decimal value</returns>
    public static decimal ToDecimal(string json) =>
        string.IsNullOrWhiteSpace(json) ? 0 : JsonSerializer.Deserialize<decimal>(json);

    /// <summary>Converts a JSON string to a string value</summary>
    /// <param name="json">The JSON representation</param>
    /// <returns>The string value</returns>
    public static string ToString(string json) =>
        string.IsNullOrWhiteSpace(json) ? null :
        json.StartsWith('"') ? JsonSerializer.Deserialize<string>(json) : json;

    /// <summary>Converts a JSON string to a date time value</summary>
    /// <param name="json">The JSON representation</param>
    /// <param name="culture">The culture</param>
    /// <returns>The date value</returns>
    public static DateTime ToDateTime(string json, CultureInfo culture) =>
        string.IsNullOrWhiteSpace(json) ? default :
        json.StartsWith('"') ?
            JsonSerializer.Deserialize<DateTime>(json) :
            DateTime.Parse(json, culture, DateTimeStyles.AdjustToUniversal);

    /// <summary>Converts a JSON string to a boolean value</summary>
    /// <param name="json">The JSON representation</param>
    /// <returns>The boolean value</returns>
    public static bool ToBoolean(string json) =>
        !string.IsNullOrWhiteSpace(json) && JsonSerializer.Deserialize<bool>(json);

    #endregion

}