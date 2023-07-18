
namespace PayrollEngine;

/// <summary>The system specification</summary>
public static class SystemSpecification
{
    /// <summary>Application name, use to mark the application/producer in office documents</summary>
    public static readonly string ApplicationName = "Payroll Engine API";

    /// <summary>Shared settings environment variable name, containing the json file name</summary>
    public static readonly string PayrollConfigurationVariable = "PayrollConfiguration";

    /// <summary>Setting name, containing the Payroll database connection string</summary>
    public static readonly string DatabaseConnectionSetting = "DatabaseConnection";

    /// <summary>Prefix for text attribute fields</summary>
    public static readonly string TextAttributePrefix = "TA_";

    /// <summary>Prefix for date attribute fields</summary>
    public static readonly string DateAttributePrefix = "DA_";

    /// <summary>Prefix for numeric attribute fields</summary>
    public static readonly string NumericAttributePrefix = "NA_";

    /// <summary>Precision for decimal values, precision of 28 uses 13 bytes of storage.
    /// See https://docs.microsoft.com/en-us/sql/t-sql/data-types/decimal-and-numeric-transact-sql
    /// </summary>
    public static readonly int DecimalPrecision = 28;

    /// <summary>Scale for decimal values</summary>
    public static readonly int DecimalScale = 6;

    /// <summary>Datetime precision</summary>
    public static readonly int DateTimeFractionalSecondsPrecision = 7;

    /// <summary>Payrun executions</summary>
    public static readonly int PayrunMaxExecutionCount = 100;
}