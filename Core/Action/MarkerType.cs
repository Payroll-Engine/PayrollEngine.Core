using System.Text.Json.Serialization;

namespace PayrollEngine.Action;

/// <summary>Action marker type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MarkerType
{
    /// <summary>Condition marker</summary>
    Condition,
    /// <summary>Condition true marker</summary>
    ConditionTrue,
    /// <summary>Condition false marker</summary>
    ConditionFalse,
    /// <summary>Lookup value marker</summary>
    LookupValue,
    /// <summary>Case field marker</summary>
    CaseField,
    /// <summary>Source case field marker</summary>
    SourceCaseField,
    /// <summary>Target case field marker</summary>
    TargetCaseField,
    /// <summary>Case value marker</summary>
    CaseValue,
    /// <summary>Runtime value marker</summary>
    RuntimeValue,
    /// <summary>Payrun result marker</summary>
    PayrunResult,
    /// <summary>Collector marker</summary>
    Collector,
    /// <summary>Wage type marker</summary>
    WageType
}