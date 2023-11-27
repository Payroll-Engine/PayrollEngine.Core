using System;

namespace PayrollEngine;

/// <summary>A payroll mapping exception</summary>
public class PayrollMapException : PayrollException
{
    /// <inheritdoc/>
    public PayrollMapException()
    {
    }

    /// <inheritdoc/>
    public PayrollMapException(string message) :
        base(message)
    {
    }

    /// <inheritdoc/>
    public PayrollMapException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}