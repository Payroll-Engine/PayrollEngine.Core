using System;
using System.Linq;
using System.Text.Json;

namespace PayrollEngine;

/// <summary>Json extensions</summary>
public static class JsonExtensions
{
    /// <summary>Gets the type of the system</summary>
    /// <param name="valueKind">Kind of the value</param>
    /// <returns>The system type</returns>
    public static Type GetSystemType(this JsonValueKind valueKind)
    {
        switch (valueKind)
        {
            case JsonValueKind.Object:
                return typeof(object);
            case JsonValueKind.Array:
                return typeof(Array);
            case JsonValueKind.String:
                return typeof(string);
            case JsonValueKind.Number:
                return typeof(int);
            case JsonValueKind.True:
            case JsonValueKind.False:
                return typeof(bool);
            default:
                return null;
        }
    }

    /// <summary>Gets the type of the system</summary>
    /// <param name="valueKind">Kind of the value</param>
    /// <param name="value">The value, used to determine the numeric type</param>
    /// <returns>The system type</returns>
    public static Type GetSystemType(this JsonValueKind valueKind, object value)
    {
        if (valueKind != JsonValueKind.Number || value == null)
        {
            return GetSystemType(valueKind);
        }

        // integral types
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
        if (value is sbyte)
        {
            return typeof(sbyte);
        }
        if (value is byte)
        {
            return typeof(byte);
        }
        if (value is short)
        {
            return typeof(short);
        }
        if (value is ushort)
        {
            return typeof(ushort);
        }
        if (value is int)
        {
            return typeof(int);
        }
        if (value is uint)
        {
            return typeof(uint);
        }
        if (value is long)
        {
            return typeof(long);
        }
        if (value is ulong)
        {
            return typeof(ulong);
        }

        // floating point types
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types
        if (value is float)
        {
            return typeof(float);
        }
        if (value is double)
        {
            return typeof(double);
        }
        if (value is decimal)
        {
            return typeof(decimal);
        }

        return typeof(int);
    }

    /// <summary>Get the json element value</summary>
    /// <param name="jsonElement">The json element</param>
    /// <returns>The json element value</returns>
    public static object GetValue(this JsonElement jsonElement)
    {
        switch (jsonElement.ValueKind)
        {
            case JsonValueKind.String:
                return jsonElement.GetString();
            case JsonValueKind.Number:
                return jsonElement.GetDecimal();
            case JsonValueKind.True:
            case JsonValueKind.False:
                return jsonElement.GetBoolean();
            case JsonValueKind.Array:
                // recursive values
                return jsonElement.EnumerateArray().
                    Select(GetValue).ToList();
            case JsonValueKind.Object:
                // recursive values
                return jsonElement.EnumerateObject().
                    ToDictionary(item => item.Name, item => GetValue(item.Value));
            case JsonValueKind.Undefined:
            case JsonValueKind.Null:
                return null;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}