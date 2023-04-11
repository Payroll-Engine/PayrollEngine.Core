using System;
using System.Text.Json;

namespace PayrollEngine;

/// <summary><see cref="IAttributeObject" /> extension methods</summary>
public static class AttributeObjectExtensions
{
    /// <summary>Test attribute</summary>
    /// <param name="attributeObject">The attribute object</param>
    /// <param name="key">The value key</param>
    /// <returns>True if attribute is available</returns>
    public static bool ContainsAttribute(this IAttributeObject attributeObject, string key) =>
        attributeObject.Attributes?.ContainsKey(key) ?? false;

    /// <summary>Get attribute value</summary>
    /// <param name="attributeObject">The attribute object</param>
    /// <param name="key">The value key</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The dictionary value</returns>
    public static object GetAttribute(this IAttributeObject attributeObject, string key,
        object defaultValue = default) =>
        attributeObject.Attributes == null ?
            defaultValue :
            attributeObject.Attributes.GetValue(key, defaultValue);

    /// <summary>Get attribute value</summary>
    /// <param name="attributeObject">The attribute object</param>
    /// <param name="key">The value key</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The dictionary value</returns>
    public static T GetAttribute<T>(this IAttributeObject attributeObject, string key,
        T defaultValue = default) =>
        attributeObject.Attributes == null ?
            defaultValue :
            (T)Convert.ChangeType(GetAttribute(attributeObject, key, (object)defaultValue), typeof(T));

    /// <summary>Remove attribute</summary>
    /// <param name="attributeObject">The attribute object</param>
    /// <param name="key">The value key</param>
    public static void RemoveAttribute(this IAttributeObject attributeObject, string key)
    {
        if (ContainsAttribute(attributeObject, key))
        {
            attributeObject.Attributes.Remove(key);
        }
    }

    /// <summary>Set attribute value</summary>
    /// <param name="attributeObject">The attribute object</param>
    /// <param name="key">The value key</param>
    /// <param name="value">The value to set</param>
    public static void SetAttribute(this IAttributeObject attributeObject, string key, object value)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException(nameof(key));
        }

        // remove
        if (value == null)
        {
            RemoveAttribute(attributeObject, key);
        }
        else
        {
            // ensure attributes dictionary
            attributeObject.Attributes ??= new();
            // add/update value
            attributeObject.Attributes[key] = value;
        }
    }

    /// <summary>Set attribute value</summary>
    /// <param name="attributeObject">The attribute object</param>
    /// <param name="key">The value key</param>
    /// <param name="value">The value to set</param>
    public static void SetAttribute<T>(this IAttributeObject attributeObject, string key, T value) =>
        SetAttribute(attributeObject, key, (object)value);

    /// <summary>Get attribute <see cref="Guid"/> value</summary>
    /// <param name="attributeObject">The attribute object</param>
    /// <param name="key">The value key</param>
    /// <returns>The dictionary value, <see cref="Guid.Empty"/> on missing attribute</returns>
    public static Guid GetAttributeGuid(this IAttributeObject attributeObject, string key)
    {
        if (attributeObject.Attributes == null || !attributeObject.Attributes.ContainsKey(key))
        {
            return Guid.Empty;
        }

        var value = attributeObject.GetAttribute<string>(key);
        return string.IsNullOrWhiteSpace(value) ? Guid.Empty : Guid.Parse(value);
    }

    /// <summary>Set attribute <see cref="Guid"/> value</summary>
    /// <param name="attributeObject">The attribute object</param>
    /// <param name="key">The value key</param>
    /// <param name="value">The Guid value to set</param>
    public static void SetAttributeGuid(this IAttributeObject attributeObject, string key, Guid value)
    {
        if (value == Guid.Empty)
        {
            attributeObject.RemoveAttribute(key);
        }
        else
        {
            attributeObject.SetAttribute(key, JsonSerializer.Serialize(value));
        }
    }

}
