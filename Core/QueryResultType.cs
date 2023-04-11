using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The query result type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum QueryResultType
{
    /// <summary>Items only</summary>
    Items = 0,

    /// <summary>Count only</summary>
    Count = 1,

    /// <summary>Items and count</summary>
    ItemsWithCount = 2
}