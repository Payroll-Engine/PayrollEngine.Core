using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The lookup range mode</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LookupRangeMode
{
    /// <summary>No lookup range mode</summary>
    None,

    /// <summary>Threshold by range value (first range value must be zero)</summary>
    Threshold,

    /// <summary>Split by range value and factor (first range value must be zero)</summary>
    Progressive
}