using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;

namespace PayrollEngine;

/// <summary>
/// Attribute dictionary extension methods
/// </summary>
public static class AttributesExtensions
{
    /// <param name="attributes">The attributes dictionary</param>
    extension(IDictionary<string, object> attributes)
    {
        /// <summary>
        /// Test for attribute
        /// </summary>
        /// <param name="name">The attribute name</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>True if attribute exists</returns>
        public bool HasAttribute(string name,
            string culture = null) =>
            attributes != null && attributes.ContainsKey(attributes.GetAttributeKey(name, culture));

        /// <summary>
        /// Get member value
        /// </summary>
        /// <param name="defaultValue">The default value</param>
        /// <param name="culture">The attribute culture</param>
        /// <param name="memberName">The member name</param>
        /// <returns>The member value</returns>
        public T GetMemberAttributeValue<T>(T defaultValue = default, string culture = null, [CallerMemberName] string memberName = "") =>
            attributes.GetAttributeValue(memberName, defaultValue: defaultValue, culture: culture);

        /// <summary>
        /// Get attribute value
        /// </summary>
        /// <param name="name">The attribute name</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The attribute value</returns>
        public T GetAttributeValue<T>(string name, T defaultValue = default, string culture = null)
        {
            // empty attributes
            if (attributes == null)
            {
                return defaultValue;
            }

            // empty value
            var key = attributes.GetAttributeKey(name, culture);
            if (!attributes.TryGetValue(key, out var value))
            {
                return defaultValue;
            }

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
        /// <param name="name">The attribute name</param>
        /// <param name="value">The attribute value</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>True for an existing attribute</returns>
        public bool TryGetAttributeValue<T>(string name, out T value, string culture = null)
        {
            if (!attributes.HasAttribute(name))
            {
                value = default;
                return false;
            }

            value = attributes.GetAttributeValue<T>(name, culture: culture);
            return true;
        }

        /// <summary>
        /// Get a string attribute value
        /// </summary>
        /// <param name="name">The attribute name</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The string value</returns>
        public string GetStringAttributeValue(string name, string culture = null) => 
            attributes.GetAttributeValue<string>(name, culture: culture);

        /// <summary>
        /// Get a date attribute value
        /// </summary>
        /// <param name="name">The attribute name</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The date value</returns>
        public DateTime? GetDateTimeAttributeValue(string name, CultureInfo culture = null)
        {
            culture ??= Thread.CurrentThread.CurrentUICulture;
            return DateTime.TryParse(attributes.GetAttributeValue<string>(name, culture: culture.Name), culture,
                out var dateTime)
                ? dateTime
                : null;
        }

        /// <summary>
        /// Get a boolean attribute value
        /// </summary>
        /// <param name="name">The attribute name</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The boolean value</returns>
        public bool? GetBooleanAttributeValue(string name, string culture = null) =>
            attributes.TryGetAttributeValue(name, out bool value, culture) ? value : null;

        /// <summary>
        /// Get a integer attribute value
        /// </summary>
        /// <param name="name">The attribute name</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The integer value</returns>
        public int? GetIntegerAttributeValue(string name, string culture = null) =>
            attributes.TryGetAttributeValue(name, out int value, culture) ? value : null;

        /// <summary>
        /// Get a decimal attribute value
        /// </summary>
        /// <param name="name">The attribute name</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The decimal value</returns>
        public decimal? GetDecimalAttributeValue(string name, string culture = null) =>
            attributes.TryGetAttributeValue(name, out decimal value, culture) ? value : null;
    }

    /// <summary>
    /// Get an enum attribute value
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    /// <param name="attributes">Attribute dictionary</param>
    /// <param name="name">Name of attribute to extract enum from</param>
    /// <param name="culture">The attribute culture</param>
    /// <returns>Enum representation of string input</returns>
    public static T? GetEnumAttributeValue<T>(this Dictionary<string, object> attributes,
        string name, string culture = null)
        where T : struct
    {
        // check for valid type
        var type = typeof(T);
        if (!type.IsEnum)
        {
            throw new PayrollException("Can not get enum value from non-Enum type.");
        }

        if (attributes.TryGetAttributeValue(name, out string attributeValue, culture) &&
            Enum.TryParse(type, attributeValue, true, out var attributeType))
        {
            return (T)attributeType;
        }

        return null;
    }

    /// <param name="attributes">The attributes dictionary</param>
    extension(IDictionary<string, object> attributes)
    {
        /// <summary>
        /// Set member value
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="culture">The attribute culture</param>
        /// <param name="memberName">The member name</param>
        /// <returns>The member value</returns>
        public void SetMemberAttributeValue<T>(T value, string culture = null, [CallerMemberName] string memberName = "") =>
            attributes.SetAttributeValue(memberName, value, culture);

        /// <summary>
        /// Set an attribute value
        /// </summary>
        /// <param name="name">The attribute name</param>
        /// <param name="value">The value</param>
        /// <param name="culture">The attribute culture</param>
        public void SetAttributeValue<T>(string name, T value, string culture = null)
        {
            attributes[attributes.GetAttributeKey(name, culture)] = value;
        }

        /// <summary>
        /// Get attribute key
        /// </summary>
        /// <param name="name">The attribute name</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The attribute key</returns>
        private string GetAttributeKey(string name, string culture = null)
        {
            if (culture == null)
            {
                return name;
            }

            // language attribute key
            var languageKey = $"{name}.{culture}";
            if (attributes.ContainsKey(languageKey))
            {
                return languageKey;
            }
            return name;
        }
    }
}