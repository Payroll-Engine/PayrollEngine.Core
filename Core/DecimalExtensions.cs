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

    /// <summary>Returns the integral digits of the specified decimal, using a step size</summary>
    /// <param name="value">The decimal number to truncate</param>
    /// <param name="stepSize">The step size used to truncate</param>
    /// <returns>The result of d rounded toward zero, to the nearest whole number within the step size</returns>
    public static decimal Truncate(this decimal value, int stepSize) =>
        value == default ? default : value - (value % stepSize);

    /// <summary>Rounds a decimal value up</summary>
    /// <param name="value">The decimal value to round</param>
    /// <param name="stepSize">The round step size</param>
    /// <returns>The up-rounded value</returns>
    public static decimal RoundUp(this decimal value, decimal stepSize) =>
        value == default || stepSize == 0 ? value : Math.Ceiling(value / stepSize) * stepSize;

    /// <summary>Rounds a decimal value down</summary>
    /// <param name="value">The decimal value to round</param>
    /// <param name="stepSize">The round step size</param>
    /// <returns>The rounded value</returns>
    public static decimal RoundDown(this decimal value, decimal stepSize) =>
        value == default || stepSize == 0 ? value : Math.Floor(value / stepSize) * stepSize;

    /// <summary>Rounds a decimal value wit predefined rounding type</summary>
    /// <param name="value">The decimal value to round</param>
    /// <param name="rounding">The rounding type</param>
    /// <returns>The rounded value</returns>
    public static decimal Round(this decimal value, DecimalRounding rounding) =>
        rounding switch
        {
            DecimalRounding.Integer => decimal.Round(value),
            DecimalRounding.Half => RoundHalf(value),
            DecimalRounding.Fifth => RoundFifth(value),
            DecimalRounding.Tenth => RoundTenth(value),
            DecimalRounding.Twentieth => RoundTwentieth(value),
            DecimalRounding.Fiftieth => RoundFiftieth(value),
            DecimalRounding.Hundredth => RoundHundredth(value),
            DecimalRounding.None => value,
            _ => value
        };

    /// <summary>Rounds a decimal value to a one-half (e.g. 50 cents)</summary>
    /// <param name="value">The decimal value to round</param>
    /// <returns>The rounded value to one-half</returns>
    public static decimal RoundHalf(this decimal value) =>
        RoundPartOfOne(value, 2);

    /// <summary>Rounds a decimal value to a one-fifth (e.g. 20 cents)</summary>
    /// <param name="value">The decimal value to round</param>
    /// <returns>The rounded value to one-fifth</returns>
    public static decimal RoundFifth(this decimal value) =>
        RoundPartOfOne(value, 5);

    /// <summary>Rounds a decimal value to a one-tenth (e.g. 10 cents)</summary>
    /// <param name="value">The decimal value to round</param>
    /// <returns>The rounded value to one-tenth</returns>
    public static decimal RoundTenth(this decimal value) =>
        RoundPartOfOne(value, 10);

    /// <summary>Rounds a decimal value to a one-twentieth (e.g. 5 cents)</summary>
    /// <param name="value">The decimal value to round</param>
    /// <returns>The rounded value to one-twentieth</returns>
    public static decimal RoundTwentieth(this decimal value) =>
        RoundPartOfOne(value, 20);

    /// <summary>Rounds a decimal value to a one-fiftieth (e.g. 2 cents)</summary>
    /// <param name="value">The decimal value to round</param>
    /// <returns>The rounded value to one-fiftieth</returns>
    public static decimal RoundFiftieth(this decimal value) =>
        RoundPartOfOne(value, 50);

    /// <summary>Rounds a decimal value to a one-hundredth (e.g. 1 cents)</summary>
    /// <param name="value">The decimal value to round</param>
    /// <returns>The rounded value to one-hundredth</returns>
    public static decimal RoundHundredth(this decimal value) =>
        RoundPartOfOne(value, 100);

    /// <summary>Rounds a decimal value to a one-tenth</summary>
    /// <param name="value">The decimal value to round</param>
    /// <param name="divisor">The divisor factor</param>
    /// <returns>The rounded value to one-tenth</returns>
    public static decimal RoundPartOfOne(this decimal value, int divisor) =>
        value == default ? default : Math.Round(value * divisor, MidpointRounding.AwayFromZero) / divisor;

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