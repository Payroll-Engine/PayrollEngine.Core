using System;
using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The Webhook message type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WebhookAction
{
    /// <summary>No message</summary>
    None = 0,

    /// <summary>Case function request</summary>
    CaseFunctionRequest = 10,

    /// <summary>Case change added</summary>
    CaseChangeAdded = 11,

    /// <summary>Payrun function request (reserved, never triggered)</summary>
    [Obsolete("Never triggered by the backend. Scripts invoke webhooks with CaseFunctionRequest.")]
    PayrunFunctionRequest = 20,

    /// <summary>Process payrun job</summary>
    PayrunJobProcess = 21,

    /// <summary>Payrun job finished</summary>
    PayrunJobFinish = 22,

    /// <summary>Report function request (reserved, never triggered)</summary>
    [Obsolete("Never triggered by the backend. Scripts invoke webhooks with CaseFunctionRequest.")]
    ReportFunctionRequest = 30,

    /// <summary>Task change</summary>
    TaskChange = 40
}