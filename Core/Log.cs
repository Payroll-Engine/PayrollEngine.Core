using System;
using System.Diagnostics.CodeAnalysis;

namespace PayrollEngine;

/// <summary>Static logger</summary>
public static class Log
{
    #region Logger

    [SuppressMessage("NDepend", "ND1901:AvoidNonReadOnlyStaticFields", 
        Justification = "Logger is initialized at runtime and must remain mutable.")]
    private static volatile ILogger logger = PayrollNullLogger.Instance;

    /// <summary>The static logger. Returns <see cref="PayrollNullLogger.Instance"/> if no logger has been configured.</summary>
    public static ILogger Logger => logger;

    /// <summary>Test if a logger has been configured</summary>
    /// <returns>True if a custom logger has been set</returns>
    public static bool HasLogger => logger is not PayrollNullLogger;

    /// <summary>Set logger based on the configuration</summary>
    /// <param name="value">The static logger</param>
    public static void SetLogger(ILogger value)
    {
        ArgumentNullException.ThrowIfNull(value);
        logger = value;
    }

    /// <summary>Determine if events at the specified level will be passed through to the log sinks</summary>
    /// <param name="level">Level to check</param>
    /// <returns>True if the level is enabled; otherwise, false</returns>
    public static bool IsEnabled(LogLevel level) =>
        logger.IsEnabled(level);

    #endregion

    #region Write

    /// <summary>Write a log event with the specified level</summary>
    /// <param name="level">The level of the event</param>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    public static void Write(LogLevel level, string messageTemplate, params object[] propertyValues) =>
        logger.Write(level, messageTemplate, propertyValues);

    /// <summary>Write a log event with the specified level and associated exception</summary>
    /// <param name="level">The level of the event</param>
    /// <param name="exception">Exception related to the event</param>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    public static void Write(LogLevel level, Exception exception, string messageTemplate, params object[] propertyValues) =>
        logger.Write(level, exception, messageTemplate, propertyValues);

    #endregion

    #region Trace

    /// <summary>Write a log event with the <see cref="LogLevel.Verbose"/> level</summary>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Verbose("Staring into space, wondering if we're alone.");
    /// </example>
    public static void Trace(string messageTemplate, params object[] propertyValues) =>
        logger.Trace(messageTemplate, propertyValues);

    /// <summary>Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception</summary>
    /// <param name="exception">Exception related to the event</param>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
    /// </example>
    public static void Trace(Exception exception, string messageTemplate, params object[] propertyValues) =>
        logger.Trace(exception, messageTemplate, propertyValues);

    #endregion

    #region Debug

    /// <summary>Write a log event with the <see cref="LogLevel.Debug"/> level</summary>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
    /// </example>
    public static void Debug(string messageTemplate, params object[] propertyValues) =>
        logger.Debug(messageTemplate, propertyValues);

    /// <summary>Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception</summary>
    /// <param name="exception">Exception related to the event</param>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Debug(ex, "Swallowing a mundane exception");
    /// </example>
    public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues) =>
        logger.Debug(exception, messageTemplate, propertyValues);

    #endregion

    #region Information

    /// <summary>Write a log event with the <see cref="LogLevel.Information"/> level</summary>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
    /// </example>
    public static void Information(string messageTemplate, params object[] propertyValues) =>
        logger.Information(messageTemplate, propertyValues);

    /// <summary>Write a log event with the <see cref="LogLevel.Information"/> level and associated exception</summary>
    /// <param name="exception">Exception related to the event</param>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
    /// </example>
    public static void Information(Exception exception, string messageTemplate, params object[] propertyValues) =>
        logger.Information(exception, messageTemplate, propertyValues);

    #endregion

    #region Warning

    /// <summary>Write a log event with the <see cref="LogLevel.Warning"/> level</summary>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
    /// </example>
    public static void Warning(string messageTemplate, params object[] propertyValues) =>
        logger.Warning(messageTemplate, propertyValues);

    /// <summary>Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception</summary>
    /// <param name="exception">Exception related to the event</param>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
    /// </example>
    public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues) =>
        logger.Warning(exception, messageTemplate, propertyValues);

    #endregion

    #region Error

    /// <summary>Write a log event with the <see cref="LogLevel.Error"/> level</summary>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
    /// </example>
    public static void Error(string messageTemplate, params object[] propertyValues) =>
        logger.Error(messageTemplate, propertyValues);

    /// <summary>Write a log event with the <see cref="LogLevel.Error"/> level and associated exception</summary>
    /// <param name="exception">Exception related to the event</param>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
    /// </example>
    public static void Error(Exception exception, string messageTemplate, params object[] propertyValues) =>
        logger.Error(exception, messageTemplate, propertyValues);

    #endregion

    #region Critical

    /// <summary>Write a log event with the <see cref="LogLevel.Fatal"/> level</summary>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Fatal("Process terminating.");
    /// </example>
    public static void Critical(string messageTemplate, params object[] propertyValues) =>
        logger.Critical(messageTemplate, propertyValues);

    /// <summary>Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception</summary>
    /// <param name="exception">Exception related to the event</param>
    /// <param name="messageTemplate">Message template describing the event</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template</param>
    /// <example>
    /// Log.Fatal(ex, "Process terminating.");
    /// </example>
    public static void Critical(Exception exception, string messageTemplate, params object[] propertyValues) =>
        logger.Critical(exception, messageTemplate, propertyValues);

    #endregion

}