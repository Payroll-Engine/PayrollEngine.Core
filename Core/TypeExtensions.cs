using System;
using System.Reflection;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="Type"/></summary>
public static class TypeExtensions
{
    /// <summary>
    /// Get the public type properties
    /// </summary>
    /// <param name="type">The type</param>
    /// <returns>The public type properties</returns>
    public static PropertyInfo[] GetInstanceProperties(this Type type) =>
        TypeTool.GetInstanceProperties(type);

    /// <summary>
    /// Determines whether the type is numeric.
    /// See https://stackoverflow.com/a/1750024
    /// </summary>
    /// <param name="type">The type</param>
    /// <returns>True for numeric types</returns>
    public static bool IsNumericType(this Type type)
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
    /// Check if value has an nullable underlying type.
    /// </summary>
    /// <param name="type">Type to be checked</param>
    /// <returns>Type is nullable or not</returns>
    public static bool IsNullable(this Type type) =>
        Nullable.GetUnderlyingType(type) != null;

    /// <summary>Gets the default value of a type</summary>
    /// <param name="type">The type</param>
    /// <returns>The default value</returns>
    public static object GetDefaultValue(this Type type) =>
        type.IsValueType ? Activator.CreateInstance(type) : null;

    /// <summary>Gets the type of nullable types</summary>
    /// <param name="type">The underlying type</param>
    public static Type GetNullableType(this Type type)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
        return underlyingType;
    }

    /// <summary>Test for serialized type</summary>
    /// <param name="type">The underlying type</param>
    public static bool IsSerializedType(this Type type)
    {
        var nullableType = Nullable.GetUnderlyingType(type);
        var baseType = nullableType ?? type;
        return baseType != typeof(string) && !baseType.IsEnum && (baseType.IsArray || baseType.IsClass || baseType.IsGenericType);
    }
}