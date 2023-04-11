using System;
using System.Runtime.Serialization;

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

    /// <inheritdoc/>
    protected PayrollException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }
}