using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The type of a case issue</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseIssueType
{
    /// <summary>Unknown issue</summary>
    Unknown = 0,

    /// <summary>Case validation failed</summary>
    CaseInvalid = 1,

    /// <summary>Unknown case</summary>
    CaseUnknown = 2,

    /// <summary>Case relation validation failed</summary>
    CaseRelationInvalid = 3,

    /// <summary>Duplicated case field</summary>
    CaseFieldDuplicated = 4,

    /// <summary>Incomplete case value</summary>
    CaseValueIncomplete = 5,

    /// <summary>Missing case value</summary>
    CaseValueMissing = 6,

    /// <summary>Invalid case value start date</summary>
    CaseValueStartInvalid = 7,

    /// <summary>Invalid case value end date</summary>
    CaseValueEndInvalid = 8,

    /// <summary>Missing case value end date</summary>
    CaseValueEndMissing = 9,

    /// <summary>Invalid weekday (0..6)</summary>
    CaseValueWeekdayInvalid = 10,

    /// <summary>Invalid month (0..11)</summary>
    CaseValueMonthInvalid = 11
}