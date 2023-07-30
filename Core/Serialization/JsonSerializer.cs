using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollEngine.Serialization;

/// <summary>Extensions for System.Text.Json.JsonSerializer</summary>
public static class JsonSerializer
{

    #region File

    /// <summary>Deserialize a type from file</summary>
    /// <param name="fileName">The file name</param>
    public static async Task<T> DeserializeFromFileAsync<T>(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }

        // import file
        if (!File.Exists(fileName))
        {
            throw new PayrollException($"Missing json file {fileName}");
        }
        var json = await File.ReadAllTextAsync(fileName);
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new PayrollException($"Invalid import file {fileName}");
        }

        // import from JSON
        var result = DefaultJsonSerializer.Deserialize<T>(json);
        return result;
    }

    #endregion

    #region Resource

    /// <summary>Deserialize an embedded resource</summary>
    /// <param name="type">Type within the resource assembly</param>
    /// <param name="resourceName">The resource name</param>
    public static async Task<T> DeserializeFromResourceAsync<T>(Type type, string resourceName)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        if (string.IsNullOrWhiteSpace(resourceName))
        {
            throw new ArgumentException(nameof(resourceName));
        }

        var assembly = type.Assembly;
        await using Stream stream = assembly.GetManifestResourceStream(type.Namespace + resourceName.EnsureStart("."));
        if (stream == null)
        {
            throw new PayrollException($"Invalid resource {resourceName} in assembly {assembly.FullName}");
        }
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();

        var result = DefaultJsonSerializer.Deserialize<T>(json);
        return result;
    }

    #endregion

    #region List

    /// <summary>Serialize object list</summary>
    /// <param name="value">The list</param>
    /// <returns>String representation of the list, null on undefined/empty list</returns>
    public static string SerializeList<T>(IList<T> value) =>
        value != null && value.Any() ? DefaultJsonSerializer.Serialize(value) : null;

    /// <summary>Deserialize a json to an object list/// </summary>
    /// <param name="json">The json to deserialize</param>
    /// <returns>Deserialized list, null on undefined/empty list</returns>
    public static List<T> DeserializeList<T>(string json) =>
        string.IsNullOrWhiteSpace(json) ?
            null :
            DefaultJsonSerializer.Deserialize<List<T>>(json);

    #endregion

    #region Dictionary

    /// <summary>Prevent 'null' serialization of empty dictionaries</summary>
    /// <param name="values">The dictionary to serialize</param>
    /// <returns>String representation of the dictionary, null on undefined or empty dictionary</returns>
    public static string SerializeNamedDictionary<TValue>(Dictionary<string, TValue> values) =>
        values != null && values.Any() ? System.Text.Json.JsonSerializer.Serialize(values) : null;

    /// <summary>Serialize a string/object dictionary.
    /// replace MS JsonSerializer.Deserialize&lt;Dictionary&lt;string,object&gt;&gt;()
    /// it deserializes the json { "myKey": 86 } into the dictionary item
    /// key: myKey
    /// value: ValueKind = Number : "86"
    /// and not
    /// key: myKey
    /// value: 86
    /// See also https://github.com/dotnet/runtime/issues/30524#issuecomment-539704342
    /// </summary>
    /// <param name="json">The json string to deserialize</param>
    /// <returns>Deserialized dictionary, null on undefined or empty dictionary</returns>
    public static Dictionary<string, TValue> DeserializeNamedDictionary<TValue>(string json) =>
        string.IsNullOrWhiteSpace(json) ? null :
            System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, TValue>>(json);

    #endregion
}