using System;

namespace PayrollEngine;

/// <summary>Calculates and logs the cache ratio</summary>
public class CacheRatio
{
    /// <summary>The cache ratio name</summary>
    public string Name { get; }

    /// <summary>The logger level</summary>
    public LogLevel LogLevel { get; }

    /// <summary>Hit count</summary>
    public int HitCount { get; private set; }

    /// <summary>Missed count</summary>
    public int MissedCount { get; private set; }

    /// <summary>Calculate the cache hit ratio.
    /// See https://wp-rocket.me/blog/calculate-hit-and-miss-ratios/
    /// </summary>
    /// <returns>The cache hit ratio</returns>
    public decimal HitRatio =>
        (decimal)HitCount / (HitCount + MissedCount);

    /// <summary>A new stopwatch</summary>
    public CacheRatio(string name, LogLevel logLevel = LogLevel.Information)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Invalid stopwatch name", nameof(name));
        }

        Name = name;
        LogLevel = logLevel;
    }

    /// <summary>Updates and logs the cache ratio</summary>
    /// <param name="cached">The cache state</param>
    public void UpdateWithLog(bool cached)
    {
        if (cached)
        {
            HitCount++;
        }
        else
        {
            MissedCount++;
        }
        Log.Write(LogLevel, $"{Name}: hit={HitCount}, missed={MissedCount}, hit-ratio=[{HitRatio:#0.###}]");
    }
}