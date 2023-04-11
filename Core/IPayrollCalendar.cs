using System;
using System.Globalization;

namespace PayrollEngine;

/// <summary>Represents a payroll calendar</summary>
public interface IPayrollCalendar
{
    /// <summary>The tenant id</summary>
    int TenantId { get; }

    /// <summary>The user id</summary>
    int? UserId { get; }

    /// <summary>The calendar culture</summary>
    CultureInfo Culture { get; }

    /// <summary>The calendar</summary>
    Calendar Calendar => Culture.Calendar;

    /// <summary>The calendar configuration</summary>
    CalendarConfiguration Configuration { get; }

    /// <summary>Returns the week of year for the specified DateTime</summary>
    /// <param name="moment">The moment of the week</param>
    /// <returns> The returned value is an integer between 1 and 53</returns>
    int GetWeekOfYear(DateTime moment);
}