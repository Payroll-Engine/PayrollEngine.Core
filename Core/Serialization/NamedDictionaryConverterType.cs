using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PayrollEngine.Serialization;

/// <summary>
/// Named dictionary inner converter type
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public sealed class NamedDictionaryConverterType<TKey, TValue> :
    JsonConverter<Dictionary<TKey, TValue>> where TKey : class
{
    /// <summary>
    /// Serializer constructor
    /// </summary>
    /// <param name="options">The serializer options</param>
    // ReSharper disable once UnusedParameter.Local
    public NamedDictionaryConverterType(JsonSerializerOptions options)
    {
    }

    /// <summary>
    /// Read named dictionary
    /// </summary>
    /// <param name="reader">The json reader</param>
    /// <param name="typeToConvert">The type to convert</param>
    /// <param name="options">The deserialization options</param>
    /// <returns>The named dictionary</returns>
    public override Dictionary<TKey, TValue> Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        var dictionary = new Dictionary<TKey, TValue>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return dictionary;
            }

            // key
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            if (!(reader.GetString() is TKey key))
            {
                throw new ArgumentException("Named dictionary key must be string");
            }

            // value
            TValue value = System.Text.Json.JsonSerializer.Deserialize<TValue>(ref reader, options);
            // dictionary
            dictionary.Add(key, value);
        }

        throw new JsonException();
    }

    /// <summary>
    /// Write named dictionary
    /// </summary>
    /// <param name="writer">The json writer</param>
    /// <param name="values">The named dictionary to write</param>
    /// <param name="options">The deserialization options</param>
    public override void Write(Utf8JsonWriter writer,
        Dictionary<TKey, TValue> values, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        foreach (var value in values)
        {
            var propertyName = value.Key.ToString();
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                writer.WritePropertyName(propertyName);
                System.Text.Json.JsonSerializer.Serialize(writer, value.Value, options);
            }
        }
        writer.WriteEndObject();
    }
}