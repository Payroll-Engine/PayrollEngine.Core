using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace PayrollEngine;

/// <summary>
/// Information about a c# object
/// </summary>
public static class ObjectInfo
{
    private static readonly Dictionary<Type, List<PropertyInfo>> Properties = new();
    private static readonly object Locker = new();

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
                Properties[type] = new(type.GetInstanceProperties());
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

    /// <summary>
    /// Get property types of the domain object
    /// </summary>
    /// <returns>A dictionary with filed field name (dictionary key) and the field type (dictionary value)</returns>
    public static IDictionary<string, Type> GetPropertyTypes(this Type type)
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
    /// <param name="type">The object type</param>
    /// <returns>A hash set with ordinal ignore case</returns>
    public static HashSet<string> GetFieldNames(this Type type)
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
    /// <param name="type">The object type</param>
    /// <param name="name">The property name</param>
    /// <returns>The property info</returns>
    public static PropertyInfo GetProperty(this Type type, string name) =>
        GetProperties(type).FirstOrDefault(x => string.Equals(x.Name, name));

    /// <summary>
    /// Test if property exists
    /// </summary>
    /// <param name="type">The object type</param>
    /// <param name="name">The property name</param>
    /// <returns>A hash set with ordinal ignore case</returns>
    public static bool ContainsProperty(this Type type, string name) =>
        GetProperty(type, name) != null;

    /// <summary>
    /// Test if property from a specific type exists
    /// </summary>
    /// <param name="type">The object type</param>
    /// <param name="name">The property name</param>
    /// <param name="propertyType">The property type</param>
    /// <returns>A hash set with ordinal ignore case</returns>
    public static bool ContainsProperty(this Type type, string name, Type propertyType)
    {
        var property = GetProperty(type, name);
        return property != null && propertyType == property.PropertyType;
    }

    /// <summary>
    /// Get object property value
    /// </summary>
    /// <param name="obj">The object</param>
    /// <param name="name">The property name</param>
    /// <returns>The property value</returns>
    public static object GetPropertyValue(this object obj, string name)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
        var property = GetProperty(obj.GetType(), name);
        return property?.GetValue(obj);
    }

    /// <summary>
    /// Get object property value
    /// </summary>
    /// <param name="obj">The object</param>
    /// <param name="name">The property name</param>
    /// <param name="defaultValue">default value</param>
    /// <returns>The property value</returns>
    public static T GetPropertyValue<T>(this object obj, string name, T defaultValue = default)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
        var property = GetProperty(obj.GetType(), name);
        var value = property?.GetValue(obj);
        return value != null ? (T)Convert.ChangeType(value, typeof(T)) : defaultValue;
    }

    /// <summary>
    /// Set object property value
    /// </summary>
    /// <param name="obj">The object</param>
    /// <param name="name">The property name</param>
    /// <param name="value">The value to set</param>
    public static void SetPropertyValue(this object obj, string name, object value)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
        var property = GetProperty(obj.GetType(), name);
        if (property != null)
        {
            property.SetValue(obj, value);
        }
    }

    /// <summary>
    /// Resolve property expression
    /// </summary>
    /// <param name="item">The value item</param>
    /// <param name="expression">The property expression</param>
    /// <returns>Value with property</returns>
    public static PropertyValue ResolvePropertyValue(this object item, string expression)
    {
        while (true)
        {
            if (item == null || string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            // item properties
            var itemProperties = GetProperties(item.GetType());
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
                    property.GetValue(item, null) is not IDictionary<string, object> dictionary)
                {
                    return null;
                }

                // missing dictionary value
                if (!dictionary.ContainsKey(childExpression))
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
                value = dictionary[childExpression];
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

            // plain property
            value = property.GetValue(item, null);

            if (value == null)
            {
                return null;
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
            item = value;
            expression = childExpression;
        }
    }
}