using System.Collections.Generic;
using System.Data;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.DataTableExtensions
/// <summary>Data table row extension methods</summary>
public static class DataTableRowExtensions
{
    /// <summary>Get table rows</summary>
    /// <param name="table">The table</param>
    /// <returns>A row collection</returns>
    public static EnumerableRowCollection Rows(this System.Data.DataTable table) =>
        table.AsEnumerable();

    /// <summary>Test for any table rows</summary>
    /// <param name="table">The table</param>
    /// <returns>True if rows are present</returns>
    public static bool HasRows(this System.Data.DataTable table) =>
        table != null && table.Rows.Count > 0;

    /// <summary>Test for single row table</summary>
    /// <param name="table">The table</param>
    /// <returns>True for a single row collection</returns>
    public static bool IsSingleRow(this System.Data.DataTable table) =>
        table != null && table.Rows.Count == 1;

    /// <summary>Get single row table</summary>
    /// <param name="table">The table</param>
    /// <returns>The single row</returns>
    public static System.Data.DataRow SingleRow(this System.Data.DataTable table)
    {
        if (!IsSingleRow(table))
        {
            throw new PayrollException($"Table {table.TableName} is not single, count={table.Rows.Count}");
        }
        return table.Rows[0];
    }

    /// <summary>Get as single row table</summary>
    /// <param name="table">The table</param>
    /// <returns>The single row, null on table with multiple rows</returns>
    public static System.Data.DataRow AsSingleRow(this System.Data.DataTable table) =>
        IsSingleRow(table) ? SingleRow(table) : null;

    /// <summary>Get single row id</summary>
    /// <param name="table">The table</param>
    /// <returns>The data row id</returns>
    public static int GetSingleRowId(this System.Data.DataTable table) =>
        IsSingleRow(table) ? SingleRow(table).GetRowId() : 0;

    /// <summary>Get single row name</summary>
    /// <param name="table">The table</param>
    /// <returns>The data row name</returns>
    public static string GetSingleRowName(this System.Data.DataTable table) =>
        IsSingleRow(table) ? SingleRow(table).GetRowName() : null;

    /// <summary>Get single row identifier</summary>
    /// <param name="table">The table</param>
    /// <returns>The data row identifier</returns>
    public static string GetSingleRowIdentifier(this System.Data.DataTable table) =>
        IsSingleRow(table) ? SingleRow(table).GetRowIdentifier() : null;

    /// <summary>Get single row table value</summary>
    /// <param name="table">The table</param>
    /// <param name="column">The column name</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The data row value</returns>
    public static T GetSingleRowValue<T>(this System.Data.DataTable table, string column, T defaultValue = default) =>
        IsSingleRow(table) ? SingleRow(table).GetValue(column, defaultValue) : defaultValue;

    /// <summary>Select table rows by filter</summary>
    /// <param name="table">The table</param>
    /// <param name="filterExpression">The filter matching the rows to delete</param>
    public static IEnumerable<System.Data.DataRow> SelectRows(this System.Data.DataTable table, string filterExpression) =>
        table.Select(filterExpression);

    /// <summary>Delete table rows by filter</summary>
    /// <param name="table">The table</param>
    /// <param name="filterExpression">The filter matching the rows to delete</param>
    public static int DeleteRows(this System.Data.DataTable table, string filterExpression)
    {
        var deleteCount = 0;
        var deleteRows = SelectRows(table, filterExpression);
        foreach (var deleteRow in deleteRows)
        {
            deleteRow.Delete();
            deleteCount++;
        }
        if (deleteCount > 0)
        {
            table.AcceptChanges();
        }
        return deleteCount;
    }
}