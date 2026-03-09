using System;
using System.Data;
using System.Collections.Generic;

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

    /// <param name="table">The table</param>
    extension(System.Data.DataTable table)
    {
        /// <summary>Add table column</summary>
        /// <param name="columnName">Name of the column</param>
        /// <param name="expression">The compute expression</param>
        public System.Data.DataColumn AddColumn<T>(string columnName, string expression = null) => table.AddColumn(columnName, typeof(T), expression);

        /// <summary>Add table column</summary>
        /// <param name="columnName">Name of the column</param>
        /// <param name="type">The column type</param>
        /// <param name="expression">The compute expression</param>
        public System.Data.DataColumn AddColumn(string columnName, Type type, string expression = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(columnName);

            if (expression == null)
            {
                return table.Columns.Add(columnName, type);
            }
            return table.Columns.Add(columnName, type, expression);
        }

        /// <summary>Insert table column at certain list position</summary>
        /// <param name="index">The column list position</param>
        /// <param name="columnName">Name of the column</param>
        /// <param name="expression">The compute expression</param>
        public System.Data.DataColumn InsertColumn<T>(int index, string columnName, string expression = null)
        {
            if (index > table.Columns.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var column = table.AddColumn<T>(columnName, expression);
            // change column position
            column.SetOrdinal(index);
            return column;
        }

        /// <summary>Ensure table column</summary>
        /// <param name="columnName">Name of the column</param>
        /// <param name="expression">The compute expression</param>
        public System.Data.DataColumn EnsureColumn<T>(string columnName, string expression = null) => 
            table.EnsureColumn(columnName, typeof(T), expression);

        /// <summary>Ensure table column</summary>
        /// <param name="columnName">Name of the column</param>
        /// <param name="type">The column type</param>
        /// <param name="expression">The compute expression</param>
        public System.Data.DataColumn EnsureColumn(string columnName, Type type, string expression = null)
        {
            if (!ContainsColumn(table, columnName))
            {
                return table.AddColumn(columnName, type, expression);
            }
            return table.Columns[columnName];
        }

        /// <summary>Ensure table columns</summary>
        /// <param name="columns">The columns to ensure</param>
        public void EnsureColumns<T>(IEnumerable<string> columns)
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
        /// <param name="columns">The columns to ensure</param>
        public void EnsureColumns(DataColumnCollection columns)
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
        /// <param name="oldColumnName">Existing name of the column</param>
        /// <param name="newColumnName">Existing name of the column</param>
        /// <returns>The column name</returns>
        public string RenameColumn(string oldColumnName, string newColumnName)
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
        /// <param name="columnName">Name of the column</param>
        public void RemoveColumn(string columnName) =>
            table?.Columns.Remove(columnName);
    }
}