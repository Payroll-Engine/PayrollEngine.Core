using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The case result kind</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ResultKind
{
    /// <summary>Collector result</summary>
    Collector = 10,

    /// <summary>Collector custom result</summary>
    CollectorCustom = 11,

    /// <summary>Wage type result</summary>
    WageType = 20,

    /// <summary>Wage type custom result</summary>
    WageTypeCustom = 21,

    /// <summary>Payrun result</summary>
    Payrun = 30
}