using System;

namespace PayrollEngine;

/// <summary>Date specifications and base functions</summary>
public static class Date
{
    #region Value

    /// <summary>Represents the smallest possible value of a time instant</summary>
    public static DateTime MinValue => DateTime.MinValue.ToUtc();

    /// <summary>Represents the largest possible value of a time instant</summary>
    public static DateTime MaxValue => DateTime.MaxValue.ToUtc();

    /// <summary>Gets a time instant that is set to the current date and time</summary>
    public static DateTime Now => DateTime.UtcNow;

    /// <summary>Gets a time instant that is set to the current day</summary>
    public static DateTime Today => Now.Date;

    /// <summary>Get the previous tick</summary>
    /// <param name="dateTime">The source date time</param>
    /// <returns>The previous tick</returns>
    public static DateTime PreviousTick(DateTime dateTime) =>
        dateTime.AddTicks(-1);

    /// <summary>Get the next tick</summary>
    /// <param name="dateTime">The source date time</param>
    /// <returns>The next tick</returns>
    public static DateTime NextTick(DateTime dateTime) =>
        dateTime.AddTicks(1);

    /// <summary>Test if the date is midnight.
    /// See https://stackoverflow.com/questions/681435/what-is-the-best-way-to-determine-if-a-system-datetime-is-midnight
    /// </summary>
    /// <param name="moment">The moment to test</param>
    /// <returns>True in case the moment is date</returns>
    public static bool IsMidnight(DateTime moment) =>
        moment.TimeOfDay.Ticks == 0;

    #endregion

    #region Calendar

    /// <summary>Number of semi years in a year</summary>
    public static readonly int SemiYearsInYear = 2;

    /// <summary>Number of quarters in a year</summary>
    public static readonly int QuartersInYear = 4;

    /// <summary>Number of bi months in a year</summary>
    public static readonly int BiMonthsInYear = 6;

    /// <summary>Number of calendar months in a year</summary>
    public static readonly int MonthsInYear = 12;

    /// <summary>Number of lunisolar months in a year</summary>
    public static readonly int LunisolarMonthsInYear = 13;

    /// <summary>Number of semi months in a year</summary>
    public static readonly int SemiMonthsInYear = 24;

    /// <summary>Days in semi month</summary>
    public static readonly int DaysInSemiMonth = 15;

    /// <summary>Number of bi weeks in a year</summary>
    public static readonly int BiWeeksInYear = 26;

    /// <summary>Number of weeks in a year</summary>
    public static readonly int WeeksInYear = 52;

    /// <summary>Number of months in a half year</summary>
    public static readonly int MonthsInSemiYear = MonthsInYear / 2;

    /// <summary>Number of months in a quarter</summary>
    public static readonly int MonthsInQuarter = 3;

    /// <summary>Number of weeks in a lunisolar month</summary>
    public static readonly int WeeksInLunisolarMonth = 4;

    /// <summary>Number of days in a week</summary>
    public static readonly int DaysInWeek = 7;

    /// <summary>Number of days in two week</summary>
    public static readonly int DaysInBiWeek = DaysInWeek * 2;

    /// <summary>Number of days in a lunisolar month</summary>
    public static readonly int DaysInLunisolarMonth = WeeksInLunisolarMonth * DaysInWeek;

    /// <summary>First month in year</summary>
    public static readonly int FirstMonthOfCalendarYear = 1;

    /// <summary>First day in month</summary>
    public static readonly int FirstDayOfMonth = 1;

    /// <summary>Last month in year</summary>
    public static readonly int LastMonthOfCalendarYear = MonthsInYear;

    /// <summary>Returns the number of days in the specified month and year</summary>
    /// <param name="year">The year</param>
    /// <param name="month">The month (a number ranging from 1 to 12)</param>
    /// <returns>The number of days in <paramref name="month" /> for the specified <paramref name="year" />.
    /// For example, if <paramref name="month" /> equals 2 for February, the return value is 28 or 29 depending upon whether <paramref name="year" /> is a leap year</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///         <paramref name="month" /> is less than 1 or greater than 12
    /// -or-
    /// <paramref name="year" /> is less than 1 or greater than 9999</exception>
    public static int DaysInMonth(int year, int month) =>
        DateTime.DaysInMonth(year, month);

