using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The action source</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ActionSource
{
    /// <summary>Script</summary>
    Script = 0,

    /// <summary>System</summary>
    System = 1
}