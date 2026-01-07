namespace PayrollEngine.Action;

/// <summary>The action specification</summary>
public static class ActionSpecification
{
    /// <summary>Action comment marker</summary>
    public static char ActionCommentMarker => '#';

    /// <summary>Action condition marker</summary>
    public static char ActionConditionMarker => '?';

    /// <summary>Action condition true marker</summary>
    public static char ActionConditionTrueMarker => '=';

    /// <summary>Action condition false marker</summary>
    public static char ActionConditionFalseMarker => '!';

    /// <summary>Token reference marker</summary>
    public static char RefTokenMarker => '^';

    /// <summary>Lookup value token marker</summary>
    public static char LookupTokenMarker => '#';

    /// <summary>Case value token marker</summary>
    public static char CaseValueTokenMarker => '^';

    /// <summary>Case field token marker</summary>
    public static char CaseFieldTokenMarker => ':';

    /// <summary>Source case field token marker</summary>
    public static char SourceCaseFieldTokenMarker => '<';

    /// <summary>Target case field token marker</summary>
    public static char TargetCaseFieldTokenMarker => '>';

    /// <summary>Runtime value token marker</summary>
    public static char RuntimeValueTokenMarker => '|';

    /// <summary>Payrun result token marker</summary>
    public static char PayrunResultTokenMarker => '@';

    /// <summary>Collector token marker</summary>
    public static char CollectorTokenMarker => '&';

    /// <summary>Wage type token marker</summary>
    public static char WageTypeTokenMarker => '$';
}
