
namespace PayrollEngine;

/// <summary>Extensions for <see cref="CaseFieldTimeType"/></summary>
public static class CaseValueTimeTypeExtensions
{
    /// <summary>Test if time type is a calendar period</summary>
    /// <param name="timeType">The time type</param>
    /// <returns>True for time type is scalable</returns>
    public static bool IsCalendarPeriod(this CaseFieldTimeType timeType) =>
        timeType == CaseFieldTimeType.CalendarPeriod;
}