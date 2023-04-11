using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>Enumeration with the year months</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Month
{
    /// <summary>Not set</summary>
    NotSet = 0,

    /// <summary>Indicates January</summary>
    January = 1,

    /// <summary>Indicates February</summary>
    February = 2,

    /// <summary>Indicates March</summary>
    March = 3,

    /// <summary>Indicates April</summary>
    April = 4,

    /// <summary>Indicates May</summary>
    May = 5,

    /// <summary>Indicates June</summary>
    June = 6,

    /// <summary>Indicates July</summary>
    July = 7,

    /// <summary>Indicates August</summary>
    August = 8,

    /// <summary>Indicates September</summary>
    September = 9,

    /// <summary>Indicates October</summary>
    October = 10,

    /// <summary>Indicates November</summary>
    November = 11,

    /// <summary>Indicates December</summary>
    December = 12
}