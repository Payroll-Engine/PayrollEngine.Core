using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The payrun job execution mode</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PayrunJobExecutionMode
{
    /// <summary>Execute payrun job synchronously</summary>
    Synchronous = 0,

    /// <summary>Execute payrun job asynchronously</summary>
    Asynchronous = 1
}