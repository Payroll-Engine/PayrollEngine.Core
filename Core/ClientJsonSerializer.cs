using System.Text.Encodings.Web;
using System.Text.Json;

namespace PayrollEngine;

/// <summary>Payroll client JSON serializer</summary>
public static class ClientJsonSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        // support german characters
        // https://stackoverflow.com/questions/58003293/dotnet-core-system-text-json-unescape-unicode-string
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    /// <summary>Deserialize object using the default options</summary>
    /// <param name="json">The JSON text</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>Object of type T</returns>
    public static T Deserialize<T>(string json) where T : class =>
        JsonSerializer.Deserialize<T>(json, Options);

    /// <summary>Serialize object using the default options</summary>
    /// <param name="obj">The object to serialize</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>The serialized JSON text</returns>
    public static string Serialize<T>(T obj) where T : class =>
        JsonSerializer.Serialize(obj, Options);
}