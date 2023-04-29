using System;
using System.Text.Json;

namespace PayrollEngine;

/// <summary>Json extensions for <see cref="string"/></summary>
public static class StringJsonExtensions
{
    /// <summary>Test if the string is a json array or object</summary>
    /// <param name="value">The string value</param>
    /// <returns>True if the string represents a json array or object</returns>
    public static bool IsJson(this string value) =>
        IsJsonArray(value) || IsJsonObject(value);

    /// <summary>Test if the string is a json array</summary>
    /// <param name="value">The string value</param>
    /// <returns>True if the string represents a json array</returns>
    public static bool IsJsonArray(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }
        var trimmedValue = value.Trim();
        return trimmedValue.StartsWith('[') && trimmedValue.EndsWith(']');
    }

    /// <summary>Test if the string is a json object</summary>
    /// <param name="value">The string value</param>
    /// <returns>True if the string represents a json object</returns>
    public static bool IsJsonObject(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }
        var trimmedValue = value.Trim();
        return trimmedValue.StartsWith('{') && trimmedValue.EndsWith('}');
    }

    /// <summary>Convert a string to case value</summary>
    /// <param name="value">The string to convert</param>
    /// <param name="valueType">Target type</param>
    /// <returns>The converted value</returns>
    public static object JsonToValue(this string value, ValueType valueType) =>
        ValueConvert.ToValue(value, valueType);

    /// <summary>Converts a json string to an integer value</summary>
    /// <param name="json">The json representation</param>
    /// <returns>The integer value</returns>
    public static int JsonToInteger(this string json) =>
        ValueConvert.ToInteger(json);

    /// <summary>Converts a json string to an decimal value</summary>
    /// <param name="json">The json representation</param>
    /// <returns>The decimal value</returns>
    public static decimal JsonToDecimal(this string json) =>
        ValueConvert.ToDecimal(json);

    /// <summary>Converts a json string to a string value</summary>
    /// <param name="json">The json representation</param>
    /// <returns>The string value</returns>
    public static string JsonToString(this string json) =>
        ValueConvert.ToString(json);

    /// <summary>Converts a json string to a date value</summary>
    /// <param name="json">The json representation</param>
    /// <returns>The date value</returns>
    public static DateTime JsonToDateTime(this string json) =>
        ValueConvert.ToDateTime(json);

    /// <summary>Converts a json string to a boolean value</summary>
    /// <param name="json">The json representation</param>
    /// <returns>The boolean value</returns>
    public static bool JsonToBoolean(this string json) =>
        ValueConvert.ToBoolean(json);

    /// <summary>Prettify json string</summary>
    /// <param name="json">The json representation</param>
    /// <returns>Indented json representation</returns>
    public static string JsonPrettify(this string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return json;
        }

        // slow approach
        using var jDoc = JsonDocument.Parse(json);
        return JsonSerializer.Serialize(jDoc, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}