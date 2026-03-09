using System;
using System.Globalization;
using System.Text.Json;

namespace PayrollEngine;

/// <summary>Json extensions for <see cref="string"/></summary>
public static class StringJsonExtensions
{
    /// <param name="value">The string value</param>
    extension(string value)
    {
        /// <summary>Test if the string is a json array or object</summary>
        /// <returns>True if the string represents a json array or object</returns>
        public bool IsJson() => value.IsJsonArray() || value.IsJsonObject();

        /// <summary>Test if the string is a json array</summary>
        /// <returns>True if the string represents a json array</returns>
        public bool IsJsonArray()
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            var trimmedValue = value.Trim();
            return trimmedValue.StartsWith('[') && trimmedValue.EndsWith(']');
        }

        /// <summary>Test if the string is a json object</summary>
        /// <returns>True if the string represents a json object</returns>
        public bool IsJsonObject()
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            var trimmedValue = value.Trim();
            return trimmedValue.StartsWith('{') && trimmedValue.EndsWith('}');
        }

        /// <summary>Convert a string to case value</summary>
        /// <param name="valueType">Target type</param>
        /// <param name="culture">The culture</param>
        /// <returns>The converted value</returns>
        public object JsonToValue(ValueType valueType, CultureInfo culture) =>
            ValueConvert.ToValue(value, valueType, culture);

        /// <summary>Converts a json string to an integer value</summary>
        /// <returns>The integer value</returns>
        public int JsonToInteger() =>
            ValueConvert.ToInteger(value);

        /// <summary>Converts a json string to an decimal value</summary>
        /// <returns>The decimal value</returns>
        public decimal JsonToDecimal() =>
            ValueConvert.ToDecimal(value);

        /// <summary>Converts a json string to a string value</summary>
        /// <returns>The string value</returns>
        public string JsonToString() =>
            ValueConvert.ToString(value);

        /// <summary>Converts a json string to a date value</summary>
        /// <param name="culture">The culture</param>
        /// <returns>The date value</returns>
        public DateTime JsonToDateTime(CultureInfo culture) =>
            ValueConvert.ToDateTime(value, culture);

        /// <summary>Converts a json string to a boolean value</summary>
        /// <returns>The boolean value</returns>
        public bool JsonToBoolean() =>
            ValueConvert.ToBoolean(value);

        /// <summary>Prettify json string</summary>
        /// <returns>Indented json representation</returns>
        public string JsonPrettify()
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            // slow approach
            using var jDoc = JsonDocument.Parse(value);
            return JsonSerializer.Serialize(jDoc, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}