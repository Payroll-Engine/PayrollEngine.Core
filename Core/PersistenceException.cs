using System;

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
}