using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>Aggregation type for a period case field <see cref="CaseFieldTimeType.Period"/></summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseFieldAggregationType
{
    /// <summary>Summary of period values</summary>
    Summary = 0,

    /// <summary>The last period value</summary>
    Last = 1,

    /// <summary>The first period value</summary>
    First = 2
}