
namespace PayrollEngine;

/// <summary>Extensions for <see cref="CalendarTimeUnit"/></summary>
public static class CalendarTimeUnitExtensions
{
    /// <summary>Get the count of periods within the cycle</summary>
    /// <param name="cycleUnit">The payroll cycle unit</param>
    /// <param name="periodUnit">The payroll period unit</param>
    /// <returns>True for a valid cycle/period combination</returns>
    public static int PeriodCount(this CalendarTimeUnit cycleUnit, CalendarTimeUnit periodUnit)
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
    /// <param name="cycleUnit">The payroll cycle unit</param>
    /// <param name="periodUnit">The payroll period unit</param>
    /// <returns>True for a valid cycle/period combination</returns>
    public static bool IsValidTimeUnit(this CalendarTimeUnit cycleUnit, CalendarTimeUnit periodUnit)
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
