using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The time period type for a regulation case field</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseFieldTimeType
{
    /// <summary>Value without any time restriction (e.g. precautionary duty)</summary>
    Timeless = 0,

    /// <summary>Value applies to a specific time moment (e.g. bonus)</summary>
    Moment = 1,

    /// <summary>Value within a time period (e.g. working hours)</summary>
    Period = 2,

    /// <summary>Value within a time period, mapped to the payroll calendar (e.g. monthly wage)</summary>
    CalendarPeriod = 3
}