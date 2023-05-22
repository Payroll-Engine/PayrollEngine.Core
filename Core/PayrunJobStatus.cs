using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>
/// The payrun job status
/// Supported state changes:
/// - *: Draft | Forecast | Abort
/// - Draft: Release | Abort
/// - Release: Process | Abort
/// - Process: Complete | Cancel
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PayrunJobStatus
{
    /// <summary>Draft Legal results (default)</summary>
    Draft,

    /// <summary>Legal results are released for processing</summary>
    Release,

    /// <summary>Legal results are processed</summary>
    Process,

    /// <summary>Legal results has been processed successfully</summary>
    Complete,

    /// <summary>Forecast results</summary>
    Forecast,

    /// <summary>Unreleased Job has been aborted</summary>
    Abort,

    /// <summary>Released Job has been canceled</summary>
    Cancel
}