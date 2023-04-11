using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The case result kind</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReportAttributeMode
{
    /// <summary>Attributes as JSON</summary>
    Json,

    /// <summary>Attributes as Table</summary>
    Table
}