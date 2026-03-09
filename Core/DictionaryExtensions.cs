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
    /// <param name="source">The source dictionary</param>
    extension(IDictionary<string, object> source)
    {
        /// <summary>Get typed value from a string/object dictionary</summary>
        /// <param name="key">The item key</param>
        /// <returns>The key value if available, otherwise the default value of the type</returns>
        public T GetValue<T>(string key) => source.GetValue<T>(key, default);

        /// <summary>Get typed value from a string/object dictionary</summary>
        /// <param name="key">The item key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The key value if available, otherwise the default value</returns>
        public T GetValue<T>(string key, T defaultValue)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);

            if (source == null || !source.TryGetValue(key, out var value))
            {
                return defaultValue;
            }

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
    }

    /// <param name="source">The dictionary to test</param>
    extension<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
        /// <summary>Test if a dictionary is empty</summary>
        /// <returns>True if the dictionary is null or empty</returns>
        public bool IsNullOrEmpty() =>
            source == null || !source.Any();

        /// <summary>Copy a dictionary, including null check</summary>
        /// <returns>A new dictionary with the source items</returns>
        public Dictionary<TKey, TValue> Copy() =>
            source == null ? null : new Dictionary<TKey, TValue>(source);

        /// <summary>Copy all dictionary value to another dictionary</summary>
        /// <param name="target">The copy target</param>
        public void CopyTo(Dictionary<TKey, TValue> target)
        {
            ArgumentNullException.ThrowIfNull(target);
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
        /// <param name="key">The key to merge</param>
        /// <param name="value">The value to merge</param>
        public IReadOnlyDictionary<TKey, TValue> ToReadOnly(TKey key, TValue value)
        {
            var mergeDictionary = new Dictionary<TKey, TValue> { { key, value } };
            source?.CopyTo(mergeDictionary);
            return new ReadOnlyDictionary<TKey, TValue>(mergeDictionary);
        }

        /// <summary>Dictionary as text</summary>
        /// <returns>True if the dictionary is null or empty</returns>
        public string ToText()
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
}