using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The payrun job result</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PayrunJobResult
{
    /// <summary>All results</summary>
    Full = 0,

    /// <summary>Changed period values only</summary>
    Incremental = 1
}