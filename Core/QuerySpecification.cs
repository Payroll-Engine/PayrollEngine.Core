namespace PayrollEngine;

/// <summary>The script specification</summary>
public static class QuerySpecification
{
    /// <summary>Status</summary>
    public static readonly string StatusOperation = "status";

    /// <summary>Order by</summary>
    public static readonly string OrderByOperation = "orderby";

    /// <summary>Order by ascending</summary>
    public static readonly string OrderByAscending = "ASC";

    /// <summary>Order by descending</summary>
    public static readonly string OrderByDescending = "DESC";

    /// <summary>Filter</summary>
    public static readonly string FilterOperation = "filter";

    /// <summary>Select</summary>
    public static readonly string SelectOperation = "select";

    /// <summary>Top</summary>
    public static readonly string TopOperation = "top";

    /// <summary>Skip</summary>
    public static readonly string SkipOperation = "skip";

    /// <summary>Equals filter</summary>
    public static readonly string EqualsFilter = "eq";

    /// <summary>Not equals filter</summary>
    public static readonly string NotEqualsFilter = "ne";

    /// <summary>Greater filter</summary>
    public static readonly string GreaterFilter = "gt";

    /// <summary>Greater equals filter</summary>
    public static readonly string GreaterEqualsFilter = "ge";

    /// <summary>Less filter</summary>
    public static readonly string LessFilter = "lt";

    /// <summary>Less equals filter</summary>
    public static readonly string LessEqualsFilter = "le";

    /// <summary>Result operation</summary>
    public static readonly string ResultOperation = "result";

    /// <summary>Contains function</summary>
    public static readonly string ContainsFunction = "contains";

    /// <summary>Ends with function</summary>
    public static readonly string EndsWithFunction = "endswith";

    /// <summary>Starts with function</summary>
    public static readonly string StartsWithFunction = "startswith";

    /// <summary>Year function</summary>
    public static readonly string YearFunction = "year";

    /// <summary>Month function</summary>
    public static readonly string MonthFunction = "month";

    /// <summary>Day function</summary>
    public static readonly string DayFunction = "day";

    /// <summary>Hour function</summary>
    public static readonly string HourFunction = "hour";

    /// <summary>Minute function</summary>
    public static readonly string MinuteFunction = "minute";

    /// <summary>Date function</summary>
    public static readonly string DateFunction = "date";

    /// <summary>Time function</summary>
    public static readonly string TimeFunction = "time";
}