using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The calendar time unit</summary>
/// <remarks>The enum value is used as year divisor
/// see also https://docs.oracle.com/cd/A60725_05/html/comnls/us/per/pylgp02.htm</remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CalendarTimeUnit
{
    /// <summary>Year</summary>
    Year = 1,

    /// <summary>Semi year</summary>
    SemiYear = 2,

    /// <summary>Quarter</summary>
    Quarter = 4,

    /// <summary>Bi month</summary>
    BiMonth = 6,

    /// <summary>Calendar month</summary>
    CalendarMonth = 12,

    /// <summary>Lunisolar month</summary>
    LunisolarMonth = 13,

    /// <summary>Semi month</summary>
    SemiMonth = 24,

    /// <summary>Bi week</summary>
    BiWeek = 26,

    /// <summary>Week</summary>
    Week = 52
}