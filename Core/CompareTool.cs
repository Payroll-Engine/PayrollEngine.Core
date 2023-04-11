using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PayrollEngine;

/// <summary>Compare complex objects</summary>
public static class CompareTool
{
    /// <summary>Compare an object containing value, base types are not considered</summary>
    /// <param name="left">The left object to compare</param>
    /// <param name="right">The right object to compare</param>
    /// <returns>True if the objects contains equals values</returns>
    public static bool Equals<T>(T left, T right) where T : IEquatable<T>
    {
        // both undefined
        if (left == null && right == null)
        {
            return true;
        }
        // one side undefined or different count
        if (left == null || right == null)
        {
            return false;
        }

        return left.Equals(right);
    }

    /// <summary>Compare objects by properties, base types are not considered</summary>
    /// <param name="left">The left object to compare</param>
    /// <param name="right">The right object to compare</param>
    /// <returns>True if the objects contains equals values</returns>
    public static bool EqualProperties<T>(T left, T right) where T : IEquatable<T>
    {
        // both undefined
        if (left == null && right == null)
        {
            return true;
        }
        // one side undefined or different count
        if (left == null || right == null)
        {
            return false;
        }

        var properties = TypeTool.GetInstanceProperties(typeof(T));
        foreach (var property in properties)
        {
            if (!EqualValues(property.GetValue(left), property.GetValue(right)))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>Compare a list with object containing value</summary>
    /// <param name="left">The left item to compare</param>
    /// <param name="right">The right item to compare</param>
    /// <returns>True if the lists items are equal</returns>
    public static bool EqualTypeLists<T>(IList<T> left, IList<T> right) where T : IEquatable<T>
    {
        // both undefined
        if (left == null && right == null)
        {
            return true;
        }
        // one side undefined or different count
        if (left == null || right == null || left.Count != right.Count)
        {
            return false;
        }

        for (var index = 0; index < left.Count; index++)
        {
            var leftValue = left[index];
            var rightValue = right[index];
            if (leftValue == null && rightValue == null)
            {
                continue;
            }
            if (leftValue != null && !leftValue.Equals(rightValue))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>Compare values of two lists</summary>
    /// <param name="left">The left item to compare</param>
    /// <param name="right">The right item to compare</param>
    /// <returns>True if the list items are equal</returns>
    public static bool EqualLists(IList left, IList right)
    {
        // both undefined
        if (left == null && right == null)
        {
            return true;
        }
        // one side undefined or different count
        if (left == null || right == null || left.Count != right.Count)
        {
            return false;
        }

        // compare values
        for (var index = 0; index < left.Count; index++)
        {
            var leftValue = left[index];
            var rightValue = right[index];
            if (!EqualValues(leftValue, rightValue))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>Compare distinct items of two lists</summary>
    /// <param name="left">The left item to compare</param>
    /// <param name="right">The right item to compare</param>
    /// <returns>True if the distinct list items are equal</returns>
    public static bool EqualDistinctLists<T>(IList<T> left, IList<T> right)
    {
        // both undefined
        if (left == null && right == null)
        {
            return true;
        }
        // one side undefined
        if (left == null || right == null)
        {
            return false;
        }
        // order distinct items by hash code
        var leftDistinct = left.Distinct().OrderBy(x => x != null ? x.GetHashCode() : 0).ToList();
        var rightDistinct = right.Distinct().OrderBy(x => x != null ? x.GetHashCode() : 0).ToList();
        return EqualLists(leftDistinct, rightDistinct);
    }

    /// <summary>Compare two dictionaries</summary>
    /// <param name="left">The left item to compare</param>
    /// <param name="right">The right item to compare</param>
    /// <returns>True if the dictionary items are equal</returns>
    public static bool EqualDictionaries<TKey, TValue>(IDictionary<TKey, TValue> left, IDictionary<TKey, TValue> right)
    {
        // both undefined
        if (left == null && right == null)
        {
            return true;
        }
        // one side undefined or different count
        if (left == null || right == null)
        {
            return false;
        }
        // compare content: contains key and same value
        // https://stackoverflow.com/questions/3804367/testing-for-equality-between-dictionaries-in-c-sharp
        var equals = left.Count == right.Count && !left.Except(right).Any();
        return equals;
    }

    /// <summary>Compare two dictionaries</summary>
    /// <param name="left">The left item to compare</param>
    /// <param name="right">The right item to compare</param>
    /// <returns>True if the dictionary items are equal</returns>
    public static bool EqualDictionaries(IDictionary left, IDictionary right)
    {
        // both undefined
        if (left == null && right == null)
        {
            return true;
        }
        // one side undefined or different count
        if (left == null || right == null || left.Count != right.Count)
        {
            return false;
        }

        foreach (var key in left.Keys)
        {
            if (!right.Contains(key) || !EqualValues(left[key], right[key]))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>Compare two byte arrays</summary>
    /// <param name="left">The left bytes to compare</param>
    /// <param name="right">The right bytes to compare</param>
    /// <returns>True if the byte arrays are equal</returns>
    public static bool EqualBytes(byte[] left, byte[] right)
    {
        // both undefined
        if (left == null && right == null)
        {
            return true;
        }
        // one side undefined
        if (left == null || right == null)
        {
            return false;
        }
        return left.SequenceEqual(right);
    }

    private static bool EqualValues(object leftValue, object rightValue)
    {
        if (leftValue == null && rightValue == null)
        {
            return true;
        }

        // list
        if (leftValue is IList leftList && rightValue is IList rightList)
        {
            if (!EqualLists(leftList, rightList))
            {
                return false;
            }
            return true;
        }

        // dictionary
        if (leftValue is IDictionary leftDictionary && rightValue is IDictionary rightDictionary)
        {
            if (!EqualDictionaries(leftDictionary, rightDictionary))
            {
                return false;
            }
            return true;
        }

        // equatable
        var equalEquatable = EqualEquatable(leftValue, rightValue);
        if (equalEquatable.HasValue && equalEquatable.Value)
        {
            return true;
        }

        // object
        var equalObject = leftValue != null && leftValue.Equals(rightValue);
        return equalObject;
    }

    private static bool? EqualEquatable(object leftValue, object rightValue)
    {
        if (leftValue == null || rightValue == null)
        {
            return null;
        }

        var leftType = leftValue.GetType();
        var rightType = rightValue.GetType();
        if (leftType == rightType)
        {
            var leftEquatable = leftType.GetInterfaces()
                .Where(t => t.IsGenericType)
                .Select(t => t.GetGenericTypeDefinition())
                .Any(t => t == typeof(IEquatable<>));
            var rightEquatable = rightType.GetInterfaces()
                .Where(t => t.IsGenericType)
                .Select(t => t.GetGenericTypeDefinition())
                .Any(t => t == typeof(IEquatable<>));
            if (leftEquatable.Equals(rightEquatable))
            {
                MethodInfo method = leftType.GetMethod(nameof(IEquatable<object>.Equals), new[] { rightType });
                if (method != null)
                {
                    var equals = method.Invoke(leftValue, new[] { rightValue });
                    {
                        return equals != null && (bool)equals;
                    }
                }
            }
        }
        return null;
    }
}