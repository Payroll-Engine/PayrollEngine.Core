using System;
using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>Configuration for the payroll calendar</summary>
public class CalendarConfiguration : IEquatable<CalendarConfiguration>
{
    /// <summary>The first month of a year</summary>
    public Month FirstMonthOfYear { get; set; } = Month.January;

    /// <summary>The working days</summary>
    public IList<DayOfWeek> WorkingDays { get; set; }

    /// <summary>Average month days</summary>
    public decimal AverageMonthDays { get; set; }

    /// <summary>Average working days</summary>
    public decimal AverageWorkDays { get; set; }

    /// <summary>The calendar calculation mode</summary>
    public CalendarCalculationMode CalculationMode { get; set; } = CalendarCalculationMode.MonthCalendarDay;

    /// <summary>Calendar week rule</summary>
    public CalendarWeekRule CalendarWeekRule { get; set; } = CalendarWeekRule.FirstFourDayWeek;

    /// <summary>First day of week</summary>
    public DayOfWeek FirstDayOfWeek { get; set; } = DayOfWeek.Monday;

    /// <summary>Initializes a new instance</summary>
    public CalendarConfiguration()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CalendarConfiguration(CalendarConfiguration copySource) =>
        CopyTool.CopyProperties(copySource, this);

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public bool Equals(CalendarConfiguration compare) =>
        CompareTool.EqualProperties(this, compare);
}