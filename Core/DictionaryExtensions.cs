using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="IDictionary{TKey,TValue}"/></summary>
public static class DictionaryExtensions
{
    /// <summary>Get typed value from a string/object dictionary</summary>
    /// <param name="source">The source dictionary</param>
    /// <param name="key">The item key</param>
    /// <returns>The key value if available, otherwise the default value of the type</returns>
    public static T GetValue<T>(this IDictionary<string, object> source, string key) =>
        GetValue<T>(source, key, default);

    /// <summary>Get typed value from a string/object dictionary</summary>
    /// <param name="source">The source dictionary</param>
    /// <param name="key">The item key</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The key value if available, otherwise the default value</returns>
    public static T GetValue<T>(this IDictionary<string, object> source, string key, T defaultValue)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException(nameof(key));
        }

        if (source == null || !source.ContainsKey(key))
        {
            return defaultValue;
        }

        var value = source[key];
        if (value is JsonElement jsonElement)
        {
            value = jsonElement.GetValue();
        }
        if (typeof(T) == typeof(object))
        {
            return (T)value;
        }
        return (T)Convert.ChangeType(value, typeof(T));
    }

    /// <summary>Test if a dictionary is empty</summary>
    /// <param name="source">The dictionary to test</param>
    /// <returns>True if the dictionary is null or empty</returns>
    public static bool IsNullOrEmpty<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) =>
        source == null || !source.Any();

    /// <summary>Copy a dictionary, including null check</summary>
    /// <param name="source">The copy source</param>
    /// <returns>A new dictionary with the source items</returns>
    public static Dictionary<TKey, TValue> Copy<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) =>
        source == null ? null : new Dictionary<TKey, TValue>(source);

    /// <summary>Copy all dictionary value to another dictionary</summary>
    /// <param name="source">The copy source</param>
    /// <param name="target">The copy target</param>
    public static void CopyTo<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Dictionary<TKey, TValue> target)
    {
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }
        if (source == null)
        {
            return;
        }
        foreach (var item in source)
        {
            target[item.Key] = item.Value;
        }
    }

    /// <summary>Merge a value into a read only dictionary</summary>
    /// <param name="source">The copy source</param>
    /// <param name="key">The key to merge</param>
    /// <param name="value">The value to merge</param>
    public static IReadOnlyDictionary<TKey, TValue> ToReadOnly<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source,
        TKey key, TValue value)
    {
        var mergeDictionary = new Dictionary<TKey, TValue> { { key, value } };
        source?.CopyTo(mergeDictionary);
        return new ReadOnlyDictionary<TKey, TValue>(mergeDictionary);
    }

    /// <summary>Dictionary as text</summary>
    /// <param name="source">The dictionary to test</param>
    /// <returns>True if the dictionary is null or empty</returns>
    public static string ToText<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
        if (source == null)
        {
            return null;
        }

        var buffer = new StringBuilder();
        foreach (var value in source)
        {
            if (buffer.Length > 0)
            {
                buffer.Append(", ");
            }
            buffer.Append($"{value.Key}={value.Value}");
        }
        return buffer.ToString();
    }
}