using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The collect type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CollectType
{
    /// <summary>Values summary</summary>
    Summary = 0,

    /// <summary>Minimum value</summary>
    Minimum = 1,

    /// <summary>Maximum value</summary>
    Maximum = 2,

    /// <summary>Values average</summary>
    Average = 3,

    /// <summary>Values range: maximum - minimum</summary>
    Range = 4,

    /// <summary>Values count</summary>
    Count = 5
}