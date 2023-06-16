using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The calendar week mode</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CalendarWeekMode
{
    /// <summary>Use the days of the entire week</summary>
    Week = 0,

    /// <summary>Use the working days from the payroll calendar</summary>
    WorkWeek = 1
}
