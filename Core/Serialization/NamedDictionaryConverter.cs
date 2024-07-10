using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PayrollEngine.Serialization;

/// <summary>JSON converter for <see cref="Dictionary{TKey,TValue}"/></summary>
public class NamedDictionaryConverter : JsonConverterFactory
{
    /// <summary>Determines whether the type can be converted</summary>
    /// <param name="typeToConvert">The type is checked to whether it can be converted</param>
    /// <returns>True if the type can be converted</returns>
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        if (typeToConvert.GetGenericTypeDefinition() != typeof(Dictionary<,>))
        {
            return false;
        }

        return typeToConvert.GetGenericArguments()[0] == typeof(string);
    }

    /// <summary>Create a converter for the provided <see cref="Type"/></summary>
    /// <param name="typeToConvert">The <see cref="Type"/> being converted</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> being used</param>
    /// <returns>An instance of a <see cref="JsonConverter{T}"/> where T is compatible with <paramref name="typeToConvert"/></returns>
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var keyType = typeToConvert.GetGenericArguments()[0];
        if (keyType != typeof(string))
        {
            throw new ArgumentException("Named dictionary key must be string");
        }
        var valueType = typeToConvert.GetGenericArguments()[1];

        var converter = (JsonConverter)Activator.CreateInstance(
            typeof(NamedDictionaryConverterType<,>).MakeGenericType(keyType, valueType),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: [options],
            culture: null);

        return converter;
    }
}