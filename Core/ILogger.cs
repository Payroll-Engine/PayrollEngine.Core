
using System;

namespace PayrollEngine
{
    /// <summary>Payroll engine logger</summary>
    public interface ILogger
    {

        /// <summary>Checks if the given <paramref name="logLevel"/> is enabled</summary>
        /// <param name="logLevel">Level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        bool IsEnabled(LogLevel logLevel);

        #region Write

        /// <summary>Write a log event with the specified level</summary>
        /// <param name="level">The level of the event</param>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        void Write(LogLevel level, string messageTemplate, params object[] propertyValues);

        /// <summary>Write a log event with the specified level and associated exception</summary>
        /// <param name="level">The level of the event</param>
        /// <param name="exception">Exception related to the event</param>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        void Write(LogLevel level, Exception exception, string messageTemplate, params object[] propertyValues);

        #endregion

        #region Trace

        /// <summary>Write a log event with the <see cref="LogLevel.Verbose"/> level</summary>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        void Trace(string messageTemplate, params object[] propertyValues);

        /// <summary>Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception</summary>
        /// <param name="exception">Exception related to the event</param>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        void Trace(Exception exception, string messageTemplate, params object[] propertyValues);

        #endregion

        #region Debug

        /// <summary>Write a log event with the <see cref="LogLevel.Debug"/> level</summary>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        void Debug(string messageTemplate, params object[] propertyValues);

        /// <summary>Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception</summary>
        /// <param name="exception">Exception related to the event</param>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Debug(ex, "Swallowing a mundane exception");
        /// </example>
        void Debug(Exception exception, string messageTemplate, params object[] propertyValues);

        #endregion

        #region Information

        /// <summary>Write a log event with the <see cref="LogLevel.Information"/> level</summary>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        void Information(string messageTemplate, params object[] propertyValues);

        /// <summary>Write a log event with the <see cref="LogLevel.Information"/> level and associated exception</summary>
        /// <param name="exception">Exception related to the event</param>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        void Information(Exception exception, string messageTemplate, params object[] propertyValues);

        #endregion

        #region Warning

        /// <summary>Write a log event with the <see cref="LogLevel.Warning"/> level</summary>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        void Warning(string messageTemplate, params object[] propertyValues);

        /// <summary>Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception</summary>
        /// <param name="exception">Exception related to the event</param>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        void Warning(Exception exception, string messageTemplate, params object[] propertyValues);

        #endregion

        #region Error

        /// <summary>Write a log event with the <see cref="LogLevel.Error"/> level</summary>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        void Error(string messageTemplate, params object[] propertyValues);

        /// <summary>Write a log event with the <see cref="LogLevel.Error"/> level and associated exception</summary>
        /// <param name="exception">Exception related to the event</param>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        void Error(Exception exception, string messageTemplate, params object[] propertyValues);

        #endregion

        #region Critical

        /// <summary>Write a log event with the <see cref="LogLevel.Fatal"/> level</summary>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Fatal("Process terminating.");
        /// </example>
        void Critical(string messageTemplate, params object[] propertyValues);

        /// <summary>Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception</summary>
        /// <param name="exception">Exception related to the event</param>
        /// <param name="messageTemplate">Message template describing the event</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template</param>
        /// <example>
        /// Log.Fatal(ex, "Process terminating.");
        /// </example>
        void Critical(Exception exception, string messageTemplate, params object[] propertyValues);

        #endregion

    }
}
