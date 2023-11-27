using System;

namespace PayrollEngine.Data;

/// <summary>Payroll exception</summary>
public class DataException : Exception
{
    /// <inheritdoc/>
    public DataException()
    {
    }

    /// <inheritdoc/>
    public DataException(string message) :
        base(message)
    {
    }

    /// <inheritdoc/>
    public DataException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}