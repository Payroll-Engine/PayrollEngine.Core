using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The payroll value base types (JSON compatible)</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ValueBaseType
{
    /// <summary>String</summary>
    String = 0,

    /// <summary>Number</summary>
    Number = 1,

    /// <summary>Boolean</summary>
    Boolean = 2,

    /// <summary>Object</summary>
    Object = 3,

    /// <summary>Array</summary>
    Array = 4,

    /// <summary>No value type available</summary>
    Null = 5
}