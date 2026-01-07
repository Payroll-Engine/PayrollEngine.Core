using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="DatePeriod"/></summary>
public static class DatePeriodExtensions
{
    /// <param name="period">The period</param>
    extension(DatePeriod period)
    {
        /// <summary>Test if a specific time moment is before this period</summary>
        /// <param name="testMoment">The moment to test</param>
        /// <returns>True, if the moment is before this period</returns>
        public bool IsBefore(DateTime testMoment) =>
            testMoment < period.Start;

        /// <summary>Test if a specific time period is before this period</summary>
        /// <param name="testPeriod">The period to test</param>
        /// <returns>True, if the period is before this period</returns>
        public bool IsBefore(DatePeriod testPeriod) =>
            testPeriod.End < period.Start;

        /// <summary>Test if a specific time moment is after this period</summary>
        /// <param name="testMoment">The moment to test</param>
        /// <returns>True, if the moment is after this period</returns>
        public bool IsAfter(DateTime testMoment) =>
            testMoment > period.End;

        /// <summary>Test if a specific time period is after this period</summary>
        /// <param name="testPeriod">The period to test</param>
        /// <returns>True, if the period is after this period</returns>
        public bool IsAfter(DatePeriod testPeriod) =>
            testPeriod.Start > period.End;

        /// <summary>Test if a specific time moment is within the period, including open periods</summary>
        /// <param name="testMoment">The moment to test</param>
        /// <returns>True, if the moment is within this period</returns>
        public bool IsWithin(DateTime testMoment) =>
            testMoment.IsWithin(period.Start, period.End);

        /// <summary>Test if a specific time period is within the period, including open periods</summary>
        /// <param name="testPeriod">The period to test</param>
        /// <returns>True, if the test period is within this period</returns>
        public bool IsWithin(DatePeriod testPeriod) => 
            testPeriod.IsWithin(period.Start) && testPeriod.IsWithin(period.End);

        /// <summary>Test if a specific time moment is within or before the period, including open periods</summary>
        /// <param name="testMoment">The moment to test</param>
        /// <returns>True, if the moment is within or before this period</returns>
        public bool IsWithinOrBefore(DateTime testMoment) =>
            testMoment <= period.End;

        /// <summary>Test if a specific time moment is within or after the period, including open periods</summary>
        /// <param name="testMoment">The moment to test</param>
        /// <returns>True, if the moment is within or after this period</returns>
        public bool IsWithinOrAfter(DateTime testMoment) =>
            testMoment >= period.Start;

        /// <summary>Test if period is overlapping this period</summary>
        /// <param name="testPeriod">The period to test</param>
        /// <returns>True, if the period is overlapping this period</returns>
        public bool IsOverlapping(DatePeriod testPeriod) =>
            period.Start < testPeriod.End && testPeriod.Start < period.End;

        /// <summary>Build the intersection of two date periods</summary>
        /// <param name="intersectPeriod">The period to intersect</param>
        /// <returns>The intersection period, null if no intersection is present</returns>
        public DatePeriod Intersect(DatePeriod intersectPeriod)
        {
            if (!period.IsOverlapping(intersectPeriod))
            {
                return null;
            }
            return new(
                new(Math.Max(period.Start.Ticks, intersectPeriod.Start.Ticks)),
                new(Math.Min(period.End.Ticks, intersectPeriod.End.Ticks)));
        }

        /// <summary>Split date period by splitting dates</summary>
        /// <param name="splitMoments">The moments used to split</param>
        /// <returns>The splitting periods</returns>
        public List<DatePeriod> Split(List<DateTime> splitMoments)
        {
            // ensure unique moments, ordered from oldest to newest
            var moments = new HashSet<DateTime>(splitMoments).OrderBy(x => x);

            var splitPeriods = new List<DatePeriod>();
            foreach (var moment in moments)
            {
                // no intersection
                if (!period.IsWithin(moment))
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
        /// <param name="workingDays">The working days</param>
        /// <returns>The number of working days</returns>
        public int GetWorkingDaysCount(IEnumerable<DayOfWeek> workingDays)
        {
            var dayCount = 0;
            var date = period.Start.Date;
            var dayOfWeeks = workingDays as DayOfWeek[] ?? workingDays.ToArray();
            while (date <= period.End.Date)
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
}