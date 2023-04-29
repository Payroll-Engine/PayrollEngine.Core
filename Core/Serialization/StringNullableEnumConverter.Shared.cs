using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PayrollEngine.Serialization;

/// <summary>JSON converter for nullable enums.
/// See https://stackoverflow.com/questions/59379896/does-jsonstringenumconverter-system-text-json-support-null-values
/// </summary>
/// <typeparam name="T"></typeparam>
public class StringNullableEnumConverter<T> : JsonConverter<T>
{
    private readonly JsonConverter<T> converter;
    private readonly Type enumType;

    /// <inheritdoc/>
    public StringNullableEnumConverter() :
        this(null)
    {
    }

    /// <inheritdoc/>
    public StringNullableEnumConverter(JsonSerializerOptions options)
    {
        // for performance, use the existing converter if available
        if (options != null)
        {
            converter = (JsonConverter<T>)options.GetConverter(typeof(T));
        }

        // cache the underlying type
        enumType = typeof(T).GetNullableType();
    }

    /// <summary>Determines whether the type can be converted</summary>
    /// <param name="typeToConvert">The type is checked as to whether it can be converted</param>
    /// <returns>True if the type can be converted</returns>
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(T).IsAssignableFrom(typeToConvert);
    }

    /// <summary>Read and convert the JSON to T</summary>
    /// <remarks>A converter may throw any Exception, but should throw <cref>JsonException</cref> when the JSON is invalid</remarks>
    /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from</param>
    /// <param name="typeToConvert">The <see cref="Type"/> being converted</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> being used</param>
    /// <returns>The value that was converted</returns>
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (converter != null)
        {
            return converter.Read(ref reader, enumType, options);
        }

        var value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
        {
            return default;
        }

        // for performance, parse with ignoreCase:false first.
        if (!Enum.TryParse(enumType, value, ignoreCase: false, out var result)
            && !Enum.TryParse(enumType, value, ignoreCase: true, out result))
        {
            throw new JsonException($"Unable to convert \"{value}\" to Enum \"{enumType}\"");
        }

        return (T)result;
    }

    /// <summary>Write the value as JSON</summary>
    /// <remarks>A converter may throw any Exception, but should throw <cref>JsonException</cref> when the JSON cannot be created</remarks>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write to</param>
    /// <param name="value">The value to convert</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> being used</param>
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}