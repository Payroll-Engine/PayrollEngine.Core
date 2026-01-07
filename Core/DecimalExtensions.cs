using System;

namespace PayrollEngine;

/// <summary>
/// Extensions for <see cref="decimal"/>
/// </summary>
public static class DecimalExtensions
{
    /// <param name="left">The left value to test</param>
    extension(decimal left)
    {
        /// <summary> Determines if the decimal value is equal to (==) the float parameter according to the system specification of number scale</summary>
        /// <param name="right">The right value to test</param>
        /// <returns>True, if both values are withing the system precision scale</returns>
        public bool AlmostEquals(decimal right) => left.AlmostEquals(right, SystemSpecification.DecimalScale);

        /// <summary>Determines if the decimal value is equal to (==) the float parameter according to the defined precision.
        /// See https://stackoverflow.com/a/8608505
        /// </summary>
        /// <param name="right">The right value to test</param>
        /// <param name="precision">The precision, number of digits after the decimal that will be considered when comparing</param>
        /// <returns>True, if both values are withing the precision scale</returns>
        public bool AlmostEquals(decimal right, int precision) =>
            Math.Round(left - right, precision) == 0;

        /// <summary>Returns the integral digits of the specified decimal, using a step size</summary>
        /// <param name="stepSize">The step size used to truncate</param>
        /// <returns>The result of d rounded toward zero, to the nearest whole number within the step size</returns>
        public decimal Truncate(int stepSize) =>
            left == 0 ? 0 : left - (left % stepSize);

        /// <summary>Rounds a decimal value up</summary>
        /// <param name="stepSize">The round step size</param>
        /// <returns>The up-rounded value</returns>
        public decimal RoundUp(decimal stepSize) =>
            left == 0 || stepSize == 0 ? left : Math.Ceiling(left / stepSize) * stepSize;

        /// <summary>Rounds a decimal value down</summary>
        /// <param name="stepSize">The round step size</param>
        /// <returns>The rounded value</returns>
        public decimal RoundDown(decimal stepSize) =>
            left == 0 || stepSize == 0 ? left : Math.Floor(left / stepSize) * stepSize;

        /// <summary>Rounds a decimal value wit predefined rounding type</summary>
        /// <param name="rounding">The rounding type</param>
        /// <returns>The rounded value</returns>
        public decimal Round(DecimalRounding rounding) =>
            rounding switch
            {
                DecimalRounding.Integer => decimal.Round(left),
                DecimalRounding.Half => left.RoundHalf(),
                DecimalRounding.Fifth => left.RoundFifth(),
                DecimalRounding.Tenth => left.RoundTenth(),
                DecimalRounding.Twentieth => left.RoundTwentieth(),
                DecimalRounding.Fiftieth => left.RoundFiftieth(),
                DecimalRounding.Hundredth => left.RoundHundredth(),
                _ => left
            };

        /// <summary>Rounds a decimal value to a one-half (e.g. 50 cents)</summary>
        /// <returns>The rounded value to one-half</returns>
        public decimal RoundHalf() => left.RoundPartOfOne(2);

        /// <summary>Rounds a decimal value to a one-fifth (e.g. 20 cents)</summary>
        /// <returns>The rounded value to one-fifth</returns>
        public decimal RoundFifth() => left.RoundPartOfOne(5);

        /// <summary>Rounds a decimal value to a one-tenth (e.g. 10 cents)</summary>
        /// <returns>The rounded value to one-tenth</returns>
        public decimal RoundTenth() => left.RoundPartOfOne(10);

        /// <summary>Rounds a decimal value to a one-twentieth (e.g. 5 cents)</summary>
        /// <returns>The rounded value to one-twentieth</returns>
        public decimal RoundTwentieth() => left.RoundPartOfOne(20);

        /// <summary>Rounds a decimal value to a one-fiftieth (e.g. 2 cents)</summary>
        /// <returns>The rounded value to one-fiftieth</returns>
        public decimal RoundFiftieth() => left.RoundPartOfOne(50);

        /// <summary>Rounds a decimal value to a one-hundredth (e.g. 1 cents)</summary>
        /// <returns>The rounded value to one-hundredth</returns>
        public decimal RoundHundredth() => left.RoundPartOfOne(100);

        /// <summary>Rounds a decimal value to a one-tenth</summary>
        /// <param name="divisor">The divisor factor</param>
        /// <returns>The rounded value to one-tenth</returns>
        public decimal RoundPartOfOne(int divisor) =>
            left == 0 ? 0 : Math.Round(left * divisor, MidpointRounding.AwayFromZero) / divisor;

        /// <summary>Round a decimal value using the payroll system precision</summary>
        /// <returns>The rounded payroll value</returns>
        public decimal RoundPayroll()
        {
            // ignore minimal rounding values
            // https://stackoverflow.com/questions/48212254/set-the-precision-for-decimal-numbers-in-c-sharp
            return decimal.Round(left, SystemSpecification.DecimalScale, MidpointRounding.AwayFromZero);
        }
    }
}