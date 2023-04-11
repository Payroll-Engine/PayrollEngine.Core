using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The payrun retro type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RetroTimeType
{
    /// <summary>Retro calculation without time limits</summary>
    Anytime = 0,

    /// <summary>Retro calculation within the current cycle</summary>
    Cycle = 1
}