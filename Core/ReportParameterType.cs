using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The report value types</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReportParameterType
{
    /// <summary>Custom value</summary>
    Value = 0,

    /// <summary>The current date and time</summary>
    Now = 10,
    /// <summary>The current day</summary>
    Today = 11,

    /// <summary>Tenant id</summary>
    TenantId = 100,
    /// <summary>User id</summary>
    UserId = 110,

    /// <summary>Employee id or identifier</summary>
    EmployeeId = 200,
    /// <summary>Regulation id or name</summary>
    RegulationId = 210,
    /// <summary>Payroll id or name</summary>
    PayrollId = 220,
    /// <summary>Payrun id or name</summary>
    PayrunId = 230,
    /// <summary>Report id or name</summary>
    ReportId = 240,
    /// <summary>Webhook id or name</summary>
    WebhookId = 250
}