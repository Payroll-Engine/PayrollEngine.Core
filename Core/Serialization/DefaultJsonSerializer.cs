using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PayrollEngine.Serialization;

/// <summary>Default JSON serializer</summary>
public static class DefaultJsonSerializer
{
    private static JsonSerializerOptions options => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        IgnoreReadOnlyProperties = true,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters = { new NamedDictionaryConverter() }
    };

    /// <summary>Deserialize object using the default options</summary>
    /// <param name="json">The JSON text</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>Object of type T</returns>
    public static T Deserialize<T>(string json) =>
        System.Text.Json.JsonSerializer.Deserialize<T>(json, options);

    /// <summary>Serialize object using the default options</summary>
    /// <param name="obj">The object to serialize</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>The serialized JSON text</returns>
    public static string Serialize<T>(T obj) =>
        obj != null ? System.Text.Json.JsonSerializer.Serialize(obj, obj.GetType(), options) : null;

    /// <summary>Serialize object to string content using the default options</summary>
    /// <param name="obj">The object to serialize</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>The string content</returns>
    public static StringContent SerializeJson<T>(T obj) =>
        SerializeJson(Serialize(obj));

    /// <summary>
    /// Serialize object to string content using the default options
    /// </summary>
    /// <param name="obj">The object to serialize</param>
    /// <returns>The string content</returns>
    public static StringContent SerializeJson(object obj) =>
        SerializeJson(Serialize(obj));

    /// <summary>Serialize object to string content using the default options</summary>
    /// <param name="json">The JSON text</param>
    /// <returns>The string content</returns>
    public static StringContent SerializeJson(string json) =>
        new(json, Encoding.UTF8, ContentType.Json);
}