using System.Globalization;

namespace PayrollEngine;

/// <summary>Hash extensions for <see cref="string"/></summary>
public static class StringHashExtensions
{
    /// <param name="source">The source value</param>
    extension(string source)
    {
        /// <summary>Get the payroll lookup hash code, combined by key and range value
        /// See https://andrewlock.net/why-is-string-gethashcode-different-each-time-i-run-my-program-in-net-core/
        /// </summary>
        /// <param name="rangeValue">The range value(lookup)</param>
        /// <returns>The value key hash code, zero on value or range value</returns>
        public int ToPayrollHash(decimal? rangeValue)
        {
            if (string.IsNullOrWhiteSpace(source) && !rangeValue.HasValue)
            {
                return 0;
            }

            // single hash key
            if (!rangeValue.HasValue)
            {
                return StringHashExtensions.GetHashCode(source);
            }
            if (string.IsNullOrWhiteSpace(source))
            {
                return StringHashExtensions.GetHashCode(rangeValue);
            }

            // combined hash key
            return StringHashExtensions.GetHashCode(source, rangeValue.Value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>Get the lookup key hash code</summary>
        /// <returns>The value key hash code, zero on empty value</returns>
        public int ToPayrollHash() =>
            string.IsNullOrWhiteSpace(source) ? 0 : StringHashExtensions.GetHashCode(source);
    }

    private static int GetHashCode(params object[] values)
    {
        // create hash code to store in database
        // the CLR GetHashCode() depends on the framework version
        // see https://stackoverflow.com/a/5155015/15659039
        unchecked // Overflow is fine, just wrap
        {
            var hash = 23;
            foreach (var value in values)
            {
                var stringValue = value.ToString();
                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    continue;
                }
                foreach (var c in stringValue)
                {
                    hash = hash * 31 + c;
                }
            }
            return hash;
        }
    }
}