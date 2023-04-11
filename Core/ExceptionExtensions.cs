using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="Exception"/></summary>
public static class ExceptionExtensions
{
    /// <summary>Get the base exception message</summary>
    /// <param name="exception">The top level exception</param>
    /// <returns>The base exception message</returns>
    public static string GetBaseMessage(this Exception exception) =>
        exception.GetBaseException().Message;
}