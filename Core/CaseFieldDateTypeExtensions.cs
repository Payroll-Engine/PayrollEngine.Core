using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="CaseFieldDateType"/></summary>
public static class CaseFieldDateTypeExtensions
{
    /// <summary>Test if a start date matches the case field date type</summary>
    /// <param name="dateType">The case field date type</param>
    /// <param name="value">The value to test</param>
    /// <returns>Tru if the start date matches the case field date type</returns>
    public static bool IsStartMatching(this CaseFieldDateType dateType, DateTime value)
    {
        return dateType switch
        {
            CaseFieldDateType.Day =>
                // daily base
                true,
            CaseFieldDateType.Sunday => value.DayOfWeek == System.DayOfWeek.Sunday,
            CaseFieldDateType.Monday => value.DayOfWeek == System.DayOfWeek.Monday,
            CaseFieldDateType.Tuesday => value.DayOfWeek == System.DayOfWeek.Tuesday,
            CaseFieldDateType.Wednesday => value.DayOfWeek == System.DayOfWeek.Wednesday,
            CaseFieldDateType.Thursday => value.DayOfWeek == System.DayOfWeek.Thursday,
            CaseFieldDateType.Friday => value.DayOfWeek == System.DayOfWeek.Friday,
            CaseFieldDateType.Saturday => value.DayOfWeek == System.DayOfWeek.Saturday,
            CaseFieldDateType.Month =>
                // first day of month
                value.Day == 1,
            CaseFieldDateType.January => value.Month == (int)Month.January && value.Day == 1,
            CaseFieldDateType.February => value.Month == (int)Month.February && value.Day == 1,
            CaseFieldDateType.March => value.Month == (int)Month.March && value.Day == 1,
            CaseFieldDateType.April => value.Month == (int)Month.April && value.Day == 1,
            CaseFieldDateType.May => value.Month == (int)Month.May && value.Day == 1,
            CaseFieldDateType.June => value.Month == (int)Month.June && value.Day == 1,
            CaseFieldDateType.July => value.Month == (int)Month.July && value.Day == 1,
            CaseFieldDateType.August => value.Month == (int)Month.August && value.Day == 1,
            CaseFieldDateType.September => value.Month == (int)Month.September && value.Day == 1,
            CaseFieldDateType.October => value.Month == (int)Month.October && value.Day == 1,
            CaseFieldDateType.November => value.Month == (int)Month.November && value.Day == 1,
            CaseFieldDateType.December => value.Month == (int)Month.December && value.Day == 1,
            CaseFieldDateType.Year =>
                // first day of year
                value.Month == 1 && value.Day == 1,
            _ => false
        };
    }

    /// <summary>Test if end date matches the case field date type</summary>
    /// <param name="dateType">The case field date type</param>
    /// <param name="value">The value to test</param>
    /// <returns>Tru if the end date matches the case field date type</returns>
    public static bool IsEndMatching(this CaseFieldDateType dateType, DateTime value)
    {
        var daysInMonth = DateTime.DaysInMonth(value.Year, value.Month);

        return dateType switch
        {
            CaseFieldDateType.Day =>
                // daily base
                true,
            CaseFieldDateType.Sunday => value.DayOfWeek == System.DayOfWeek.Sunday,
            CaseFieldDateType.Monday => value.DayOfWeek == System.DayOfWeek.Monday,
            CaseFieldDateType.Tuesday => value.DayOfWeek == System.DayOfWeek.Tuesday,
            CaseFieldDateType.Wednesday => value.DayOfWeek == System.DayOfWeek.Wednesday,
            CaseFieldDateType.Thursday => value.DayOfWeek == System.DayOfWeek.Thursday,
            CaseFieldDateType.Friday => value.DayOfWeek == System.DayOfWeek.Friday,
            CaseFieldDateType.Saturday => value.DayOfWeek == System.DayOfWeek.Saturday,
            CaseFieldDateType.Month =>
                // last day of month
                value.Day == daysInMonth,
            CaseFieldDateType.January => value.Month == (int)Month.January && value.Day == daysInMonth,
            CaseFieldDateType.February => value.Month == (int)Month.February && value.Day == daysInMonth,
            CaseFieldDateType.March => value.Month == (int)Month.March && value.Day == daysInMonth,
            CaseFieldDateType.April => value.Month == (int)Month.April && value.Day == daysInMonth,
            CaseFieldDateType.May => value.Month == (int)Month.May && value.Day == daysInMonth,
            CaseFieldDateType.June => value.Month == (int)Month.June && value.Day == daysInMonth,
            CaseFieldDateType.July => value.Month == (int)Month.July && value.Day == daysInMonth,
            CaseFieldDateType.August => value.Month == (int)Month.August && value.Day == daysInMonth,
            CaseFieldDateType.September => value.Month == (int)Month.September && value.Day == daysInMonth,
            CaseFieldDateType.October => value.Month == (int)Month.October && value.Day == daysInMonth,
            CaseFieldDateType.November => value.Month == (int)Month.November && value.Day == daysInMonth,
            CaseFieldDateType.December => value.Month == (int)Month.December && value.Day == daysInMonth,
            CaseFieldDateType.Year =>
                // last day of year
                value.Month == (int)Month.December && value.Day == daysInMonth,
            _ => false
        };
    }
}