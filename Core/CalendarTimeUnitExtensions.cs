namespace PayrollEngine;

/// <summary>Extensions for <see cref="CalendarTimeUnit"/></summary>
public static class CalendarTimeUnitExtensions
{
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

        // test division
        return periodValue % cycleValue == 0m;
    }
}
