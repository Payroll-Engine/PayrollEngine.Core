using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="PayrunJobStatus"/></summary>
public static class PayrunJobStatusExtensions
{
    /// <summary>Test if the payrun job is in a working state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a working state</returns>
    public static bool IsWorkingState(this PayrunJobStatus jobStatus) =>
        jobStatus is PayrunJobStatus.Draft or PayrunJobStatus.Release or PayrunJobStatus.Process;

    /// <summary>Test if the payrun job is in a final state</summary>
    /// <param name="jobStatus">The payrun job status</param>
    /// <returns>True, if the state is a final state</returns>
    public static bool IsFinalState(this PayrunJobStatus jobStatus) =>
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