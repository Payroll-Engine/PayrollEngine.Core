using System.Collections.Generic;
using System.Linq;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="IEnumerable{T}"/></summary>
public static class EnumerableExtensions
{
    /// <param name="source">The list to test</param>
    extension<TValue>(IEnumerable<TValue> source)
    {
        /// <summary>Test if a list is empty</summary>
        /// <returns>True if the list is null or empty</returns>
        public bool IsNullOrEmpty() =>
            source == null || !source.Any();

        /// <summary>Copy a list, including null check</summary>
        /// <returns>A new list with the source items</returns>
        public List<TValue> Copy() =>
            source == null ? null : [..source];

        /// <summary>Get the duplicated values.
        /// See https://stackoverflow.com/a/3811482
        /// </summary>
        /// <returns>A list with the duplicated values</returns>
        public IEnumerable<TValue> Duplicates() =>
            source.GroupBy(value => value).SelectMany(group => group.Skip(1));
    }
}