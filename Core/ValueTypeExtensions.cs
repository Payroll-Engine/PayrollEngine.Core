using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="ValueType"/></summary>
public static class ValueTypeExtensions
{
    /// <param name="valueType">The value type</param>
    extension(ValueType valueType)
    {
        /// <summary>Get the data system type</summary>
        /// <returns>The data type</returns>
        public Type GetSystemType()
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
            throw new PayrollException($"Unknown value type {valueType}.");
        }

        /// <summary>Get the base value type (JSON type)</summary>
        /// <returns>The value base type</returns>
        /// <exception cref="PayrollException">Unknown value type</exception>
        public ValueBaseType GetBaseType()
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
        /// <returns>The value</returns>
        public object GetDefaultValue()
        {
            return valueType.GetSystemType().Name switch
            {
                nameof(Int32) => 0,
                nameof(Decimal) => 0,
                nameof(String) => string.Empty,
                nameof(DateTime) => default(DateTime),
                nameof(Boolean) => false,
                _ => null
            };
        }

        /// <summary>Test if value type is a bool</summary>
        /// <returns>True for boolean value types</returns>
        public bool IsBoolean() =>
            valueType == ValueType.Boolean;

        /// <summary>Test if value type is a string</summary>
        /// <returns>True for string value types</returns>
        public bool IsString() =>
            valueType is ValueType.String or
                ValueType.WebResource;

        /// <summary>Test if value type is a date time</summary>
        /// <returns>True for date time value types</returns>
        public bool IsDateTime() =>
            valueType is ValueType.Date or
                ValueType.DateTime;

        /// <summary>Test if value type is a number</summary>
        /// <returns>True for number value types</returns>
        public bool IsNumber() => 
            valueType.IsInteger() || valueType.IsDecimal();

        /// <summary>Test if value type is an integer</summary>
        /// <returns>True for integer value types</returns>
        public bool IsInteger() =>
            valueType is ValueType.Integer or
                ValueType.Weekday or
                ValueType.Month or
                ValueType.Year;

        /// <summary>Test if value type is a decimal number</summary>
        /// <returns>True for decimal number value types</returns>
        public bool IsDecimal() =>
            valueType is ValueType.Decimal or
                ValueType.Money or
                ValueType.Percent or
                ValueType.NumericBoolean;

        /// <summary>
        /// Test for the base type
        /// </summary>
        /// <param name="baseType">The value base type</param>
        /// <returns>True if the value type matches base type is</returns>
        public bool IsTypeOf(ValueBaseType baseType) => 
            valueType.GetBaseType() == baseType;
    }
}