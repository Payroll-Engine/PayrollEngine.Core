using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="PayrunJobStatus"/></summary>
public static class PayrunJobStatusExtensions
{
    /// <summary>Test if the payrun job is in a draft state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a draft state</returns>
    public static bool IsDraft(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Draft;

    /// <summary>Test if the payrun job is in a release state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a release state</returns>
    public static bool IsRelease(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Release;

    /// <summary>Test if the payrun job is in a process state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a process state</returns>
    public static bool IsProcess(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Process;

    /// <summary>Test if the payrun job is in a complete state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a complete state</returns>
    public static bool IsComplete(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Complete;

    /// <summary>Test if the payrun job is in a forecast state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a forecast state</returns>
    public static bool IsForecast(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Forecast;

    /// <summary>Test if the payrun job is in a abort state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a abort state</returns>
    public static bool IsAbort(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Abort;

    /// <summary>Test if the payrun job is in a cancel state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a cancel state</returns>
    public static bool IsCancel(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Cancel;

    /// <summary>Test if the payrun job is in a working state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a working state</returns>
    public static bool IsWorking(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Draft or PayrunJobStatus.Release or PayrunJobStatus.Process;

    /// <summary>Test if the payrun job is in a working state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a working state</returns>
    public static bool IsLegal(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Release or PayrunJobStatus.Process or PayrunJobStatus.Complete;

    /// <summary>Test if the payrun job is in a final state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a final state</returns>
    public static bool IsFinal(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Complete or PayrunJobStatus.Forecast or PayrunJobStatus.Abort or PayrunJobStatus.Cancel;

    /// <summary>Test if the payrun job state change is handled by the webhook start message, change from final to transmit</summary>
    /// <param name="oldJobStatus">The source payrun job status</param>
    /// <param name="newJobStatus">The target payrun job status</param>
    /// <returns>True, if the state is a final state</returns>
    public static bool IsWebhookProcessChange(this PayrunJobStatus oldJobStatus, PayrunJobStatus newJobStatus) =>
        oldJobStatus == PayrunJobStatus.Release &&
        newJobStatus == PayrunJobStatus.Process;

    /// <summary>Test if the payrun job state change is handled by the webhook finished message,
    /// change from process to complete or failed/// </summary>
    /// <param name="oldJobStatus">The source payrun job status</param>
    /// <param name="newJobStatus">The target payrun job status</param>
    /// <returns>True, if the state is a final state</returns>
    public static bool IsWebhookFinishChange(this PayrunJobStatus oldJobStatus, PayrunJobStatus newJobStatus) =>
        oldJobStatus == PayrunJobStatus.Process &&
        newJobStatus is PayrunJobStatus.Complete or PayrunJobStatus.Cancel;

    /// <summary> Test if the state change is valid.
    /// Supported state changes:
    /// - *: Draft | Forecast | Abort
    /// - Draft: Release | Abort
    /// - Release: Process | Abort
    /// - Process: Complete | Cancel</summary>
    /// <param name="oldJobStatus">The source payrun job status</param>
    /// <param name="newJobStatus">The target payrun job status</param>
    /// <returns>True, if the state change is valid</returns>
    public static bool IsValidStateChange(this PayrunJobStatus oldJobStatus, PayrunJobStatus newJobStatus)
    {
        return oldJobStatus switch
        {
            PayrunJobStatus.Draft => newJobStatus is PayrunJobStatus.Release or PayrunJobStatus.Abort,
            PayrunJobStatus.Release => newJobStatus is PayrunJobStatus.Process or PayrunJobStatus.Abort,
            PayrunJobStatus.Process => newJobStatus is PayrunJobStatus.Complete or PayrunJobStatus.Cancel,
            PayrunJobStatus.Complete =>
                // no state change allowed on final states
                false,
            PayrunJobStatus.Forecast =>
                // no state change allowed on final states
                false,
            PayrunJobStatus.Abort =>
                // no state change allowed on final states
                false,
            PayrunJobStatus.Cancel =>
                // no state change allowed on final states
                false,
            _ => throw new ArgumentOutOfRangeException(nameof(oldJobStatus), oldJobStatus, null)
        };
    }
}