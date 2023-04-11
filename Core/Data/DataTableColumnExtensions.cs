using System;
using System.Collections.Generic;
using System.Data;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.DataTableExtensions
/// <summary>Data table column extension methods</summary>
public static class DataTableColumnExtensions
{

    /// <summary>Test for table column</summary>
    /// <param name="table">The table</param>
    /// <param name="columnName">Name of the column</param>
    public static bool ContainsColumn(System.Data.DataTable table, string columnName) =>
        table.Columns.Contains(columnName);

    /// <summary>Add table column</summary>
    /// <param name="table">The table</param>
    /// <param name="columnName">Name of the column</param>
    /// <param name="expression">The compute expression</param>
    public static System.Data.DataColumn AddColumn<T>(this System.Data.DataTable table, string columnName, string expression = null) =>
        AddColumn(table, columnName, typeof(T), expression);

    /// <summary>Add table column</summary>
    /// <param name="table">The table</param>
    /// <param name="columnName">Name of the column</param>
    /// <param name="type">The column type</param>
    /// <param name="expression">The compute expression</param>
    public static System.Data.DataColumn AddColumn(this System.Data.DataTable table, string columnName, Type type, string expression = null)
    {
        if (string.IsNullOrWhiteSpace(columnName))
        {
            throw new ArgumentException(nameof(columnName));
        }

        if (expression == null)
        {
            return table.Columns.Add(columnName, type);
        }
        return table.Columns.Add(columnName, type, expression);
    }

    /// <summary>Insert table column at certain list position</summary>
    /// <param name="table">The table</param>
    /// <param name="index">The column list position</param>
    /// <param name="columnName">Name of the column</param>
    /// <param name="expression">The compute expression</param>
    public static System.Data.DataColumn InsertColumn<T>(this System.Data.DataTable table, int index, string columnName, string expression = null)
    {
        if (index > table.Columns.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        var column = AddColumn<T>(table, columnName, expression);
        // change column position
        column.SetOrdinal(index);
        return column;
    }

    /// <summary>Ensure table column</summary>
    /// <param name="table">The table</param>
    /// <param name="columnName">Name of the column</param>
    /// <param name="expression">The compute expression</param>
    public static System.Data.DataColumn EnsureColumn<T>(this System.Data.DataTable table, string columnName, string expression = null) =>
        EnsureColumn(table, columnName, typeof(T), expression);

    /// <summary>Ensure table column</summary>
    /// <param name="table">The table</param>
    /// <param name="columnName">Name of the column</param>
    /// <param name="type">The column type</param>
    /// <param name="expression">The compute expression</param>
    public static System.Data.DataColumn EnsureColumn(this System.Data.DataTable table, string columnName, Type type, string expression = null)
    {
        if (!ContainsColumn(table, columnName))
        {
            return AddColumn(table, columnName, type, expression);
        }
        return table.Columns[columnName];
    }

    /// <summary>Ensure table columns</summary>
    /// <param name="table">The table</param>
    /// <param name="columns">The columns to ensure</param>
    public static void EnsureColumns<T>(this System.Data.DataTable table, IEnumerable<string> columns)
    {
        foreach (var column in columns)
        {
            if (!table.Columns.Contains(column))
            {
                table.Columns.Add(column, typeof(T));
            }
        }
    }

    /// <summary>Ensure table column</summary>
    /// <param name="table">The table</param>
    /// <param name="columns">The columns to ensure</param>
    public static void EnsureColumns(this System.Data.DataTable table, DataColumnCollection columns)
    {
        foreach (System.Data.DataColumn column in columns)
        {
            if (!table.Columns.Contains(column.ColumnName))
            {
                table.Columns.Add(column.ColumnName, column.DataType);
            }
        }
    }

    /// <summary>Rename table column</summary>
    /// <param name="table">The table</param>
    /// <param name="oldColumnName">Existing name of the column</param>
    /// <param name="newColumnName">Existing name of the column</param>
    /// <returns>The column name</returns>
    public static string RenameColumn(this System.Data.DataTable table, string oldColumnName, string newColumnName)
    {
        if (table == null)
        {
            return null;
        }
        var column = table.Columns[oldColumnName];
        if (column == null)
        {
            return null;
        }
        column.ColumnName = newColumnName;
        return newColumnName;
    }

    /// <summary>Remove table column</summary>
    /// <param name="table">The table</param>
    /// <param name="columnName">Name of the column</param>
    public static void RemoveColumn(this System.Data.DataTable table, string columnName) =>
        table?.Columns.Remove(columnName);

}