using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="DateTime"/></summary>
public static class DateTimeExtensions
{
    /// <param name="dateTime">The date time to test</param>
    extension(DateTime dateTime)
    {
        /// <summary>
        /// Test if the date is undefined
        /// </summary>
        /// <returns>True, if the date time is undefined</returns>
        public bool IsUndefined() =>
            dateTime == Date.MinValue;

        /// <summary>Test if the date is defined</summary>
        /// <returns>True, if the date time is defined</returns>
        public bool IsDefined() =>
            !dateTime.IsUndefined();

        /// <summary>Test if the date is midnight</summary>
        /// <returns>True in case the moment is date</returns>
        public bool IsMidnight() =>
            Date.IsMidnight(dateTime);

        /// <summary>Test if a specific time moment is within a date period</summary>
        /// <param name="start">The period start</param>
        /// <param name="end">The period end</param>
        /// <returns>True if the moment is within the start end date</returns>
        public bool IsWithin(DateTime start, DateTime end) =>
            Date.IsWithin(start, end, dateTime);

        /// <summary>Test if the date is in UTC</summary>
        /// <returns>True, date time is UTC</returns>
        public bool IsUtc() =>
            dateTime.Kind == DateTimeKind.Utc;

        /// <summary>Convert a date into the UTC value. Dates (without time part) are used without time adaption</summary>
        /// <returns>The UTC date time</returns>
        public DateTime ToUtc() =>
            Date.ToUtc(dateTime);

        /// <summary>Get a compact <see cref="DateTime"/>, removes empty time value</summary>
        public string ToCompactString() =>
            dateTime.IsMidnight() ?
                dateTime.ToShortDateString() :
                $"{dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}";

        /// <summary>Convert a date into the UTC string value.
        /// See https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip
        /// </summary>
        /// <param name="provider">The format provider</param>
        /// <returns>The UTC date time string</returns>
        public string ToUtcString(IFormatProvider provider) =>
            Date.ToUtcString(dateTime, provider);

        /// <summary>Format a period start date (removes empty time parts)</summary>
        /// <returns>The formatted period start date</returns>
        public string ToPeriodStartString() =>
            Date.ToPeriodStartString(dateTime);

        /// <summary>Format a period end date (removed empty time parts, and round last moment values)</summary>
        /// <returns>The formatted period end date</returns>
        public string ToPeriodEndString() =>
            Date.ToPeriodEndString(dateTime);

        /// <summary>Get the previous tick</summary>
        /// <returns>The previous tick</returns>
        public DateTime PreviousTick() =>
            Date.PreviousTick(dateTime);

        /// <summary>Get the next tick</summary>
        /// <returns>The next tick</returns>
        public DateTime NextTick() =>
            Date.NextTick(dateTime);

        /// <summary>Round the last moment to the next unit</summary>
        public DateTime RoundLastMoment() =>
            Date.RoundLastMoment(dateTime);

        /// <summary>Test if two dates are in the same year</summary>
        /// <param name="compare">The second date to test</param>
        /// <returns>Return true if year and mont of both dates is equal</returns>
        public bool IsSameYear(DateTime compare) =>
            dateTime.Year == compare.Year;

        /// <summary>Test if two dates are in the same year and month</summary>
        /// <param name="compare">The second date to test</param>
        /// <returns>Return true if year and mont of both dates is equal</returns>
        public bool IsSameMonth(DateTime compare) =>
            dateTime.IsSameYear(compare) && dateTime.Month == compare.Month;
    }
}