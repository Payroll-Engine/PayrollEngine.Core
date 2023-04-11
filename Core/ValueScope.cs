using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The scope for a payroll value</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ValueScope
{
    /// <summary>Local case value available for one division</summary>
    Local = 0,

    /// <summary>Global case value available for all divisions</summary>
    Global = 1
}