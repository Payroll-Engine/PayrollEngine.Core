using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The payrun retro pay mode</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RetroPayMode
{
    /// <summary>Retro pay calculation starting from the period containing the oldest value change (start date)</summary>
    ValueChange = 0,

    /// <summary>No retro pay calculation</summary>
    None = 1
}