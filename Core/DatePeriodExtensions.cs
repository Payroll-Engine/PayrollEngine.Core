using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="DatePeriod"/></summary>
public static class DatePeriodExtensions
{
    /// <summary>Test if a specific time moment is before this period</summary>
    /// <param name="period">The period</param>
    /// <param name="testMoment">The moment to test</param>
    /// <returns>True, if the moment is before this period</returns>
    public static bool IsBefore(this DatePeriod period, DateTime testMoment) =>
        testMoment < period.Start;

    /// <summary>Test if a specific time period is before this period</summary>
    /// <param name="period">The period</param>
    /// <param name="testPeriod">The period to test</param>
    /// <returns>True, if the period is before this period</returns>
    public static bool IsBefore(this DatePeriod period, DatePeriod testPeriod) =>
        testPeriod.End < period.Start;

    /// <summary>Test if a specific time moment is after this period</summary>
    /// <param name="period">The period</param>
    /// <param name="testMoment">The moment to test</param>
    /// <returns>True, if the moment is after this period</returns>
    public static bool IsAfter(this DatePeriod period, DateTime testMoment) =>
        testMoment > period.End;

    /// <summary>Test if a specific time period is after this period</summary>
    /// <param name="period">The period</param>
    /// <param name="testPeriod">The period to test</param>
    /// <returns>True, if the period is after this period</returns>
    public static bool IsAfter(this DatePeriod period, DatePeriod testPeriod) =>
        testPeriod.Start > period.End;

    /// <summary>Test if a specific time moment is within the period, including open periods</summary>
    /// <param name="period">The period</param>
    /// <param name="testMoment">The moment to test</param>
    /// <returns>True, if the moment is within this period</returns>
    public static bool IsWithin(this DatePeriod period, DateTime testMoment) =>
        testMoment.IsWithin(period.Start, period.End);

    /// <summary>Test if a specific time period is within the period, including open periods</summary>
    /// <param name="period">The period</param>
    /// <param name="testPeriod">The period to test</param>
    /// <returns>True, if the test period is within this period</returns>
    public static bool IsWithin(this DatePeriod period, DatePeriod testPeriod) =>
        IsWithin(testPeriod, period.Start) && IsWithin(testPeriod, period.End);

    /// <summary>Test if a specific time moment is within or before the period, including open periods</summary>
    /// <param name="period">The period</param>
    /// <param name="testMoment">The moment to test</param>
    /// <returns>True, if the moment is within or before this period</returns>
    public static bool IsWithinOrBefore(this DatePeriod period, DateTime testMoment) =>
        testMoment <= period.End;

    /// <summary>Test if a specific time moment is within or after the period, including open periods</summary>
    /// <param name="period">The period</param>
    /// <param name="testMoment">The moment to test</param>
    /// <returns>True, if the moment is within or after this period</returns>
    public static bool IsWithinOrAfter(this DatePeriod period, DateTime testMoment) =>
        testMoment >= period.Start;

    /// <summary>Test if period is overlapping this period</summary>
    /// <param name="period">The period</param>
    /// <param name="testPeriod">The period to test</param>
    /// <returns>True, if the period is overlapping this period</returns>
    public static bool IsOverlapping(this DatePeriod period, DatePeriod testPeriod) =>
        period.Start < testPeriod.End && testPeriod.Start < period.End;

    /// <summary>Build the intersection of two date periods</summary>
    /// <param name="period">The period</param>
    /// <param name="intersectPeriod">The period to intersect</param>
    /// <returns>The intersection period, null if no intersection is present</returns>
    public static DatePeriod Intersect(this DatePeriod period, DatePeriod intersectPeriod)
    {
        if (!IsOverlapping(period, intersectPeriod))
        {
            return null;
        }
        return new(
            new(Math.Max(period.Start.Ticks, intersectPeriod.Start.Ticks)),
            new(Math.Min(period.End.Ticks, intersectPeriod.End.Ticks)));
    }

    /// <summary>Split date period by splitting dates</summary>
    /// <param name="period">The period</param>
    /// <param name="splitMoments">The moments used to split</param>
    /// <returns>The splitting periods</returns>
    public static List<DatePeriod> Split(this DatePeriod period, List<DateTime> splitMoments)
    {
        // ensure unique moments, ordered from oldest to newest
        var moments = new HashSet<DateTime>(splitMoments).OrderBy(x => x);

        var splitPeriods = new List<DatePeriod>();
        foreach (var moment in moments)
        {
            // no intersection
            if (!IsWithin(period, moment))
            {
                continue;
            }

            var periodStart = splitPeriods.Any() ?
                // in-between period
                splitPeriods.Last().End.NextTick() :
                // first period
                period.Start;
            splitPeriods.Add(new(periodStart, moment.PreviousTick()));
        }

        // last period
        if (splitPeriods.Any())
        {
            var last = splitPeriods.Last();
            if (last.End != period.End)
            {
                splitPeriods.Add(new(last.End.NextTick(), period.End));
            }
        }
        return splitPeriods;
    }

    /// <summary>Calculate the count of working days</summary>
    /// <param name="period">The period</param>
    /// <param name="workingDays">The working days</param>
    /// <returns>The number of working days</returns>
    public static int GetWorkingDaysCount(this DatePeriod period, IEnumerable<DayOfWeek> workingDays)
    {
        var dayCount = 0;
        var date = period.Start.Date;
        var dayOfWeeks = workingDays as DayOfWeek[] ?? workingDays.ToArray();
        while (date < period.End.Date)
        {
            if (date.IsDayOfWeek(dayOfWeeks))
            {
                dayCount++;
            }
            date = date.AddDays(1);
        }
        return dayCount;
    }
}