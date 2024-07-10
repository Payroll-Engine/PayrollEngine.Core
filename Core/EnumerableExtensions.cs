using System.Collections.Generic;
using System.Linq;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="IEnumerable{T}"/></summary>
public static class EnumerableExtensions
{
    /// <summary>Test if a list is empty</summary>
    /// <param name="source">The list to test</param>
    /// <returns>True if the list is null or empty</returns>
    public static bool IsNullOrEmpty<TValue>(this IEnumerable<TValue> source) =>
        source == null || !source.Any();

    /// <summary>Copy a list, including null check</summary>
    /// <param name="source">The copy source</param>
    /// <returns>A new list with the source items</returns>
    public static List<TValue> Copy<TValue>(this IEnumerable<TValue> source) =>
        source == null ? null : [..source];

    /// <summary>Get the duplicated values.
    /// See https://stackoverflow.com/a/3811482
    /// </summary>
    /// <param name="source">The copy source</param>
    /// <returns>A list with the duplicated values</returns>
    public static IEnumerable<TValue> Duplicates<TValue>(this IEnumerable<TValue> source) =>
        source.GroupBy(value => value).SelectMany(group => group.Skip(1));
}