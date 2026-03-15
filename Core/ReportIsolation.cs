using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The report data isolation level.
/// Controls which context data (division, employee) is automatically
/// injected into the report request.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReportIsolation
{
    /// <summary>No isolation — report receives no automatic context injection.</summary>
    None,

    /// <summary>Global isolation — report is scoped to the tenant.</summary>
    Global,

    /// <summary>Company isolation — report is scoped to the company case values.</summary>
    Company,

    /// <summary>Division isolation — report receives the division context.</summary>
    Division,

    /// <summary>Employee isolation — report receives the employee context.</summary>
    Employee
}
