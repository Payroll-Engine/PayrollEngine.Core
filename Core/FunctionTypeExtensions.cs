using System;
using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="FunctionType"/></summary>
public static class FunctionTypeExtensions
{
    /// <summary>Convert to bitmask</summary>
    /// <param name="functionTypes">The function types</param>
    /// <returns>The function type bitmask</returns>
    public static long ToBitmask(this List<FunctionType> functionTypes)
    {
        if (functionTypes == null)
        {
            return default;
        }

        var bitmask = (long)default;
        foreach (var functionType in functionTypes)
        {
            bitmask |= (long)functionType;
        }
        return bitmask;
    }

    /// <summary>Convert from bitmask</summary>
    /// <param name="bitmask">The function types bitmask</param>
    /// <returns>The function types</returns>
    public static List<FunctionType> ToFunctionTypes(this long bitmask)
    {
        if (bitmask == default)
        {
            return default;
        }

        var functionTypes = new List<FunctionType>();
        foreach (var functionType in Enum.GetValues<FunctionType>())
        {
            if ((bitmask & (long)functionType) == (long)functionType)
            {
                functionTypes.Add(functionType);
            }
        }
        return functionTypes;
    }

    /// <summary>Test if function is available</summary>
    /// <param name="functionTypes">The function types</param>
    /// <param name="test">The test function</param>
    /// <returns>True if the function is available</returns>
    public static bool ContainsFunction(this List<FunctionType> functionTypes, FunctionType test)
    {
        if (functionTypes != null)
        {
            foreach (var functionType in functionTypes)
            {
                if (functionType == test)
                {
                    return true;
                }
            }
        }
        return false;
    }
}