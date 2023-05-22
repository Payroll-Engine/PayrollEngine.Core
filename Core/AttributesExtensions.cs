using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace PayrollEngine;

/// <summary>
/// Attribute dictionary extension methods
/// </summary>
public static class AttributesExtensions
{
    /// <summary>
    /// Test for attribute
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="name">The attribute name</param>
    /// <param name="language">The attribute language</param>
    /// <returns>True if attribute exists</returns>
    public static bool HasAttribute(this IDictionary<string, object> attributes, string name,
        Language? language = null) =>
        attributes != null && attributes.ContainsKey(GetAttributeKey(attributes, name, language));

    /// <summary>
    /// Get member value
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="language">The attribute language</param>
    /// <param name="memberName">The member name</param>
    /// <returns>The member value</returns>
    public static T GetMemberAttributeValue<T>(this IDictionary<string, object> attributes,
       T defaultValue = default, Language? language = null, [CallerMemberName] string memberName = "") =>
        GetAttributeValue(attributes, memberName, defaultValue: defaultValue, language: language);

    /// <summary>
    /// Get attribute value
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="name">The attribute name</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="language">The attribute language</param>
    /// <returns>The attribute value</returns>
    public static T GetAttributeValue<T>(this IDictionary<string, object> attributes,
        string name, T defaultValue = default, Language? language = null)
    {
        // empty attributes
        if (attributes == null)
        {
            return defaultValue;
        }

        // empty value
        var key = GetAttributeKey(attributes, name, language);
        if (!attributes.ContainsKey(key))
        {
            return defaultValue;
        }

        var value = attributes[key];
        // undefined default
        if (value == null)
        {
            return defaultValue;
        }

        // json value
        if (value is JsonElement jsonElement)
        {
            value = typeof(T) == typeof(string) ?
                jsonElement.ToString() : jsonElement.GetValue();
        }

        // list value
        if (value is IList listValues)
        {
            var type = typeof(T);
            if (!type.IsGenericType)
            {
                throw new InvalidOperationException($"Type {typeof(T)} must be an array");
            }

            var itemType = type.GetGenericArguments()[0];
            var itemValues = Activator.CreateInstance(typeof(T)) as IList;
            if (itemValues == null)
            {
                throw new InvalidOperationException($"Type {typeof(T)} must be a List");
            }
            foreach (var listValue in listValues)
            {
                var itemValue = Convert.ChangeType(listValue, itemType);
                itemValues.Add(itemValue);
            }
            value = itemValues;
        }

        return (T)Convert.ChangeType(value, typeof(T).GetNullableType());
    }

    /// <summary>
    /// Try to get an attribute value
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="name">The attribute name</param>
    /// <param name="value">The attribute value</param>
    /// <param name="language">The attribute language</param>
    /// <returns>True for an existing attribute</returns>
    public static bool TryGetAttributeValue<T>(this IDictionary<string, object> attributes,
        string name, out T value, Language? language = null)
    {
        if (!HasAttribute(attributes, name))
        {
            value = default;
            return false;
        }

        value = GetAttributeValue<T>(attributes, name, language: language);
        return true;
    }

    /// <summary>
    /// Get a string attribute value
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="name">The attribute name</param>
    /// <param name="language">The attribute language</param>
    /// <returns>The string value</returns>
    public static string GetStringAttributeValue(this IDictionary<string, object> attributes,
        string name, Language? language = null) =>
        GetAttributeValue<string>(attributes, name, language: language);

    /// <summary>
    /// Get a date attribute value
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="name">The attribute name</param>
    /// <param name="language">The attribute language</param>
    /// <param name="culture">The attribute culture</param>
    /// <returns>The date value</returns>
    public static DateTime? GetDateTimeAttributeValue(this IDictionary<string, object> attributes,
        string name, Language? language = null, CultureInfo culture = null) =>
        DateTime.TryParse(GetAttributeValue<string>(attributes, name, language: language),
            culture ?? CultureInfo.CurrentCulture,
            out var dateTime) ? dateTime : null;

    /// <summary>
    /// Get a boolean attribute value
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="name">The attribute name</param>
    /// <param name="language">The attribute language</param>
    /// <returns>The boolean value</returns>
    public static bool? GetBooleanAttributeValue(this IDictionary<string, object> attributes,
        string name, Language? language = null) =>
        TryGetAttributeValue(attributes, name, out bool value, language) ? value : null;

    /// <summary>
    /// Get a integer attribute value
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="name">The attribute name</param>
    /// <param name="language">The attribute language</param>
    /// <returns>The integer value</returns>
    public static int? GetIntegerAttributeValue(this IDictionary<string, object> attributes,
        string name, Language? language = null) =>
        TryGetAttributeValue(attributes, name, out int value, language) ? value : null;

    /// <summary>
    /// Get a decimal attribute value
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="name">The attribute name</param>
    /// <param name="language">The attribute language</param>
    /// <returns>The decimal value</returns>
    public static decimal? GetDecimalAttributeValue(this IDictionary<string, object> attributes,
        string name, Language? language = null) =>
        TryGetAttributeValue(attributes, name, out decimal value, language) ? value : null;

    /// <summary>
    /// Get an enum attribute value
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    /// <param name="attributes">Attribute dictionary</param>
    /// <param name="name">Name of attribute to extract enum from</param>
    /// <param name="language">The attribute language</param>
    /// <returns>Enum representation of string input</returns>
    public static T? GetEnumAttributeValue<T>(this Dictionary<string, object> attributes,
        string name, Language? language = null)
        where T : struct
    {
        // check for valid type
        var type = typeof(T);
        if (!type.IsEnum)
        {
            throw new PayrollException("Can not get enum value from non-Enum type");
        }

        if (attributes.TryGetAttributeValue(name, out string attributeValue, language) &&
            Enum.TryParse(type, attributeValue, true, out var attributeType))
        {
            return (T)attributeType;
        }

        return null;
    }

    /// <summary>
    /// Set member value
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="value">The value</param>
    /// <param name="language">The attribute language</param>
    /// <param name="memberName">The member name</param>
    /// <returns>The member value</returns>
    public static void SetMemberAttributeValue<T>(this IDictionary<string, object> attributes,
        T value, Language? language = null, [CallerMemberName] string memberName = "") =>
        SetAttributeValue(attributes, memberName, value, language);

    /// <summary>
    /// Set an attribute value
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="name">The attribute name</param>
    /// <param name="value">The value</param>
    /// <param name="language">The attribute language</param>
    public static void SetAttributeValue<T>(this IDictionary<string, object> attributes,
        string name, T value, Language? language = null)
    {
        attributes[GetAttributeKey(attributes, name, language)] = value;
    }

    /// <summary>
    /// Get attribute key
    /// </summary>
    /// <param name="attributes">The attributes dictionary</param>
    /// <param name="name">The attribute name</param>
    /// <param name="language">The attribute language</param>
    /// <returns>The attribute key</returns>
    private static string GetAttributeKey(this IDictionary<string, object> attributes,
        string name, Language? language = null)
    {
        if (language == null)
        {
            return name;
        }

        // language attribute key
        var languageKey = $"{name}.{language.Value.LanguageCode()}";
        if (attributes.ContainsKey(languageKey))
        {
            return languageKey;
        }
        return name;
    }
}