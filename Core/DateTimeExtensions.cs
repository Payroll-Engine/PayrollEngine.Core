using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="DateTime"/></summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Test if the date is undefined
    /// </summary>
    /// <param name="dateTime">The date time to test</param>
    /// <returns>True, if the date time is undefined</returns>
    public static bool IsUndefined(this DateTime dateTime) =>
        dateTime == Date.MinValue;

    /// <summary>Test if the date is defined</summary>
    /// <param name="dateTime">The date time to test</param>
    /// <returns>True, if the date time is defined</returns>
    public static bool IsDefined(this DateTime dateTime) =>
        !IsUndefined(dateTime);

    /// <summary>Test if the date is midnight</summary>
    /// <param name="moment">The moment to test</param>
    /// <returns>True in case the moment is date</returns>
    public static bool IsMidnight(this DateTime moment) =>
        Date.IsMidnight(moment);

    /// <summary>Test if a specific time moment is within a date period</summary>
    /// <param name="moment">The moment to test</param>
    /// <param name="start">The period start</param>
    /// <param name="end">The period end</param>
    /// <returns>True if the moment is within the start end date</returns>
    public static bool IsWithin(this DateTime moment, DateTime start, DateTime end) =>
        Date.IsWithin(start, end, moment);

    /// <summary>Test if the date is in UTC</summary>
    /// <param name="dateTime">The source date time</param>
    /// <returns>True, date time is UTC</returns>
    public static bool IsUtc(this DateTime dateTime) =>
        dateTime.Kind == DateTimeKind.Utc;

    /// <summary>Convert a date into the UTC value. Dates (without time part) are used without time adaption</summary>
    /// <param name="moment">The source date time</param>
    /// <returns>The UTC date time</returns>
    public static DateTime ToUtc(this DateTime moment) =>
        Date.ToUtc(moment);

    /// <summary>Get a compact <see cref="DateTime"/>, removes empty time value</summary>
    /// <param name="dateTime">The date time</param>
    public static string ToCompactString(this DateTime dateTime) =>
        IsMidnight(dateTime) ? dateTime.ToShortDateString() : $"{dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}";

    /// <summary>Convert a date into the UTC string value.
    /// See https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip
    /// </summary>
    /// <param name="dateTime">The date time</param>
    /// <param name="provider">The format provider</param>
    /// <returns>The UTC date time string</returns>
    public static string ToUtcString(this DateTime dateTime, IFormatProvider provider) =>
        Date.ToUtcString(dateTime, provider);

    /// <summary>Format a period start date (removes empty time parts)</summary>
    /// <param name="start">The period start date</param>
    /// <returns>The formatted period start date</returns>
    public static string ToPeriodStartString(this DateTime start) =>
        Date.ToPeriodStartString(start);

    /// <summary>Format a period end date (removed empty time parts, and round last moment values)</summary>
    /// <param name="end">The period end date</param>
    /// <returns>The formatted period end date</returns>
    public static string ToPeriodEndString(this DateTime end) =>
        Date.ToPeriodEndString(end);

    /// <summary>Get the previous tick</summary>
    /// <param name="dateTime">The source date time</param>
    /// <returns>The previous tick</returns>
    public static DateTime PreviousTick(this DateTime dateTime) =>
        Date.PreviousTick(dateTime);

    /// <summary>Get the next tick</summary>
    /// <param name="dateTime">The source date time</param>
    /// <returns>The next tick</returns>
    public static DateTime NextTick(this DateTime dateTime) =>
        Date.NextTick(dateTime);

    /// <summary>Round the last moment to the next unit</summary>
    /// <param name="moment">Moment to round</param>
    public static DateTime RoundLastMoment(this DateTime moment) =>
        Date.RoundLastMoment(moment);

    /// <summary>Test if two dates are in the same year</summary>
    /// <param name="date">The first date to test</param>
    /// <param name="compare">The second date to test</param>
    /// <returns>Return true if year and mont of both dates is equal</returns>
    public static bool IsSameYear(this DateTime date, DateTime compare) =>
        date.Year == compare.Year;

    /// <summary>Test if two dates are in the same year and month</summary>
    /// <param name="date">The first date to test</param>
    /// <param name="compare">The second date to test</param>
    /// <returns>Return true if year and mont of both dates is equal</returns>
    public static bool IsSameMonth(this DateTime date, DateTime compare) =>
        date.IsSameYear(compare) && date.Month == compare.Month;

}