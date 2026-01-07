using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>
/// Information about a c# object
/// </summary>
public static class ObjectInfo
{
    private static readonly Dictionary<Type, List<PropertyInfo>> Properties = new();
    private static readonly Lock Locker = new();

    /// <summary>
    /// Get object properties
    /// </summary>
    /// <remarks>Use the local type cache</remarks>
    /// <param name="type">The object type</param>
    /// <returns>The object property infos</returns>
    public static IList<PropertyInfo> GetProperties(Type type)
    {
        lock (Locker)
        {
            if (!Properties.ContainsKey(type))
            {
                Properties[type] = [..type.GetInstanceProperties()];
            }
            return Properties[type];
        }
    }

    /// <summary>
    /// Get object properties
    /// </summary>
    /// <returns>The object property infos</returns>
    public static IList<PropertyInfo> GetProperties<T>() =>
        GetProperties(typeof(T));

    /// <param name="type">The object type</param>
    extension(Type type)
    {
        /// <summary>
        /// Get property types of the domain object
        /// </summary>
        /// <returns>A dictionary with filed field name (dictionary key) and the field type (dictionary value)</returns>
        public IDictionary<string, Type> GetPropertyTypes()
        {
            var propertyInfos = GetProperties(type);
            lock (Locker)
            {
                return propertyInfos.ToDictionary(x => x.Name, x => x.PropertyType);
            }
        }

        /// <summary>
        /// Get field names of the domain object
        /// </summary>
        /// <returns>A hash set with ordinal ignore case</returns>
        public HashSet<string> GetFieldNames()
        {
            var propertyInfos = GetProperties(type);
            lock (Locker)
            {
                return propertyInfos.Select(pi => pi.Name).ToHashSet();
            }
        }

        /// <summary>
        /// Get object property
        /// </summary>
        /// <param name="name">The property name</param>
        /// <returns>The property info</returns>
        public PropertyInfo GetProperty(string name) =>
            GetProperties(type).FirstOrDefault(x => string.Equals(x.Name, name));

        /// <summary>
        /// Test if property exists
        /// </summary>
        /// <param name="name">The property name</param>
        /// <returns>A hash set with ordinal ignore case</returns>
        public bool ContainsProperty(string name) =>
            GetProperty(type, name) != null;

        /// <summary>
        /// Test if property from a specific type exists
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="propertyType">The property type</param>
        /// <returns>A hash set with ordinal ignore case</returns>
        public bool ContainsProperty(string name, Type propertyType)
        {
            var property = GetProperty(type, name);
            return property != null && propertyType == property.PropertyType;
        }
    }

    /// <param name="obj">The object</param>
    extension(object obj)
    {
        /// <summary>
        /// Get object property value
        /// </summary>
        /// <param name="name">The property name</param>
        /// <returns>The property value</returns>
        public object GetPropertyValue(string name)
        {
            ArgumentNullException.ThrowIfNull(obj);
            var property = GetProperty(obj.GetType(), name);
            return property?.GetValue(obj);
        }

        /// <summary>
        /// Get object property value
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>The property value</returns>
        public T GetPropertyValue<T>(string name, T defaultValue = default)
        {
            ArgumentNullException.ThrowIfNull(obj);
            var property = GetProperty(obj.GetType(), name);
            var value = property?.GetValue(obj);
            return value != null ? (T)Convert.ChangeType(value, typeof(T)) : defaultValue;
        }

        /// <summary>
        /// Set object property value
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The value to set</param>
        public void SetPropertyValue(string name, object value)
        {
            ArgumentNullException.ThrowIfNull(obj);
            var property = GetProperty(obj.GetType(), name);
            if (property != null)
            {
                property.SetValue(obj, value);
            }
        }

        /// <summary>
        /// Resolve property expression
        /// </summary>
        /// <param name="expression">The property expression</param>
        /// <returns>Value with property</returns>
        public PropertyValue ResolvePropertyValue(string expression)
        {
            while (true)
            {
                if (obj == null || string.IsNullOrWhiteSpace(expression))
                {
                    return null;
                }

                // item properties
                var itemProperties = GetProperties(obj.GetType());
                if (!itemProperties.Any())
                {
                    return null;
                }

                // child property
                var index = expression.IndexOf('.');
                var propertyName = expression;
                string childExpression = null;
                if (index > 0)
                {
                    propertyName = expression.Substring(0, index);
                    childExpression = expression.Substring(index + 1);
                }

                // property
                var property = itemProperties.FirstOrDefault(x => string.Equals(x.Name, propertyName));
                if (property == null)
                {
                    return null;
                }

                // value
                object value;

                // child expression
                if (!string.IsNullOrWhiteSpace(childExpression))
                {
                    // test for string dictionary
                    var isDictionary = false;
                    if (property.PropertyType.IsGenericType &&
                        property.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                    {
                        var keyType = property.PropertyType.GetGenericArguments()[0];
                        var valueType = property.PropertyType.GetGenericArguments()[1];
                        isDictionary = keyType == typeof(string) && valueType == typeof(object);
                    }

                    // dictionary 
                    if (isDictionary)
                    {
                        // test child expression and string/object dictionary
                        if (string.IsNullOrWhiteSpace(childExpression) ||
                            property.GetValue(obj, null) is not IDictionary<string, object> dictionary)
                        {
                            return null;
                        }

                        // missing dictionary value
                        if (!dictionary.TryGetValue(childExpression, out var value1))
                        {
                            // missing dictionary value
                            return new PropertyValue
                            {
                                Property = property,
                                Value = null,
                                DictionaryKey = childExpression
                            };
                        }

                        // dictionary value
                        value = value1;
                        if (value is JsonElement jsonElement)
                        {
                            value = jsonElement.GetValue();
                        }
                        return new PropertyValue
                        {
                            Property = property,
                            Value = value,
                            DictionaryKey = childExpression
                        };
                    }
                }

                // final property
                value = property.GetValue(obj, null);
                if (value == null)
                {
                    return new PropertyValue
                    {
                        Property = property
                    };
                }

                // final object property
                if (string.IsNullOrWhiteSpace(childExpression))
                {
                    return new PropertyValue
                    {
                        Property = property,
                        Value = value
                    };
                }

                // child property
                obj = value;
                expression = childExpression;
            }
        }
    }
}