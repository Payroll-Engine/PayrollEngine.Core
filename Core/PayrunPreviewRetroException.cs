using System;

namespace PayrollEngine;

/// <summary>
/// Thrown when a payrun preview encounters a retroactive calculation requirement.
/// Preview mode cannot perform retro calculations because results are not persisted.
/// </summary>
public class PayrunPreviewRetroException : PayrunException
{
    /// <summary>The employee identifier that triggered the retro calculation</summary>
    public string EmployeeIdentifier { get; }

    /// <summary>The retro date that would be required</summary>
    public DateTime? RetroDate { get; }

    /// <inheritdoc/>
    public PayrunPreviewRetroException()
    {
    }

    /// <inheritdoc/>
    public PayrunPreviewRetroException(string message) :
        base(message)
    {
    }

    /// <summary>
    /// Creates a new instance with employee and retro date context.
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="employeeIdentifier">The employee identifier that triggered the retro calculation</param>
    /// <param name="retroDate">The retro date that would be required</param>
    public PayrunPreviewRetroException(string message, string employeeIdentifier, DateTime? retroDate) :
        base(message)
    {
        EmployeeIdentifier = employeeIdentifier;
        RetroDate = retroDate;
    }

    /// <inheritdoc/>
    public PayrunPreviewRetroException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}
