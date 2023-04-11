using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The case value creation mode</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseValueCreationMode
{
    /// <summary>Update on changes of the case value start, end or value</summary>
    OnChanges = 0,

    /// <summary>Create value always</summary>
    Always = 1,

    /// <summary>Discard value</summary>
    Discard = 2
}