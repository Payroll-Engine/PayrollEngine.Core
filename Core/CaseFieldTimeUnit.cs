using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The date type for a regulation case field</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseFieldTimeUnit
{
    /// <summary>Date is representing a day</summary>
    Day = 0,

    /// <summary>Dates is representing a half day</summary>
    HalfDay = 1,

    /// <summary>Dates is representing a month</summary>
    Month = 2
}