using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>Override type for derivable object</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OverrideType
{
    /// <summary>Active override</summary>
    Active = 0,

    /// <summary>Inactive override</summary>
    Inactive = 1
}