using System;

namespace PayrollEngine;

/// <summary>Logger implementation that silently discards all log messages.
/// Used as a safe default when no logger has been configured.</summary>
public sealed class PayrollNullLogger : ILogger
{
    /// <summary>Singleton instance</summary>
    public static readonly PayrollNullLogger Instance = new();

    private PayrollNullLogger() { }

    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel) => false;

    /// <inheritdoc/>
    public void Write(LogLevel level, string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Write(LogLevel level, Exception exception, string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Trace(string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Trace(Exception exception, string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Debug(string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Debug(Exception exception, string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Information(string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Information(Exception exception, string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Warning(string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Warning(Exception exception, string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Error(string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Error(Exception exception, string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Critical(string messageTemplate, params object[] propertyValues) { }

    /// <inheritdoc/>
    public void Critical(Exception exception, string messageTemplate, params object[] propertyValues) { }
}
