using System;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="PayrunJobStatus"/></summary>
public static class PayrunJobStatusExtensions
{
    /// <param name="jobStatus">The payrun job status</param>
    extension(PayrunJobStatus jobStatus)
    {
        /// <summary>Test if the payrun job is in draft state</summary>
        /// <returns>True, if the state is in draft state</returns>
        public bool IsDraft() =>
            jobStatus is PayrunJobStatus.Draft;

        /// <summary>Test if the payrun job is in release state</summary>
        /// <returns>True, if the state is in release state</returns>
        public bool IsRelease() =>
            jobStatus is PayrunJobStatus.Release;

        /// <summary>Test if the payrun job is in process state</summary>
        /// <returns>True, if the state is in process state</returns>
        public bool IsProcess() =>
            jobStatus is PayrunJobStatus.Process;

        /// <summary>Test if the payrun job is in complete state</summary>
        /// <returns>True, if the state is in complete state</returns>
        public bool IsComplete() =>
            jobStatus is PayrunJobStatus.Complete;

        /// <summary>Test if the payrun job is in forecast state</summary>
        /// <returns>True, if the state is in forecast state</returns>
        public bool IsForecast() =>
            jobStatus is PayrunJobStatus.Forecast;

        /// <summary>Test if the payrun job is in abort state</summary>
        /// <returns>True, if the state is in abort state</returns>
        public bool IsAbort() =>
            jobStatus is PayrunJobStatus.Abort;

        /// <summary>Test if the payrun job is in cancel state</summary>
        /// <returns>True, if the state is in cancel state</returns>
        public bool IsCancel() =>
            jobStatus is PayrunJobStatus.Cancel;

        /// <summary>Test if the payrun job is in working state</summary>
        /// <returns>True, if the state is in working state</returns>
        public bool IsWorking() =>
            jobStatus is PayrunJobStatus.Draft or PayrunJobStatus.Release or PayrunJobStatus.Process;

        /// <summary>Test if the payrun job is in working state</summary>
        /// <returns>True, if the state is in working state</returns>
        public bool IsLegal() =>
            jobStatus is PayrunJobStatus.Release or PayrunJobStatus.Process or PayrunJobStatus.Complete;

        /// <summary>Test if the payrun job is in final state</summary>
        /// <returns>True, if the state is in final state</returns>
        public bool IsFinal() =>
            jobStatus is PayrunJobStatus.Complete or PayrunJobStatus.Forecast or PayrunJobStatus.Abort or PayrunJobStatus.Cancel;

        /// <summary>Test if the payrun job state change is handled by the webhook start message, change from final to transmit</summary>
        /// <param name="newJobStatus">The target payrun job status</param>
        /// <returns>True, if the state is in final state</returns>
        public bool IsWebhookProcessChange(PayrunJobStatus newJobStatus) =>
            jobStatus == PayrunJobStatus.Release &&
            newJobStatus == PayrunJobStatus.Process;

        /// <summary>Test if the payrun job state change is handled by the webhook finished message,
        /// change from process to complete or failed/// </summary>
        /// <param name="newJobStatus">The target payrun job status</param>
        /// <returns>True, if the state is in final state</returns>
        public bool IsWebhookFinishChange(PayrunJobStatus newJobStatus) =>
            jobStatus == PayrunJobStatus.Process &&
            newJobStatus is PayrunJobStatus.Complete or PayrunJobStatus.Cancel;

        /// <summary> Test if the state change is valid.
        /// Supported state changes:
        /// - *: Draft | Forecast | Abort
        /// - Draft: Release | Abort
        /// - Release: Process | Abort
        /// - Process: Complete | Cancel</summary>
        /// <param name="newJobStatus">The target payrun job status</param>
        /// <returns>True, if the state change is valid</returns>
        public bool IsValidStateChange(PayrunJobStatus newJobStatus)
        {
            return jobStatus switch
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
                _ => throw new ArgumentOutOfRangeException(nameof(jobStatus), jobStatus, null)
            };
        }
    }
}