using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="ValueType"/></summary>
public static class ValueTypeExtensions
{

    /// <summary>Get the data system type</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>The data type</returns>
    public static Type GetSystemType(this ValueType valueType)
    {
        if (valueType.IsString())
        {
            return typeof(string);
        }
        if (valueType.IsDateTime())
        {
            return typeof(DateTime);
        }
        if (valueType.IsInteger())
        {
            return typeof(int);
        }
        if (valueType.IsDecimal())
        {
            return typeof(decimal);
        }
        if (valueType.IsBoolean())
        {
            return typeof(bool);
        }
        if (valueType == ValueType.None)
        {
            return typeof(DBNull);
        }
        throw new PayrollException($"Unknown value type {valueType}");
    }

    /// <summary>Get the base value type (JSON type)</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>The value base type</returns>
    /// <exception cref="PayrollException">Unknown value type</exception>
    public static ValueBaseType GetBaseType(this ValueType valueType)
    {
        return valueType.GetSystemType().Name switch
        {
            nameof(Int32) => ValueBaseType.Number,
            nameof(Decimal) => ValueBaseType.Number,
            nameof(String) => ValueBaseType.String,
            nameof(DateTime) => ValueBaseType.String,
            nameof(Boolean) => ValueBaseType.Boolean,
            _ => ValueBaseType.Null
        };
    }

    /// <summary>
    /// Gets the default value of a value type
    /// </summary>
    /// <param name="valueType">The value type</param>
    /// <returns>The value</returns>
    public static object GetDefaultValue(this ValueType valueType)
    {
        return valueType.GetSystemType().Name switch
        {
            nameof(Int32) => default(int),
            nameof(Decimal) => default(decimal),
            nameof(String) => string.Empty,
            nameof(DateTime) => default(DateTime),
            nameof(Boolean) => default(bool),
            _ => null
        };
    }

    /// <summary>Test if value type is a bool</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>True for boolean value types</returns>
    public static bool IsBoolean(this ValueType valueType) =>
        valueType == ValueType.Boolean;

    /// <summary>Test if value type is a string</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>True for string value types</returns>
    public static bool IsString(this ValueType valueType) =>
        valueType is ValueType.String or
            ValueType.WebResource;

    /// <summary>Test if value type is a date time</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>True for date time value types</returns>
    public static bool IsDateTime(this ValueType valueType) =>
        valueType is ValueType.Date or
            ValueType.DateTime;

    /// <summary>Test if value type is a number</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>True for number value types</returns>
    public static bool IsNumber(this ValueType valueType) =>
        IsInteger(valueType) || IsDecimal(valueType);

    /// <summary>Test if value type is an integer</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>True for integer value types</returns>
    public static bool IsInteger(this ValueType valueType) =>
        valueType is ValueType.Integer or
            ValueType.Weekday or
            ValueType.Month or
            ValueType.Year;

    /// <summary>Test if value type is a decimal number</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>True for decimal number value types</returns>
    public static bool IsDecimal(this ValueType valueType) =>
        valueType is ValueType.Decimal or
            ValueType.Money or
            ValueType.Percent or
            ValueType.NumericBoolean;

    /// <summary>
    /// Test for the base type
    /// </summary>
    /// <param name="valueType">The value type</param>
    /// <param name="baseType">The value base type</param>
    /// <returns>True if the value type matches base type is</returns>
    public static bool IsTypeOf(this ValueType valueType, ValueBaseType baseType) =>
        GetBaseType(valueType) == baseType;
}