    /// <summary>Returns the number of days in the month of a specific date</summary>
    /// <param name="date">The date</param>
    /// <returns>The number of days in for the specified <paramref name="date" /></returns>
    public static int DaysInMonth(DateTime date) =>
        DateTime.DaysInMonth(date.Year, date.Month);

    #endregion

    #region Periods

    /// <summary>Test if two dates represents a period</summary>
    /// <param name="start">The period start</param>
    /// <param name="end">The period end</param>
    /// <returns>True if start and end represents a period</returns>
    public static bool IsPeriod(DateTime start, DateTime end) =>
        end.Ticks - start.Ticks > 1;

    /// <summary>Test if a specific time moment is within a period</summary>
    /// <param name="start">The period start</param>
    /// <param name="end">The period end</param>
    /// <param name="test">The moment to test</param>
    /// <returns>True if the test is within start and end</returns>
    public static bool IsWithin(DateTime start, DateTime end, DateTime test) =>
        start < end ?
            test >= start && test <= end :
            test >= end && test <= start;

    /// <summary>Round the last moment to the next unit</summary>
    /// <param name="moment">Moment to round</param>
    public static DateTime RoundLastMoment(DateTime moment) =>
        IsLastMomentOfDay(moment) ? moment.Date.AddDays(1) : moment;

    #endregion

    #region Year Periods

    /// <summary>Return the first moment of a year</summary>
    /// <param name="year">The moment year</param>
    /// <returns><seealso cref="DateTime"/> from the first moment in a year</returns>
    public static DateTime FirstMomentOfYear(int year) =>
        new(year, FirstMonthOfCalendarYear, FirstDayOfMonth);

    /// <summary>Test if the date is the first moment of the year</summary>
    /// <param name="moment">Moment to test</param>
    /// <returns>True on the last moment of the year</returns>
    public static bool IsFirstMomentOfYear(DateTime moment) =>
        moment.Month == FirstMonthOfCalendarYear &&
        moment.Day == FirstDayOfMonth &&
        IsMidnight(moment);

    /// <summary>Return the last moment of a year</summary>
    /// <param name="year">The moment year</param>
    /// <returns><seealso cref="DateTime"/> from the latest moment in a month</returns>
    public static DateTime LastMomentOfYear(int year) =>
        LastMomentOfDay(new(year, LastMonthOfCalendarYear, DaysInMonth(year, LastMonthOfCalendarYear)));

    /// <summary>Test if the date is the last moment of the year</summary>
    /// <param name="moment">Moment to test</param>
    /// <returns>True on the last moment of the year</returns>
    public static bool IsLastMomentOfYear(DateTime moment) =>
        moment.Month == LastMonthOfCalendarYear &&
        moment.Day == DaysInMonth(moment) &&
        IsLastMomentOfDay(moment);

    #endregion

    #region Month Periods

    /// <summary>Return the first moment of a month</summary>
    /// <param name="year">The moment year</param>
    /// <param name="month">The moment month</param>
    /// <returns><seealso cref="DateTime"/> from the first moment in a month</returns>
    public static DateTime FirstMomentOfMonth(int year, int month) =>
        new(year, month, FirstDayOfMonth);

    /// <summary>Test if the date is the first moment of the month</summary>
    /// <param name="moment">Moment to test</param>
    /// <returns>True on the last moment of the month</returns>
    public static bool IsFirstMomentOfMonth(DateTime moment) =>
        moment.Day == FirstDayOfMonth &&
        IsMidnight(moment);

    /// <summary>Return the last moment of a month</summary>
    /// <param name="year">The moment year</param>
    /// <param name="month">The moment month</param>
    /// <returns><seealso cref="DateTime"/> from the latest moment in a month</returns>
    public static DateTime LastMomentOfMonth(int year, int month) =>
        LastMomentOfDay(new(year, month, DaysInMonth(year, month)));

