
namespace PayrollEngine;

/// <summary>Extensions for <see cref="CalendarTimeUnit"/></summary>
public static class CalendarTimeUnitExtensions
{
    /// <param name="cycleUnit">The payroll cycle unit</param>
    extension(CalendarTimeUnit cycleUnit)
    {
        /// <summary>Get the count of periods within the cycle</summary>
        /// <param name="periodUnit">The payroll period unit</param>
        /// <returns>True for a valid cycle/period combination</returns>
        public int PeriodCount(CalendarTimeUnit periodUnit)
        {
            // period unit must be larger as the cycle unit
            if (periodUnit < cycleUnit)
            {
                return 0;
            }

            var periodValue = (decimal)periodUnit;
            var cycleValue = (decimal)cycleUnit;
            if (periodValue == 0 || cycleValue == 0)
            {
                return 0;
            }

            // test division
            return decimal.ToInt32(periodValue / cycleValue);
        }

        /// <summary>Test for valid time unit combination</summary>
        /// <param name="periodUnit">The payroll period unit</param>
        /// <returns>True for a valid cycle/period combination</returns>
        public bool IsValidTimeUnit(CalendarTimeUnit periodUnit)
        {
            // period unit must be larger as the cycle unit
            if (periodUnit < cycleUnit)
            {
                return false;
            }

            var periodValue = (decimal)periodUnit;
            var cycleValue = (decimal)cycleUnit;
            if (periodValue == 0 || cycleValue == 0)
            {
                return false;
            }

            // remainder should be zero
            return periodValue % cycleValue == 0m;
        }
    }
}
