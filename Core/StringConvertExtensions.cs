using System;

namespace PayrollEngine;

/// <summary>Convert extensions for <see cref="string"/></summary>
public static class StringConvertExtensions
{
    /// <summary>The related case separator</summary>
    public static readonly char RelatedCaseSeparator = ':';

    /// <summary>The case field slot separator</summary>
    private static readonly char CaseFieldSlotSeparator = ':';

    /// <summary>Extract related cases from a case relation string, format is 'sourceCaseName:targetCaseName'</summary>
    /// <param name="reference">The case relation reference</param>
    /// <returns>The related cases a tuple: item1=source case, item2=target case</returns>
    public static Tuple<string, string> ReferenceToRelatedCases(this string reference)
    {
        if (string.IsNullOrWhiteSpace(reference))
        {
            return default;
        }

        var relatedCases = reference.Split(RelatedCaseSeparator, StringSplitOptions.RemoveEmptyEntries);
        if (relatedCases.Length != 2)
        {
            throw new ArgumentException($"invalid case relation {reference}, please use 'sourceCaseName:targetCaseName')");
        }
        return new(relatedCases[0], relatedCases[1]);
    }

    /// <summary>Build related case reference string, format is 'sourceCaseName:targetCaseName'</summary>
    /// <param name="sourceCaseName">The source case name</param>
    /// <param name="targetCaseName">The target case name</param>
    /// <returns>The related cases reference</returns>
    public static string RelatedCasesToReference(this string sourceCaseName, string targetCaseName)
    {
        if (string.IsNullOrWhiteSpace(sourceCaseName))
        {
            return default;
        }
        if (string.IsNullOrWhiteSpace(targetCaseName))
        {
            return default;
        }
        return $"{sourceCaseName}{RelatedCaseSeparator}{targetCaseName}";
    }

    /// <summary>Extract case field name and slot form string, format is 'caseName:slotName'</summary>
    /// <param name="reference">The case field slot reference</param>
    /// <returns>The case field slot reference a tuple: item1=case name, item2=slot name</returns>
    public static Tuple<string, string> ReferenceToCaseFieldSlot(this string reference)
    {
        if (string.IsNullOrWhiteSpace(reference))
        {
            return default;
        }

        var caseFieldSlot = reference.Split(CaseFieldSlotSeparator, StringSplitOptions.RemoveEmptyEntries);
        if (caseFieldSlot.Length != 2)
        {
            throw new ArgumentException($"invalid case field slot {reference}, please use 'caseName:slotName')");
        }
        return new(caseFieldSlot[0], caseFieldSlot[1]);
    }

    /// <summary>Build case field slot reference string, format is 'caseName:slotName'</summary>
    /// <param name="caseName">The case name</param>
    /// <param name="slotName">The case slot name</param>
    /// <returns>The case field slot reference</returns>
    public static string CaseFieldSlotToReference(this string caseName, string slotName)
    {
        if (string.IsNullOrWhiteSpace(caseName))
        {
            return default;
        }
        if (string.IsNullOrWhiteSpace(slotName))
        {
            return default;
        }
        return $"{caseName}{CaseFieldSlotSeparator}{slotName}";
    }
}