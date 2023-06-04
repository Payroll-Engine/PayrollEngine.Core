using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>Decimal rounding types</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DecimalRounding
{
    /// <summary>No rounding</summary>
    None = 0,

    /// <summary>Round to nearest integer</summary>
    Integer = 1,

    /// <summary>Round to half (e.g. 50 cents)</summary>
    Half = 2,

    /// <summary>Round to one-fifth (e.g. 20 cents)</summary>
    Fifth = 5,

    /// <summary>Round to one-tenth (e.g. 10 cents)</summary>
    Tenth = 10,

    /// <summary>Round to one-twentieth (e.g. 5 cents)</summary>
    Twentieth = 20,

    /// <summary>Round to one-fiftieth (e.g. 2 cents)</summary>
    Fiftieth = 50,

    /// <summary>Round to one-hundredth (e.g. 1 cent)</summary>
    Hundredth = 100
}
