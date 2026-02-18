using System.Linq;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Collections;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace PayrollEngine.Serialization;

/// <summary>Default JSON serializer</summary>
public static class DefaultJsonSerializer
{
    private static JsonSerializerOptions options => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        IgnoreReadOnlyProperties = true,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers = { ValueFilter }
        },
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

    /// <summary>
    /// Filter empty properties
    /// </summary>
    /// <param name="typeInfo"></param>
    private static void ValueFilter(JsonTypeInfo typeInfo)
    {
        if (typeInfo.Kind != JsonTypeInfoKind.Object)
        {
            return;
        }

        foreach (var property in typeInfo.Properties)
        {
            // ignore empty collections
            // see https://stackoverflow.com/a/73777873/15659039
            if (typeof(ICollection).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = (_, value) =>
                    value is ICollection collection && collection.Count > 0;
                continue;
            }

            // read-only json property
            if (property.AttributeProvider?
                    .GetCustomAttributes(typeof(JsonReadOnlyAttribute), false)
                    .FirstOrDefault() is JsonReadOnlyAttribute)
            {
                property.ShouldSerialize = (_, _) => false;
            }
        }
    }
}