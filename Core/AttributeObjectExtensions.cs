using System;
using System.Text.Json;

namespace PayrollEngine;

/// <summary><see cref="IAttributeObject" /> extension methods</summary>
public static class AttributeObjectExtensions
{
    /// <param name="attributeObject">The attribute object</param>
    extension(IAttributeObject attributeObject)
    {
        /// <summary>Test attribute</summary>
        /// <param name="key">The value key</param>
        /// <returns>True if attribute is available</returns>
        public bool ContainsAttribute(string key) =>
            attributeObject.Attributes?.ContainsKey(key) ?? false;

        /// <summary>Get attribute value</summary>
        /// <param name="key">The value key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The dictionary value</returns>
        public object GetAttribute(string key,
            object defaultValue = null) =>
            attributeObject.Attributes == null ?
                defaultValue :
                attributeObject.Attributes.GetValue(key, defaultValue);

        /// <summary>Get attribute value</summary>
        /// <param name="key">The value key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The dictionary value</returns>
        public T GetAttribute<T>(string key,
            T defaultValue = default) =>
            attributeObject.Attributes == null ?
                defaultValue :
                (T)Convert.ChangeType(attributeObject.GetAttribute(key, (object)defaultValue), typeof(T));

        /// <summary>Remove attribute</summary>
        /// <param name="key">The value key</param>
        public void RemoveAttribute(string key)
        {
            if (attributeObject.ContainsAttribute(key))
            {
                attributeObject.Attributes.Remove(key);
            }
        }

        /// <summary>Set attribute value</summary>
        /// <param name="key">The value key</param>
        /// <param name="value">The value to set</param>
        public void SetAttribute(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(null, nameof(key));
            }

            // remove
            if (value == null)
            {
                attributeObject.RemoveAttribute(key);
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
        /// <param name="key">The value key</param>
        /// <param name="value">The value to set</param>
        public void SetAttribute<T>(string key, T value) => 
            attributeObject.SetAttribute(key, (object)value);

        /// <summary>Get attribute <see cref="Guid"/> value</summary>
        /// <param name="key">The value key</param>
        /// <returns>The dictionary value, <see cref="Guid.Empty"/> on missing attribute</returns>
        public Guid GetAttributeGuid(string key)
        {
            if (attributeObject.Attributes == null || !attributeObject.Attributes.ContainsKey(key))
            {
                return Guid.Empty;
            }

            var value = attributeObject.GetAttribute<string>(key);
            return string.IsNullOrWhiteSpace(value) ? Guid.Empty : Guid.Parse(value);
        }

        /// <summary>Set attribute <see cref="Guid"/> value</summary>
        /// <param name="key">The value key</param>
        /// <param name="value">The Guid value to set</param>
        public void SetAttributeGuid(string key, Guid value)
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
}
