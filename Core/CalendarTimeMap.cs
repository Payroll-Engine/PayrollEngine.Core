using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The calendar time map</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CalendarTimeMap
{
    /// <summary>Period</summary>
    Period = 0,

    /// <summary>Cycle</summary>
    Cycle = 1
}