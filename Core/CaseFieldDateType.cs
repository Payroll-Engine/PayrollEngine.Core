using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The case field date type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseFieldDateType
{
    /// <summary>Day</summary>
    Day = 0,

    /// <summary>Indicates Sunday</summary>
    Sunday = 1,
    /// <summary>Indicates Monday</summary>
    Monday = 2,
    /// <summary>Indicates Tuesday</summary>
    Tuesday = 3,
    /// <summary>Indicates Wednesday</summary>
    Wednesday = 4,
    /// <summary>Indicates Thursday</summary>
    Thursday = 5,
    /// <summary>Indicates Friday</summary>
    Friday = 6,
    /// <summary>Indicates Saturday</summary>
    Saturday = 7,

    /// <summary>Month</summary>
    Month = 10,

    /// <summary>Indicates January</summary>
    January = 11,
    /// <summary>Indicates February</summary>
    February = 12,
    /// <summary>Indicates March</summary>
    March = 13,
    /// <summary>Indicates April</summary>
    April = 14,
    /// <summary>Indicates May</summary>
    May = 15,
    /// <summary>Indicates June</summary>
    June = 16,
    /// <summary>Indicates July</summary>
    July = 17,
    /// <summary>Indicates August</summary>
    August = 18,
    /// <summary>Indicates September</summary>
    September = 19,
    /// <summary>Indicates October</summary>
    October = 20,
    /// <summary>Indicates November</summary>
    November = 21,
    /// <summary>Indicates December</summary>
    December = 22,

    /// <summary>Year</summary>
    Year = 30
}