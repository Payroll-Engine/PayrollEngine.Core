using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollEngine;

/// <summary>Day extensions for <see cref="DateTime"/></summary>
public static class DateTimeDayExtensions
{
    /// <param name="moment">The moment to test</param>
    extension(DateTime moment)
    {
        /// <summary>Ensure date is thew last moment of the day</summary>
        /// <returns>The last moment of the day</returns>
        public DateTime EnsureLastMomentOfDay() =>
            moment.IsMidnight() ? moment.AddDays(-1).LastMomentOfDay() : moment;

        /// <summary>Return the first moment of the day</summary>
        /// <returns><seealso cref="System.DateTime"/> from the first moment in a day</returns>
        public DateTime FirstMomentOfDay() =>
            Date.FirstMomentOfDay(moment);

        /// <summary>Test if the date is the first moment of the day</summary>
        /// <returns>True on the last moment of the day</returns>
        public bool IsFirstMomentOfDay() =>
            Date.IsFirstMomentOfDay(moment);

        /// <summary>Return the last moment of the day</summary>
        /// <returns><seealso cref="System.DateTime"/> from the latest moment in a day</returns>
        public DateTime LastMomentOfDay() =>
            Date.LastMomentOfDay(moment);

        /// <summary>Test if the date is the last moment of the day.
        /// Compare the day of the next tick with the current day</summary>
        /// <returns>True on the last moment of the day</returns>
        public bool IsLastMomentOfDay() =>
            Date.IsLastMomentOfDay(moment);

        /// <summary>Test if date is the first day of year</summary>
        /// <returns>Return true if the date is in the first dya of the year</returns>
        public bool IsFirstDayOfCalendarYear() =>
            moment.Month == Date.FirstMonthOfCalendarYear && moment.IsFirstDayOfMonth();

        /// <summary>Test if date is the last day of year</summary>
        /// <returns>Return true if the date is in the first dya of the year</returns>
        public bool IsLastDayOfCalendarYear() =>
            moment.Month == Date.LastMonthOfCalendarYear && moment.IsLastDayOfMonth();

        /// <summary>Test if date is the first day of month</summary>
        /// <returns>Return true if the date is in the first dya of the year</returns>
        public bool IsFirstDayOfMonth() =>
            moment.Day == Date.FirstDayOfMonth;

        /// <summary>Test if date is the last day of month</summary>
        /// <returns>Return true if the date is in the first dya of the year</returns>
        public bool IsLastDayOfMonth() =>
            moment.Day == Date.DaysInMonth(moment.Year, moment.Month);

        /// <summary>Get the previous matching day</summary>
        /// <param name="dayOfWeek">Target day of week</param>
        /// <returns>The previous matching day</returns>
        public DateTime GetPreviousWeekDay(DayOfWeek dayOfWeek)
        {
            while (moment.DayOfWeek != (System.DayOfWeek)dayOfWeek)
            {
                moment = moment.AddDays(-1);
            }
            return moment;
        }

        /// <summary>Get the next matching day</summary>
        /// <param name="dayOfWeek">Target day of week</param>
        /// <returns>The next matching day</returns>
        public DateTime GetNextWeekDay(DayOfWeek dayOfWeek)
        {
            while (moment.DayOfWeek != (System.DayOfWeek)dayOfWeek)
            {
                moment = moment.AddDays(1);
            }
            return moment;
        }

        /// <summary>Test for working day</summary>
        /// <param name="days">Available days</param>
        /// <returns>True if date is a working day</returns>
        public bool IsDayOfWeek(IEnumerable<DayOfWeek> days) =>
            days.Contains((DayOfWeek)moment.DayOfWeek);

        /// <summary>Get past days count since the minimum available date</summary>
        /// <returns>Day count since <see cref="DateTime.MinValue"/></returns>
        public int GetPastDaysCount() =>
            (int)moment.Date.Subtract(DateTime.MinValue.Date).TotalDays;
    }
}