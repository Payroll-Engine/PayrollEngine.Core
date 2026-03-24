using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>Server-wide policy controlling cross-tenant access on a backend instance.
/// Configured once at startup in PayrollServerConfiguration — not changeable at runtime.
/// Applied by TenantIsolationFilter before every request.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TenantIsolationLevel
{
    /// <summary>No cross-tenant access permitted (default).
    /// All requests must carry the Auth-Tenant header and are scoped to a single tenant.
    /// The RegulationShare controller is hidden and consolidation queries are blocked.
    /// Use for single-tenant or strictly isolated multi-tenant deployments.</summary>
    None = 0,

    /// <summary>Cross-tenant access permitted for internal consolidation queries only.
    /// Enables ExecuteConsolidatedQuery in report scripts via Consolidation-level RegulationShares.
    /// The RegulationShare controller is accessible.
    /// HTTP endpoints remain single-tenant scoped — the Auth-Tenant header is still required.
    /// Use when consolidation report scripts must aggregate results across provider tenants.</summary>
    Consolidation = 1,

    /// <summary>Cross-tenant read access permitted via HTTP.
    /// All GET requests and semantically read-only POST requests (report execution,
    /// case build, etc.) may omit the Auth-Tenant header and access any tenant.
    /// Mutating requests (PUT, PATCH, DELETE, write POST) still require the Auth-Tenant header.
    /// Use for read-only multi-tenant integrations.</summary>
    Read = 2,

    /// <summary>Full cross-tenant access permitted via HTTP.
    /// All request methods may access any tenant without the Auth-Tenant header.
    /// The Auth-Tenant header must NOT be sent — its presence is a configuration conflict
    /// and results in 400 Bad Request.
    /// Use only for trusted internal deployments where full multi-tenant access is required.</summary>
    Write = 3
}
