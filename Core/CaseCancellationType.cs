using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The cancellation type of a case</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseCancellationType
{
    /// <summary>No cancellation support</summary>
    None = 0,

    /// <summary>Cancellation by case</summary>
    Case = 1
}