using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The type of the user</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserType
{
    /// <summary>User with employee self-service</summary>
    Employee = 0,

    /// <summary>Regular payroll user</summary>
    User = 1,

    /// <summary>Tenant administrator</summary>
    TenantAdministrator = 2,

    /// <summary>System administrator</summary>
    SystemAdministrator = 3
}