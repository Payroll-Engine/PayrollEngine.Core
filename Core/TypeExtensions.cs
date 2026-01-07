using System;
using System.Collections.Generic;
using System.Reflection;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="Type"/></summary>
public static class TypeExtensions
{
    /// <param name="type">The type</param>
    extension(Type type)
    {
        /// <summary>
        /// Get the public type properties
        /// </summary>
        /// <returns>The public type properties</returns>
        public List<PropertyInfo> GetInstanceProperties() =>
            TypeTool.GetInstanceProperties(type);

        /// <summary>
        /// Determines whether the type is numeric.
        /// See https://stackoverflow.com/a/1750024
        /// </summary>
        /// <returns>True for numeric types</returns>
        public bool IsNumericType()
        {
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Byte => true,
                TypeCode.SByte => true,
                TypeCode.UInt16 => true,
                TypeCode.UInt32 => true,
                TypeCode.UInt64 => true,
                TypeCode.Int16 => true,
                TypeCode.Int32 => true,
                TypeCode.Int64 => true,
                TypeCode.Decimal => true,
                TypeCode.Double => true,
                TypeCode.Single => true,
                _ => false
            };
        }

        /// <summary>
        /// Check if value has a nullable underlying type.
        /// </summary>
        /// <returns>Type is nullable or not</returns>
        public bool IsNullable() =>
            Nullable.GetUnderlyingType(type) != null;

        /// <summary>Gets the default value of a type</summary>
        /// <returns>The default value</returns>
        public object GetDefaultValue() =>
            type.IsValueType ? Activator.CreateInstance(type) : null;

        /// <summary>Gets the type of nullable types</summary>
        public Type GetNullableType()
        {
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
            return underlyingType;
        }

        /// <summary>Test for serialized type</summary>
        public bool IsSerializedType()
        {
            var nullableType = Nullable.GetUnderlyingType(type);
            var baseType = nullableType ?? type;
            return baseType != typeof(string) && !baseType.IsEnum && (baseType.IsArray || baseType.IsClass || baseType.IsGenericType);
        }
    }
}