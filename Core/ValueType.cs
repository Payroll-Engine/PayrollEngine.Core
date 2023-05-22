using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The payroll value types for cases</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ValueType
{
    /// <summary>String (base type string)</summary>
    String = 0,
    /// <summary>Boolean (base type boolean)</summary>
    Boolean = 1,
    /// <summary>Integer (base type numeric)</summary>
    Integer = 2,
    /// <summary>Numeric boolean, any non-zero value means true (base type numeric)</summary>
    NumericBoolean = 3,
    /// <summary>Decimal (base type numeric)</summary>
    Decimal = 4,
    /// <summary>Date and time (base type string)</summary>
    DateTime = 5,
    /// <summary>No value type (base type null)</summary>
    None = 6,

    /// <summary>Date (base type string)</summary>
    Date = 10,

    /// <summary>Day of week (base type integer 0..6)</summary>
    Weekday = 20,
    /// <summary>Month (base type integer 0..11)</summary>
    Month = 21,
    /// <summary>Year (base type integer)</summary>
    Year = 22,

    /// <summary>Money (base type decimal)</summary>
    Money = 30,
    /// <summary>Percentage (base type decimal)</summary>
    Percent = 31,

    /// <summary>Web Resource e.g. Url (base type string)</summary>
    WebResource = 40
}