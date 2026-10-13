using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The assignment of a payrun period to a cycle and to the regulation date</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CalendarCycleAssignment
{
    /// <summary>The pay date determines the cycle and the regulation date (e.g. tax year by payment)</summary>
    PayDate = 0,

    /// <summary>The period end determines the cycle and the regulation date (e.g. tax year by pay period)</summary>
    PeriodEnd = 1
}
