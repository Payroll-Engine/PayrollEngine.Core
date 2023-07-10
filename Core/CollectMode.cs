using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The collect mode</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CollectMode
{
    /// <summary>Summary of values</summary>
    Summary = 0,

    /// <summary>Minimum value</summary>
    Minimum = 1,

    /// <summary>Maximum value</summary>
    Maximum = 2,

    /// <summary>Average of values</summary>
    Average = 3,

    /// <summary>Values range: maximum - minimum</summary>
    Range = 4,

    /// <summary>Values count</summary>
    Count = 5
}