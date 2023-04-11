using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="ValueType"/></summary>
public static class ValueTypeExtensions
{

    /// <summary>Get the data type</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>The data type</returns>
    public static Type GetDataType(this ValueType valueType)
    {
        switch (valueType)
        {
            case ValueType.String:
            case ValueType.WebResource:
            case ValueType.Date:
            case ValueType.DateTime:
                return typeof(string);
            case ValueType.Integer:
                return typeof(int);
            case ValueType.NumericBoolean:
            case ValueType.Decimal:
            case ValueType.Money:
            case ValueType.Percent:
            case ValueType.Hour:
            case ValueType.Day:
            case ValueType.Distance:
                return typeof(decimal);
            case ValueType.Boolean:
                return typeof(bool);
            case ValueType.None:
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
        switch (valueType)
        {
            case ValueType.String:
            case ValueType.WebResource:
            case ValueType.Date:
            case ValueType.DateTime:
                return ValueBaseType.String;
            case ValueType.Integer:
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
                return ValueBaseType.Number;
            case ValueType.Boolean:
                return ValueBaseType.Boolean;
            case ValueType.None:
                return ValueBaseType.Null;
        }
        throw new PayrollException($"Unknown value type {valueType}");
    }

    /// <summary>
    /// Gets the default type of a value type
    /// </summary>
    /// <param name="valueType">The value type</param>
    /// <returns>The value</returns>
    public static object GetDefaultValue(this ValueType valueType)
    {
        switch (valueType)
        {
            case ValueType.Integer:
                return default(int);
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
                return default(decimal);

            case ValueType.String:
            case ValueType.WebResource:
                return string.Empty;
            case ValueType.Date:
            case ValueType.DateTime:
                return default(DateTime);

            case ValueType.Boolean:
                return default(bool);
            case ValueType.None:
                return null;
            default:
                throw new PayrollException($"unknown value type {valueType}");
        }
    }

    /// <summary>Test if value type is a text</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>True for text value types</returns>
    public static bool IsText(this ValueType valueType) =>
        valueType == ValueType.String ||
        valueType == ValueType.WebResource ||
        valueType == ValueType.Date ||
        valueType == ValueType.DateTime;

    /// <summary>Test if value type is a number</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>True for number value types</returns>
    public static bool IsNumber(this ValueType valueType) =>
        GetBaseType(valueType) == ValueBaseType.Number;

    /// <summary>Test if value type is a decimal number</summary>
    /// <param name="valueType">The value type</param>
    /// <returns>True for decimal number value types</returns>
    public static bool IsDecimal(this ValueType valueType) =>
        valueType is ValueType.Decimal or
            ValueType.Money or
            ValueType.Percent or
            ValueType.Hour or
            ValueType.Day or
            ValueType.Week or
            ValueType.Month or
            ValueType.Year or
            ValueType.Distance or
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