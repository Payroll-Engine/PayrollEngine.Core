using System;
using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="FunctionType"/></summary>
public static class FunctionTypeExtensions
{
    /// <param name="functionType">The function type</param>
    extension(FunctionType functionType)
    {
        /// <summary>
        /// Test for decimal function value type
        /// </summary>
        public bool IsDecimalResult() =>
            functionType is FunctionType.CollectorApply or
                FunctionType.WageTypeValue;

        /// <summary>
        /// Test for boolean function value type
        /// </summary>
        public bool IsBooleanResult() =>
            functionType is FunctionType.CaseAvailable or
                FunctionType.CaseBuild or
                FunctionType.CaseValidate or
                FunctionType.CaseRelationBuild or
                FunctionType.CaseRelationValidate;

        /// <summary>
        /// Get function value type
        /// </summary>
        public Type GetFunctionValueType()
        {
            if (functionType.IsDecimalResult())
            {
                return typeof(decimal);
            }
            if (functionType.IsBooleanResult())
            {
                return typeof(bool);
            }
            return null;
        }
    }

    /// <summary>Convert to bitmask</summary>
    /// <param name="functionTypes">The function types</param>
    /// <returns>The function type bitmask</returns>
    public static long ToBitmask(this List<FunctionType> functionTypes)
    {
        if (functionTypes == null)
        {
            return 0;
        }

        var bitmask = (long)0;
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
        if (bitmask == 0)
        {
            return null;
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