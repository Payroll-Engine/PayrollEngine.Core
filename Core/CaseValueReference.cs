using System;
using System.Linq;

namespace PayrollEngine;

/// <summary>
/// Reference to case value
/// </summary>
public class CaseValueReference
{
    /// <summary>The case field slot separator</summary>
    public static readonly char CaseFieldSlotSeparator = ':';

    /// <summary>The case field reference</summary>
    public string Reference { get; }

    /// <summary>The case field name</summary>
    public string CaseFieldName { get; }

    /// <summary>Test if slot is present</summary>
    public bool HasCaseSlot { get; }

    /// <summary>The case slot</summary>
    public string CaseSlot { get; }

    /// <summary>New instance of <see cref="CaseValueReference"/></summary>
    /// <param name="reference">The case field reference</param>
    public CaseValueReference(string reference)
    {
        if (string.IsNullOrWhiteSpace(reference))
        {
            throw new ArgumentException(nameof(reference));
        }
        if (reference.Count(x => x == CaseFieldSlotSeparator) > 1)
        {
            throw new FormatException($"Invalid case value reference {reference}");
        }

        Reference = reference;
        var index = reference.IndexOf(CaseFieldSlotSeparator);
        HasCaseSlot = index > 0;
        if (HasCaseSlot)
        {
            CaseFieldName = reference.Substring(0, index);
            CaseSlot = reference.Substring(index + 1);
        }
        else
        {
            CaseFieldName = reference;
            CaseSlot = null;
        }
    }

    /// <summary>New instance of <see cref="CaseValueReference"/></summary>
    /// <param name="caseFieldName">The case field name</param>
    /// <param name="caseSlot">The case slot</param>
    public CaseValueReference(string caseFieldName, string caseSlot)
    {
        if (string.IsNullOrWhiteSpace(caseFieldName))
        {
            throw new ArgumentException(nameof(caseFieldName));
        }
        if (caseFieldName.Contains(CaseFieldSlotSeparator))
        {
            throw new FormatException($"Invalid case field name {caseFieldName}");
        }
        if (caseSlot != null && caseSlot.Contains(CaseFieldSlotSeparator))
        {
            throw new FormatException($"Invalid case slot {caseSlot}");
        }

        CaseFieldName = caseFieldName;
        CaseSlot = caseSlot;
        HasCaseSlot = !string.IsNullOrWhiteSpace(caseSlot);
        Reference = HasCaseSlot ? $"{caseFieldName}{CaseFieldSlotSeparator}{caseSlot}" : caseFieldName;
    }

    /// <summary>Convert to case value reference</summary>
    /// <param name="caseFieldName">The case field name</param>
    /// <param name="caseSlot">The case slot</param>
    /// <returns>The case value reference</returns>
    public static string ToReference(string caseFieldName, string caseSlot) =>
        new CaseValueReference(caseFieldName, caseSlot).Reference;

    /// <summary>The case value reference</summary>
    /// <returns>The string representation</returns>
    public override string ToString() => Reference;
}