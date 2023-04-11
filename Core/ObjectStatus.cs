using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The object status</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ObjectStatus
{
    /// <summary>Object is active</summary>
    Active = 0,

    /// <summary>Object is inactive</summary>
    Inactive = 1
}