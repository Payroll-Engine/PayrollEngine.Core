using System;

namespace PayrollEngine;

/// <summary>Copy object tool</summary>
public static class CopyTool
{
    /// <summary>Copy objects properties, base types are not considered</summary>
    /// <param name="source">The left object to compare</param>
    /// <param name="target">The right object to compare</param>
    public static void CopyObjectProperties(object source, object target)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }
        if (source.GetType() != target.GetType())
        {
            throw new ArgumentException(nameof(target));
        }
        var properties = TypeTool.GetInstanceProperties(source.GetType());
        foreach (var property in properties)
        {
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
    public static void CopyProperties<T>(T source, T target) where T : class
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        var properties = TypeTool.GetInstanceProperties(typeof(T));
        foreach (var property in properties)
        {
            if (property.SetMethod != null)
            {
                var value = property.GetValue(source);
                property.SetValue(target, value);
            }
        }
    }
}