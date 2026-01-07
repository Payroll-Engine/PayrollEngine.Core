using System;

namespace PayrollEngine;

/// <summary>Convert extensions for <see cref="string"/></summary>
public static class StringConvertExtensions
{
    /// <summary>The related case separator</summary>
    public static readonly char RelatedCaseSeparator = ':';

    /// <summary>The case field slot separator</summary>
    private static readonly char CaseFieldSlotSeparator = ':';

    /// <param name="reference">The case relation reference</param>
    extension(string reference)
    {
        /// <summary>Extract related cases from a case relation string, format is 'sourceCaseName:targetCaseName'</summary>
        /// <returns>The related cases a tuple: item1=source case, item2=target case</returns>
        public Tuple<string, string> ReferenceToRelatedCases()
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            var relatedCases = reference.Split(RelatedCaseSeparator, StringSplitOptions.RemoveEmptyEntries);
            if (relatedCases.Length != 2)
            {
                throw new ArgumentException($"invalid case relation {reference}, please use 'sourceCaseName:targetCaseName').");
            }
            return new(relatedCases[0], relatedCases[1]);
        }

        /// <summary>Build related case reference string, format is 'sourceCaseName:targetCaseName'</summary>
        /// <param name="targetCaseName">The target case name</param>
        /// <returns>The related cases reference</returns>
        public string RelatedCasesToReference(string targetCaseName)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(targetCaseName))
            {
                return null;
            }
            return $"{reference}{RelatedCaseSeparator}{targetCaseName}";
        }

        /// <summary>Extract case field name and slot form string, format is 'caseName:slotName'</summary>
        /// <returns>The case field slot reference a tuple: item1=case name, item2=slot name</returns>
        public Tuple<string, string> ReferenceToCaseFieldSlot()
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            var caseFieldSlot = reference.Split(CaseFieldSlotSeparator, StringSplitOptions.RemoveEmptyEntries);
            if (caseFieldSlot.Length != 2)
            {
                throw new ArgumentException($"invalid case field slot {reference}, please use 'caseName:slotName').");
            }
            return new(caseFieldSlot[0], caseFieldSlot[1]);
        }

        /// <summary>Build case field slot reference string, format is 'caseName:slotName'</summary>
        /// <param name="slotName">The case slot name</param>
        /// <returns>The case field slot reference</returns>
        public string CaseFieldSlotToReference(string slotName)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(slotName))
            {
                return null;
            }
            return $"{reference}{CaseFieldSlotSeparator}{slotName}";
        }
    }
}