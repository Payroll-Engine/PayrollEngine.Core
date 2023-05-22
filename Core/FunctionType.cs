using System;
using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The payroll function type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
[Flags]
public enum FunctionType
{

    // combined functions
    /// <summary>All functions</summary>
    All = Payroll | Report,
    /// <summary>All payrun with regulation functions</summary>
    Payroll = Case | CaseRelation | Payrun,
    /// <summary>All case functions</summary>
    Case = CaseAvailable | CaseChange,
    /// <summary>All case functions</summary>
    CaseChange = CaseBuild | CaseValidate,
    /// <summary>All case relation functions</summary>
    CaseRelation = CaseRelationBuild | CaseRelationValidate,
    /// <summary>All report functions</summary>
    Payrun = PayrunStart | PayrunEmployeeAvailable | PayrunEmployeeStart |
             PayrunEmployeeEnd | PayrunWageTypeAvailable | PayrunEnd |
             Collector | WageType,
    /// <summary>All collector functions</summary>
    Collector = CollectorStart | CollectorApply | CollectorEnd,
    /// <summary>All wage type functions</summary>
    WageType = WageTypeValue | WageTypeResult,
    /// <summary>All report functions</summary>
    Report = ReportBuild | ReportStart | ReportEnd,

    // case
    /// <summary>Case available function</summary>
    CaseAvailable = 0x00000001,
    /// <summary>Case build function</summary>
    CaseBuild = 0x00000002,
    /// <summary>Case validate function</summary>
    CaseValidate = 0x00000004,

    // case relation
    /// <summary>Case relation build function</summary>
    CaseRelationBuild = 0x00000008,
    /// <summary>Case relation validate function</summary>
    CaseRelationValidate = 0x00000010,

    // collector
    /// <summary>Collector start function</summary>
    CollectorStart = 0x00000020,
    /// <summary>Collector apply function</summary>
    CollectorApply = 0x00000040,
    /// <summary>Collector end function</summary>
    CollectorEnd = 0x00000080,

    // wage type
    /// <summary>Wage type value function</summary>
    WageTypeValue = 0x00000100,
    /// <summary>Wage type result function</summary>
    WageTypeResult = 0x00000200,

    // payrun
    /// <summary>Payrun start function</summary>
    PayrunStart = 0x00004000,
    /// <summary>Payrun employee available function</summary>
    PayrunEmployeeAvailable = 0x00008000,
    /// <summary>Payrun employee start function</summary>
    PayrunEmployeeStart = 0x00010000,
    /// <summary>Payrun employee end function</summary>
    PayrunEmployeeEnd = 0x00020000,
    /// <summary>Payrun wage type available function</summary>
    PayrunWageTypeAvailable = 0x00040000,
    /// <summary>Payrun end function</summary>
    PayrunEnd = 0x00080000,

    // report
    /// <summary>Report build function</summary>
    ReportBuild = 0x00100000,
    /// <summary>Report start function</summary>
    ReportStart = 0x00200000,
    /// <summary>Report end function</summary>
    ReportEnd = 0x00400000
}