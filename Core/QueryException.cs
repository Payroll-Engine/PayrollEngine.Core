using System;

namespace PayrollEngine;

/// <summary>Query exception</summary>
public class QueryException : PayrollException
{
    /// <inheritdoc/>
    public QueryException()
    {
    }

    /// <inheritdoc/>
    public QueryException(string message) :
        base(message)
    {
    }

    /// <inheritdoc/>
    public QueryException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}