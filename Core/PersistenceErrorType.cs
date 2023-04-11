using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>Persistence error type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PersistenceErrorType
{
    /// <summary>Unique constraint error</summary>
    UniqueConstraint
}