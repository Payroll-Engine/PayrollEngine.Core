using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The cancellation mode for a regulation case field</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseFieldCancellationMode
{
    /// <summary>Cancel by value time type: moment is invert, all others are previous</summary>
    TimeType = 0,

    /// <summary>Previous value, reset if no previous value is available</summary>
    Previous = 1,

    /// <summary>Keep existing value</summary>
    Keep = 2,

    /// <summary>Reset to default value, numbers=zero, boolean=false</summary>
    Reset = 3,

    /// <summary>Invert value, numbers=value * -1, boolean=switch value</summary>
    Invert = 4
}