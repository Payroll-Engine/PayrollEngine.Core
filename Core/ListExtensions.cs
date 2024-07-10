using System;
using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="IList{T}"/></summary>
public static class ListExtensions
{
    /// <summary>Try to add a new value to a list</summary>
    /// <param name="source">The list</param>
    /// <param name="value">The value to add</param>
    /// <returns>True if the value was added</returns>
    public static bool TryAddNew<TValue>(this IList<TValue> source, TValue value)
    {
        if (source.Contains(value))
        {
            return false;
        }
        source.Add(value);
        return true;
    }

    /// <summary>Try to add new values to a list</summary>
    /// <param name="source">The list</param>
    /// <param name="values">The values to add</param>
    /// <returns>The added values</returns>
    public static List<TValue> AddNew<TValue>(this IList<TValue> source, IEnumerable<TValue> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        var addedValues = new List<TValue>();
        foreach (var value in values)
        {
            if (TryAddNew(source, value))
            {
                addedValues.Add(value);
            }
        }
        return addedValues;
    }
}