namespace PayrollEngine.Action;

/// <summary>Extension for action marker type</summary>
public static class MarkerTypeExtension
{
    /// <param name="markerType">Marker type</param>
    extension(MarkerType markerType)
    {
        /// <summary>
        /// Test for supported function
        /// </summary>
        /// <param name="functionType">Function type</param>
        /// <returns></returns>
        public bool SupportedFunction(FunctionType functionType)
        {
            return markerType switch
            {
                MarkerType.CaseValue or MarkerType.LookupValue =>
                    functionType.HasFlag(FunctionType.CaseAvailable) ||
                    functionType.HasFlag(FunctionType.CaseBuild) ||
                    functionType.HasFlag(FunctionType.CaseValidate) ||
                    functionType.HasFlag(FunctionType.CaseRelationBuild) ||
                    functionType.HasFlag(FunctionType.CaseRelationValidate) ||
                    functionType.HasFlag(FunctionType.CollectorStart) ||
                    functionType.HasFlag(FunctionType.CollectorApply) ||
                    functionType.HasFlag(FunctionType.CollectorEnd) ||
                    functionType.HasFlag(FunctionType.WageTypeValue) ||
                    functionType.HasFlag(FunctionType.WageTypeResult),
                MarkerType.CaseField =>
                    functionType.HasFlag(FunctionType.CaseAvailable) ||
                    functionType.HasFlag(FunctionType.CaseBuild) ||
                    functionType.HasFlag(FunctionType.CaseValidate),
                MarkerType.SourceCaseField or MarkerType.TargetCaseField =>
                    functionType.HasFlag(FunctionType.CaseRelationBuild) ||
                    functionType.HasFlag(FunctionType.CaseRelationValidate),
                MarkerType.RuntimeValue or MarkerType.PayrunResult =>
                    functionType.HasFlag(FunctionType.CollectorStart) ||
                    functionType.HasFlag(FunctionType.CollectorApply) ||
                    functionType.HasFlag(FunctionType.CollectorEnd) ||
                    functionType.HasFlag(FunctionType.WageTypeValue) ||
                    functionType.HasFlag(FunctionType.WageTypeResult),
                MarkerType.Collector or MarkerType.WageType =>
                    functionType.HasFlag(FunctionType.WageTypeValue) ||
                    functionType.HasFlag(FunctionType.WageTypeResult),
                _ => true
            };
        }

        /// <summary>
        /// Get action marker syntax
        /// </summary>
        /// <returns></returns>
        public string GetSyntax()
        {
            switch (markerType)
            {
                case MarkerType.Condition:
                    return ActionSpecification.ActionConditionMarker.ToString();
                case MarkerType.ConditionTrue:
                    return $"{ActionSpecification.ActionConditionMarker}{ActionSpecification.ActionConditionTrueMarker}";
                case MarkerType.ConditionFalse:
                    return $"{ActionSpecification.ActionConditionMarker}{ActionSpecification.ActionConditionFalseMarker}";
                case MarkerType.LookupValue:
                    return $"{ActionSpecification.RefTokenMarker}{ActionSpecification.LookupTokenMarker}";
                case MarkerType.CaseField:
                    return $"{ActionSpecification.RefTokenMarker}{ActionSpecification.CaseFieldTokenMarker}";
                case MarkerType.SourceCaseField:
                    return $"{ActionSpecification.RefTokenMarker}{ActionSpecification.SourceCaseFieldTokenMarker}";
                case MarkerType.TargetCaseField:
                    return $"{ActionSpecification.RefTokenMarker}{ActionSpecification.TargetCaseFieldTokenMarker}";
                case MarkerType.CaseValue:
                    return $"{ActionSpecification.RefTokenMarker}{ActionSpecification.CaseValueTokenMarker}";
                case MarkerType.RuntimeValue:
                    return $"{ActionSpecification.RefTokenMarker}{ActionSpecification.RuntimeValueTokenMarker}";
                case MarkerType.PayrunResult:
                    return $"{ActionSpecification.RefTokenMarker}{ActionSpecification.PayrunResultTokenMarker}";
                case MarkerType.Collector:
                    return $"{ActionSpecification.RefTokenMarker}{ActionSpecification.CollectorTokenMarker}";
                case MarkerType.WageType:
                    return $"{ActionSpecification.RefTokenMarker}{ActionSpecification.WageTypeTokenMarker}";
            }
            return null;
        }
    }
}