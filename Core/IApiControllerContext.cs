using System;

namespace PayrollEngine;

/// <summary>Api controller context</summary>
public interface IApiControllerContext
{
    /// <summary>Activates the Api controller context</summary>
    /// <param name="targetType">The target type</param>
    /// <returns>The Api controller</returns>
    object Activate(Type targetType);
}