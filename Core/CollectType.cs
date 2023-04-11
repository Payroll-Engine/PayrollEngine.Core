using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The collect type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CollectType
{
    /// <summary>Values summary</summary>
    Sum = 0,

    /// <summary>Minimum value</summary>
    Min = 1,

    /// <summary>Maximum value</summary>
    Max = 2,

    /// <summary>Values average</summary>
    Average = 3,

    /// <summary>Values count</summary>
    Count = 4
}