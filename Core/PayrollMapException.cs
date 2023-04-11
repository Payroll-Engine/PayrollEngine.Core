using System;
using System.Runtime.Serialization;

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

    /// <inheritdoc/>
    protected PayrollMapException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }
}