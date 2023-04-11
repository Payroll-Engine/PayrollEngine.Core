using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The calculation mode for case value</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CalendarCalculationMode
{
    /// <summary>Divide value by month calendar days</summary>
    MonthCalendarDay = 0,

    /// <summary>Divide value by month average days (eg. 30 days)</summary>
    MonthAverageDay = 1,

    /// <summary>Divide value by effective month working days</summary>
    MonthEffectiveWorkDay = 2,

    /// <summary>Divide value by average month working days (e.g. 21.75 days)</summary>
    MonthAverageWorkDay = 3,

    /// <summary>Divide value by week calendar days</summary>
    WeekCalendarDay = 4
}