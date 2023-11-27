using System;

namespace PayrollEngine;

/// <summary>Payroll exception</summary>
public class PayrollException : Exception
{
    /// <inheritdoc/>
    public PayrollException()
    {
    }

    /// <inheritdoc/>
    public PayrollException(string message) :
        base(message)
    {
    }

    /// <inheritdoc/>
    public PayrollException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}