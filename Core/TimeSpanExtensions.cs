using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="TimeSpanExtensions"/></summary>
public static class TimeSpanExtensions
{
    /// <summary>Test if time type is scalable</summary>
    /// <param name="timeSpan">The time span</param>
    /// <remarks>see https://stackoverflow.com/questions/842057/how-do-i-convert-a-timespan-to-a-formatted-string</remarks>
    /// <returns>True for time type is scalable</returns>
    public static string ToReadableString(this TimeSpan timeSpan)
    {
        var days = timeSpan.Duration().Days > 0
            ? $"{timeSpan.Days:0} day{(timeSpan.Days == 1 ? string.Empty : "s")}, "
            : string.Empty;
        var hours = timeSpan.Duration().Hours > 0
            ? $"{timeSpan.Hours:0} hour{(timeSpan.Hours == 1 ? string.Empty : "s")}, "
            : string.Empty;
        var minutes = timeSpan.Duration().Minutes > 0
            ? $"{timeSpan.Minutes:0} minute{(timeSpan.Minutes == 1 ? string.Empty : "s")}, "
            : string.Empty;
        var seconds = timeSpan.Duration().Seconds > 0 ?
                $"{timeSpan.Seconds:0} second{(timeSpan.Seconds == 1 ? string.Empty : "s")}" :
                string.Empty;

        var formatted = $"{days}{hours}{minutes}{seconds}";
        if (formatted.EndsWith(", "))
        {
            formatted = formatted.Substring(0, formatted.Length - 2);
        }
        if (string.IsNullOrWhiteSpace(formatted))
        {
            formatted = "0 seconds";
        }
        return formatted;
    }
}