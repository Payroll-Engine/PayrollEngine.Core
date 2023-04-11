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
    /// <summary>Web Resource e.g. Url (base type string)</summary>
    WebResource = 11,

    /// <summary>Money (base type numeric)</summary>
    Money = 20,
    /// <summary>Percentage (base type numeric)</summary>
    Percent = 21,
    /// <summary>Hour (base type numeric)</summary>
    Hour = 22,
    /// <summary>Day (base type numeric)</summary>
    Day = 23,
    /// <summary>Week (base type numeric)</summary>
    Week = 24,
    /// <summary>Month (base type numeric)</summary>
    Month = 25,
    /// <summary>Year (base type numeric)</summary>
    Year = 26,
    /// <summary>Distance (base type numeric)</summary>
    Distance = 27
}