using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The payrun execution stage</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PayrunExecutionPhase
{
    /// <summary>Job setup execution phase</summary>
    Setup = 0,

    /// <summary>Job reevaluation execution phase</summary>
    Reevaluation = 1
}