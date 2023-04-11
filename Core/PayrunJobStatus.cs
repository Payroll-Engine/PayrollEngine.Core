using System;
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
[Flags]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PayrunJobStatus
{
    /// <summary>Draft Legal results (default)</summary>
    Draft = 0x0000,

    /// <summary>Legal results are released for processing</summary>
    Release = 0x0001,

    /// <summary>Legal results are processed</summary>
    Process = 0x0002,

    /// <summary>Legal results has been processed successfully</summary>
    Complete = 0x0004,

    /// <summary>Forecast results</summary>
    Forecast = 0x0008,

    /// <summary>Unreleased Job has been aborted</summary>
    Abort = 0x0010,

    /// <summary>Released Job has been canceled</summary>
    Cancel = 0x0020,

    /// <summary>Working status, including draft, release and process</summary>
    Working = Draft | Release | Process,

    /// <summary>Legal status, including release, process and complete</summary>
    Legal = Release | Process | Complete,

    /// <summary>Final status, including complete, forecast, abort and cancel</summary>
    Final = Complete | Forecast | Abort | Cancel
}