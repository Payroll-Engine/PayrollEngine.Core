using System;
using System.Runtime.Serialization;

namespace PayrollEngine;

/// <summary>Persistence exception</summary>
public class PersistenceException : PayrollException
{
    /// <summary>The error number</summary>
    public PersistenceErrorType ErrorType { get; }

    /// <inheritdoc/>
    public PersistenceException()
    {
    }

    /// <inheritdoc/>
    public PersistenceException(string message, PersistenceErrorType errorType) :
        base(message)
    {
        ErrorType = errorType;
    }

    /// <inheritdoc/>
    public PersistenceException(string message, PersistenceErrorType errorType, Exception innerException) :
        base(message, innerException)
    {
        ErrorType = errorType;
    }

    /// <inheritdoc/>
    protected PersistenceException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
        var errorType = info.GetInt32("ErrorType");
        if (Enum.IsDefined(typeof(PersistenceErrorType), errorType))
        {
            ErrorType = (PersistenceErrorType)errorType;
        }
    }
}