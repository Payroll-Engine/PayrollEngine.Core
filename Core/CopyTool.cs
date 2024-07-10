using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PayrollEngine;

/// <summary>Copy object tool</summary>
public static class CopyTool
{
    /// <summary>Copy objects properties, base types are not considered</summary>
    /// <param name="source">The left object to compare</param>
    /// <param name="target">The right object to compare</param>
    /// <param name="ignoreAttributes">The property attribute to ignore</param>
    public static void CopyObjectProperties(object source, object target,
        IEnumerable<Type> ignoreAttributes = null)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);
        if (source.GetType() != target.GetType())
        {
            throw new ArgumentException(null, nameof(target));
        }
        var properties = TypeTool.GetTypeProperties(source.GetType());
        var ignoreTypes = GetIgnoreTypes(ignoreAttributes);
        foreach (var property in properties)
        {
            // ignore
            if (IgnoreProperty(property, ignoreTypes))
            {
                continue;
            }

            if (property.SetMethod != null)
            {
                var value = property.GetValue(source);
                property.SetValue(target, value);
            }
        }
    }

    /// <summary>Copy typed object properties, base types are not considered</summary>
    /// <param name="source">The left object to compare</param>
    /// <param name="target">The right object to compare</param>
    /// <param name="ignoreAttributes">The property attribute to ignore</param>
    public static void CopyProperties<T>(T source, T target, IEnumerable<Type> ignoreAttributes = null) where T : class
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);

        var properties = TypeTool.GetTypeProperties(typeof(T));
        var ignoreTypes = GetIgnoreTypes(ignoreAttributes);
        foreach (var property in properties)
        {
            // ignore
            if (IgnoreProperty(property, ignoreTypes))
            {
                continue;
            }

            // copy
            if (property.SetMethod != null)
            {
                var value = property.GetValue(source);
                property.SetValue(target, value);
            }
        }
    }

    private static List<Type> GetIgnoreTypes(IEnumerable<Type> ignoreAttributes = null)
    {
        var ignoreTypes = ignoreAttributes != null ? ignoreAttributes.ToList() : [];
        if (!ignoreTypes.Any())
        {
            ignoreTypes.Add(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute));
            ignoreTypes.Add(typeof(System.Text.Json.Serialization.JsonIgnoreAttribute));
        }
        return ignoreTypes;
    }

    private static bool IgnoreProperty(PropertyInfo property, List<Type> ignoreTypes = null)
    {
        if (ignoreTypes == null)
        {
            return false;
        }
        foreach (var ignoreType in ignoreTypes)
        {
            if (property.GetCustomAttributes(ignoreType).Any())
            {
                return true;
            }
        }
        return false;
    }
}