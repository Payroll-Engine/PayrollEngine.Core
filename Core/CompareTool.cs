using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

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

    /// <summary>Compare objects properties, ignoring base types</summary>
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

        // property values
        var properties = TypeTool.GetTypeProperties(typeof(T));
        foreach (var property in properties)
        {
            if (!EqualValues(property.GetValue(left), property.GetValue(right)))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>Compare typed lists</summary>
    /// <param name="left">The left item to compare</param>
    /// <param name="right">The right item to compare</param>
    /// <returns>True if the lists items are equal</returns>
    public static bool EqualTypeLists<T>(IList<T> left, IList<T> right) where T : IEquatable<T>
    {
        // undefined or empty
        if ((left == null && right == null) ||
            (left == null && right.Count == 0) ||
            (right == null && left.Count == 0))
        {
            return true;
        }

        // one side undefined or different count
        if (left == null || right == null || left.Count != right.Count)
        {
            return false;
        }

        // list values
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

    /// <summary>Compare values of lists</summary>
    /// <param name="left">The left item to compare</param>
    /// <param name="right">The right item to compare</param>
    /// <returns>True if the list items are equal</returns>
    public static bool EqualLists(IList left, IList right)
    {
        // undefined or empty
        if ((left == null && right == null) ||
            (left == null && right.Count == 0) ||
            (right == null && left.Count == 0))
        {
            return true;
        }

        // one side undefined or different count
        if (left == null || right == null || left.Count != right.Count)
        {
            return false;
        }

        // list values
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

    /// <summary>Compare distinct items of lists</summary>
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

    /// <summary>Compare  dictionaries</summary>
    /// <param name="left">The left item to compare</param>
    /// <param name="right">The right item to compare</param>
    /// <returns>True if the dictionary items are equal</returns>
    public static bool EqualDictionaries<TKey, TValue>(IDictionary<TKey, TValue> left, IDictionary<TKey, TValue> right)
    {
        // undefined or empty
        if ((left == null && right == null) ||
            (left == null && right.Count == 0) ||
            (right == null && left.Count == 0))
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
        // ReSharper disable once UsageOfDefaultStructEquality
        var equals = left.Count == right.Count && !left.Except(right).Any();
        return equals;
    }

    /// <summary>Compare dictionaries</summary>
    /// <param name="left">The left item to compare</param>
    /// <param name="right">The right item to compare</param>
    /// <returns>True if the dictionary items are equal</returns>
    public static bool EqualDictionaries(IDictionary left, IDictionary right)
    {
        // undefined or empty
        if ((left == null && right == null) ||
            (left == null && right.Count == 0) ||
            (right == null && left.Count == 0))
        {
            return true;
        }

        // one side undefined or different count
        if (left == null || right == null || left.Count != right.Count)
        {
            return false;
        }

        // compare item
        foreach (var key in left.Keys)
        {
            if (!right.Contains(key) || !EqualValues(left[key], right[key]))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>Compare byte arrays</summary>
    /// <param name="left">The left bytes to compare</param>
    /// <param name="right">The right bytes to compare</param>
    /// <returns>True if the byte arrays are equal</returns>
    public static bool EqualBytes(byte[] left, byte[] right)
    {
        // undefined or empty
        if ((left == null && right == null) ||
            (left == null && right.Length == 0) ||
            (right == null && left.Length == 0))
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

    /// <summary>
    /// Compare values
    /// </summary>
    /// <param name="leftValue">Left value to compare</param>
    /// <param name="rightValue">True value to compare</param>
    /// <returns>True on equal object</returns>
    private static bool EqualValues(object leftValue, object rightValue)
    {
        if (leftValue == null && rightValue == null)
        {
            return true;
        }

        // list
        if (leftValue is IList || rightValue is IList)
        {
            return EqualLists(leftValue as IList, rightValue as IList);
        }

        // dictionary
        if (leftValue is IDictionary || rightValue is IDictionary)
        {
            return EqualDictionaries(leftValue as IDictionary, rightValue as IDictionary);
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

    /// <summary>
    /// Compare equatable values
    /// </summary>
    /// <param name="leftValue">Left value to compare</param>
    /// <param name="rightValue">True value to compare</param>
    /// <returns>True on equal object</returns>
    private static bool? EqualEquatable(object leftValue, object rightValue)
    {
        if (leftValue == null || rightValue == null)
        {
            return null;
        }

        // object type
        var leftType = leftValue.GetType();
        var rightType = rightValue.GetType();
        if (leftType != rightType)
        {
            return null;
        }

        var leftEquatable = leftType.GetInterfaces()
            .Where(t => t.IsGenericType)
            .Select(t => t.GetGenericTypeDefinition())
            .Any(t => t == typeof(IEquatable<>));
        var rightEquatable = rightType.GetInterfaces()
            .Where(t => t.IsGenericType)
            .Select(t => t.GetGenericTypeDefinition())
            .Any(t => t == typeof(IEquatable<>));
        if (!leftEquatable.Equals(rightEquatable))
        {
            return null;
        }

        // object values
        MethodInfo method = leftType.GetMethod(nameof(IEquatable<>.Equals), [rightType]);
        if (method == null)
        {
            return null;
        }
        var equals = method.Invoke(leftValue, [rightValue]);
        return equals != null && (bool)equals;
    }
}