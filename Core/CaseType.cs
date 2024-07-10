using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The case type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseType
{
    /// <summary>Global case</summary>
    Global = 0,

    /// <summary>National case</summary>
    National = 1,

    /// <summary>Company case</summary>
    Company = 2,

    /// <summary>Employee case</summary>
    Employee = 3
}