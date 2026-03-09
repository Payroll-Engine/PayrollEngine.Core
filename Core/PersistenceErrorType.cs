using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>Persistence error type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PersistenceErrorType
{
    /// <summary>Unique constraint error (SQL 2601, 2627)</summary>
    UniqueConstraint,

    /// <summary>Foreign key or check constraint violation (SQL 547)</summary>
    ConstraintViolation,

    /// <summary>NOT NULL constraint violation (SQL 515)</summary>
    NotNullViolation
}