using System;

namespace PayrollEngine;

/// <summary>
/// Extensions for <see cref="decimal"/>
/// </summary>
public static class DecimalExtensions
{
    /// <summary> Determines if the decimal value is equal to (==) the float parameter according to the system specification of number scale</summary>
    /// <param name="left">The left value to test</param>
    /// <param name="right">The right value to test</param>
    /// <returns>True, if both values are withing the system precision scale</returns>
    public static bool AlmostEquals(this decimal left, decimal right) =>
        AlmostEquals(left, right, SystemSpecification.DecimalScale);

    /// <summary>Determines if the decimal value is equal to (==) the float parameter according to the defined precision.
    /// See https://stackoverflow.com/a/8608505
    /// </summary>
    /// <param name="left">The left value to test</param>
    /// <param name="right">The right value to test</param>
    /// <param name="precision">The precision, number of digits after the decimal that will be considered when comparing</param>
    /// <returns>True, if both values are withing the precision scale</returns>
    public static bool AlmostEquals(this decimal left, decimal right, int precision)
    {
        return Math.Round(left - right, precision) == 0;
    }

    /// <summary>Round a decimal value using the payroll system precision</summary>
    /// <param name="value">The decimal value to round</param>
    /// <returns>The rounded payroll value</returns>
    public static decimal RoundPayroll(this decimal value)
    {
        // ignore minimal rounding values
        // https://stackoverflow.com/questions/48212254/set-the-precision-for-decimal-numbers-in-c-sharp
        return decimal.Round(value, SystemSpecification.DecimalScale, MidpointRounding.AwayFromZero);
    }
}