    /// <summary>Test if the date is the last moment of the month</summary>
    /// <param name="moment">Moment to test</param>
    /// <returns>True on the last moment of the month</returns>
    public static bool IsLastMomentOfMonth(DateTime moment) =>
        moment.Day == DaysInMonth(moment) &&
        IsLastMomentOfDay(moment);

    #endregion

    #region Day Periods

    /// <summary>Return the first moment of the day</summary>
    /// <param name="moment">Moment within the day</param>
    /// <returns><seealso cref="System.DateTime"/> from the first moment in a day</returns>
    public static DateTime FirstMomentOfDay(DateTime moment) =>
        moment.Date;

    /// <summary>Test if the date is the first moment of the day</summary>
    /// <param name="moment">Moment to test</param>
    /// <returns>True on the last moment of the day</returns>
    public static bool IsFirstMomentOfDay(DateTime moment) =>
        IsMidnight(moment);

    /// <summary>Return the last moment of the day</summary>
    /// <param name="moment">Moment within the day</param>
    /// <returns><seealso cref="System.DateTime"/> from the latest moment in a day</returns>
    public static DateTime LastMomentOfDay(DateTime moment) =>
        moment.Date.AddTicks(TimeSpan.TicksPerDay).PreviousTick();

    /// <summary>Test if the date is the last moment of the day</summary>
    /// <param name="moment">Moment to test</param>
    /// <returns>True on the last moment of the day</returns>
    public static bool IsLastMomentOfDay(DateTime moment) =>
        Equals(LastMomentOfDay(moment), moment);

    #endregion

    #region Conversion

    /// <summary>Format a compact date (removes empty time parts)</summary>
    /// <param name="moment">The period start date</param>
    /// <returns>The formatted period start date</returns>
    public static string ToCompactString(DateTime moment) =>
        IsMidnight(moment) ? moment.ToShortDateString() : $"{moment.ToShortDateString()} {moment.ToShortTimeString()}";

    /// <summary>Format a period start date (removes empty time parts)</summary>
    /// <param name="start">The period start date</param>
    /// <returns>The formatted period start date</returns>
    public static string ToPeriodStartString(DateTime start) =>
        ToCompactString(start);

    /// <summary>Format a period end date (removed empty time parts, and round last moment values)</summary>
    /// <param name="end">The period end date</param>
    /// <returns>The formatted period end date</returns>
    public static string ToPeriodEndString(DateTime end) =>
        IsMidnight(end) || IsLastMomentOfDay(end) ? $"{end.ToShortDateString()} {end.ToShortTimeString()}" : end.ToShortDateString();

    /// <summary>Convert a date into the UTC value.
    /// Dates (without time part) are used without time adaption/// </summary>
    /// <param name="moment">The source date time</param>
    /// <returns>The UTC date time</returns>
    public static DateTime ToUtc(DateTime moment)
    {
        return moment.Kind switch
        {
            DateTimeKind.Utc =>
                // already utc
                moment,
            DateTimeKind.Unspecified =>
                // specify kind
                DateTime.SpecifyKind(moment, DateTimeKind.Utc),
            DateTimeKind.Local =>
                // convert to utc
                moment.ToUniversalTime(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>Convert a date into the UTC string value</summary>
    /// <param name="dateTime">The date time</param>
    /// <param name="provider">The format provider</param>
    /// <returns>The UTC date time string</returns>
    public static string ToUtcString(DateTime dateTime, IFormatProvider provider) =>
        ToUtc(dateTime).ToString("o", provider);

    #endregion

    #region Compare

    /// <summary>Test if two dates are within the same second</summary>
    /// <param name="left">The left date to compare</param>
    /// <param name="right">The right left date to compare</param>
    /// <returns></returns>
    public static bool SameSecond(DateTime left, DateTime right) =>
        left.Year == right.Year && left.Month == right.Month && left.Day == right.Day &&
        left.Hour == right.Hour && left.Minute == right.Minute && left.Second == right.Second;

    #endregion
}