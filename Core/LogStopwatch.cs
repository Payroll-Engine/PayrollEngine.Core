#define LOG_STOPWATCH
using System.Collections.Concurrent;
using System.Diagnostics;

namespace PayrollEngine;

/// <summary>
/// A log stopwatch
/// </summary>
/// <remarks>
/// Activate with the pre-processor directive
/// #define LOG_STOPWATCH
/// </remarks>
// source: https://stackoverflow.com/a/11239345
public static class LogStopwatch
{
    private static readonly ConcurrentDictionary<string, Stopwatch> Stopwatches = new();

    /// <summary>Start a new log stopwatch by name</summary>
    /// <param name="name">The stopwatch name</param>
    [Conditional("LOG_STOPWATCH")]
    public static void Start(string name)
    {
        if (Stopwatches.ContainsKey(name))
        {
            Log.Trace($"Ignoring restart of log stopwatch {name}");
            // stopwatch already running
            return;
        }

        if (!Stopwatches.TryAdd(name, Stopwatch.StartNew()))
        {
            Log.Warning($"Start of log stopwatch {name} failed");
        }
    }

    /// <summary>Stop a log stopwatch by name</summary>
    /// <param name="name">The stopwatch name</param>
    /// <param name="logLevel">The logger level, default is information</param>
    [Conditional("LOG_STOPWATCH")]
    public static void Stop(string name, LogLevel logLevel = LogLevel.Information)
    {
        if (!Stopwatches.ContainsKey(name))
        {
            Log.Warning($"Unknown log stopwatch {name} to stop");
            // no such stopwatch currently
            return;
        }

        if (!Stopwatches.TryRemove(name, out var watch))
        {
            Log.Warning($"Stop of log stopwatch {name} failed");
            return;
        }
        watch.Stop();
        Log.Write(logLevel, $"{name}: {watch.Elapsed.TotalMilliseconds} ms");
    }
}