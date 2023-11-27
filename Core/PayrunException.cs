using System;

namespace PayrollEngine;

/// <summary>Payrun exception</summary>
public class PayrunException : PayrollException
{
    /// <inheritdoc/>
    public PayrunException()
    {
    }

    /// <inheritdoc/>
    public PayrunException(string message) :
        base(message)
    {
    }

    /// <inheritdoc/>
    public PayrunException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}