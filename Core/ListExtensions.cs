using System;
using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="IList{T}"/></summary>
public static class ListExtensions
{
    /// <param name="source">The list</param>
    extension<TValue>(IList<TValue> source)
    {
        /// <summary>Try to add a new value to a list</summary>
        /// <param name="value">The value to add</param>
        /// <returns>True if the value was added</returns>
        public bool TryAddNew(TValue value)
        {
            if (source.Contains(value))
            {
                return false;
            }
            source.Add(value);
            return true;
        }

        /// <summary>Try to add new values to a list</summary>
        /// <param name="values">The values to add</param>
        /// <returns>The added values</returns>
        public List<TValue> AddNew(IEnumerable<TValue> values)
        {
            ArgumentNullException.ThrowIfNull(values);

            var addedValues = new List<TValue>();
            foreach (var value in values)
            {
                if (source.TryAddNew(value))
                {
                    addedValues.Add(value);
                }
            }
            return addedValues;
        }
    }
}