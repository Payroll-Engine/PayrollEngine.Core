using System;
using System.Runtime.Serialization;

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

    /// <inheritdoc/>
    protected PayrunException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }
}