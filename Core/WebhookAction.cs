using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The Webhook message type</summary>
/// <remarks>Values 20 and 30 belonged to removed actions and must not be reused</remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WebhookAction
{
    /// <summary>No message</summary>
    None = 0,

    /// <summary>Case function request</summary>
    CaseFunctionRequest = 10,

    /// <summary>Case change added</summary>
    CaseChangeAdded = 11,

    /// <summary>Process payrun job</summary>
    PayrunJobProcess = 21,

    /// <summary>Payrun job finished</summary>
    PayrunJobFinish = 22,

    /// <summary>Task change</summary>
    TaskChange = 40
}