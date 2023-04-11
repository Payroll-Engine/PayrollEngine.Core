using System;
using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="Enum"/></summary>
public static class EnumExtensions
{
    /// <summary>Get all flags</summary>
    /// <param name="flags">The flags enumeration</param>
    /// <returns>The available flags</returns>
    public static IEnumerable<T> GetFlags<T>(this T flags)
        where T : Enum
    {
        foreach (Enum value in Enum.GetValues(flags.GetType()))
        {
            if (flags.HasFlag(value))
            {
                yield return (T)value;
            }
        }
    }
}