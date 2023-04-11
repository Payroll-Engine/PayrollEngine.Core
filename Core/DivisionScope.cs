using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The division scope</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DivisionScope
{
    /// <summary>All divisions</summary>
    All = 0,

    /// <summary>Local divisions only</summary>
    Local = 1,

    /// <summary>Global divisions only</summary>
    Global = 2,

    /// <summary>Global and local divisions</summary>
    GlobalAndLocal = 3
}