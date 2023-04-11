using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollEngine;

/// <summary>Day extensions for <see cref="DateTime"/></summary>
public static class DateTimeDayExtensions
{
    /// <summary>Ensure date is thew last moment of the day</summary>
    /// <param name="moment">The moment to test</param>
    /// <returns>The last moment of the day</returns>
    public static DateTime EnsureLastMomentOfDay(this DateTime moment) =>
        moment.IsMidnight() ? moment.AddDays(-1).LastMomentOfDay() : moment;

    /// <summary>Return the first moment of the day</summary>
    /// <param name="dateTime">The date time</param>
    /// <returns><seealso cref="System.DateTime"/> from the first moment in a day</returns>
    public static DateTime FirstMomentOfDay(this DateTime dateTime) =>
        Date.FirstMomentOfDay(dateTime);

    /// <summary>Test if the date is the first moment of the day</summary>
    /// <param name="dateTime">The date time</param>
    /// <returns>True on the last moment of the day</returns>
    public static bool IsFirstMomentOfDay(this DateTime dateTime) =>
        Date.IsFirstMomentOfDay(dateTime);

    /// <summary>Return the last moment of the day</summary>
    /// <param name="dateTime">The date time</param>
    /// <returns><seealso cref="System.DateTime"/> from the latest moment in a day</returns>
    public static DateTime LastMomentOfDay(this DateTime dateTime) =>
        Date.LastMomentOfDay(dateTime);

    /// <summary>Test if the date is the last moment of the day.
    /// Compare the day of the next tick with the current day</summary>
    /// <param name="dateTime">The date time</param>
    /// <returns>True on the last moment of the day</returns>
    public static bool IsLastMomentOfDay(this DateTime dateTime) =>
        Date.IsLastMomentOfDay(dateTime);

    /// <summary>Test if date is the first day of year</summary>
    /// <param name="date">The date to test</param>
    /// <returns>Return true if the date is in the first dya of the year</returns>
    public static bool IsFirstDayOfCalendarYear(this DateTime date) =>
        date.Month == Date.FirstMonthOfCalendarYear && IsFirstDayOfMonth(date);

    /// <summary>Test if date is the last day of year</summary>
    /// <param name="date">The date to test</param>
    /// <returns>Return true if the date is in the first dya of the year</returns>
    public static bool IsLastDayOfCalendarYear(this DateTime date) =>
        date.Month == Date.LastMonthOfCalendarYear && IsLastDayOfMonth(date);

    /// <summary>Test if date is the first day of month</summary>
    /// <param name="date">The date to test</param>
    /// <returns>Return true if the date is in the first dya of the year</returns>
    public static bool IsFirstDayOfMonth(this DateTime date) =>
        date.Day == Date.FirstDayOfMonth;

    /// <summary>Test if date is the last day of month</summary>
    /// <param name="date">The date to test</param>
    /// <returns>Return true if the date is in the first dya of the year</returns>
    public static bool IsLastDayOfMonth(this DateTime date) =>
        date.Day == Date.DaysInMonth(date.Year, date.Month);

    /// <summary>Get the previous matching day</summary>
    /// <param name="date">The date to start</param>
    /// <param name="dayOfWeek">Target day of week</param>
    /// <returns>The previous matching day</returns>
    public static DateTime GetPreviousWeekDay(this DateTime date, DayOfWeek dayOfWeek)
    {
        while (date.DayOfWeek != (System.DayOfWeek)dayOfWeek)
        {
            date = date.AddDays(-1);
        }
        return date;
    }

    /// <summary>Get the next matching day</summary>
    /// <param name="date">The date to start</param>
    /// <param name="dayOfWeek">Target day of week</param>
    /// <returns>The next matching day</returns>
    public static DateTime GetNextWeekDay(this DateTime date, DayOfWeek dayOfWeek)
    {
        while (date.DayOfWeek != (System.DayOfWeek)dayOfWeek)
        {
            date = date.AddDays(1);
        }
        return date;
    }

    /// <summary>Test for working day</summary>
    /// <param name="date">The date to test</param>
    /// <param name="days">Available days</param>
    /// <returns>True if date is a working day</returns>
    public static bool IsDayOfWeek(this DateTime date, IEnumerable<DayOfWeek> days) =>
        days.Contains((DayOfWeek)date.DayOfWeek);

    /// <summary>Get past days count since the minimum available date</summary>
    /// <param name="date">The date to count</param>
    /// <returns>Day count since <see cref="DateTime.MinValue"/></returns>
    public static int GetPastDaysCount(this DateTime date) =>
        (int)date.Date.Subtract(DateTime.MinValue.Date).TotalDays;
}