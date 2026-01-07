using System.Data;
using System.Collections.Generic;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.DataTableExtensions
/// <summary>Data table row extension methods</summary>
public static class DataTableRowExtensions
{
    /// <param name="table">The table</param>
    extension(System.Data.DataTable table)
    {
        /// <summary>Get table rows</summary>
        /// <returns>A row collection</returns>
        public EnumerableRowCollection Rows() =>
            table.AsEnumerable();

        /// <summary>Test for any table rows</summary>
        /// <returns>True if rows are present</returns>
        public bool HasRows() =>
            table != null && table.Rows.Count > 0;

        /// <summary>Test for single row table</summary>
        /// <returns>True for a single row collection</returns>
        public bool IsSingleRow() =>
            table != null && table.Rows.Count == 1;

        /// <summary>Get single row table</summary>
        /// <returns>The single row</returns>
        public System.Data.DataRow SingleRow()
        {
            if (!table.IsSingleRow())
            {
                throw new PayrollException($"Table {table.TableName} is not single, count={table.Rows.Count}.");
            }
            return table.Rows[0];
        }

        /// <summary>Get as single row table</summary>
        /// <returns>The single row, null on table with multiple rows</returns>
        public System.Data.DataRow AsSingleRow() =>
            table.IsSingleRow() ? table.SingleRow() : null;

        /// <summary>Get single row id</summary>
        /// <returns>The data row id</returns>
        public int SingleRowId() =>
            table.IsSingleRow() ? table.SingleRow().Id() : 0;

        /// <summary>Get single row name</summary>
        /// <returns>The data row name</returns>
        public string SingleRowName() =>
            table.IsSingleRow() ? table.SingleRow().Name() : null;

        /// <summary>Get single row identifier</summary>
        /// <returns>The data row identifier</returns>
        public string SingleRowIdentifier() =>
            table.IsSingleRow() ? table.SingleRow().Identifier() : null;

        /// <summary>Get single row table value</summary>
        /// <param name="column">The column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data row value</returns>
        public T SingleRowValue<T>(string column, T defaultValue = default) =>
            table.IsSingleRow() ? table.SingleRow().GetValue(column, defaultValue) : defaultValue;

        /// <summary>Select table rows by filter</summary>
        /// <param name="filterExpression">The filter matching the rows to delete</param>
        public IEnumerable<System.Data.DataRow> SelectRows(string filterExpression) =>
            table.Select(filterExpression);

        /// <summary>Delete table rows by filter</summary>
        /// <param name="filterExpression">The filter matching the rows to delete</param>
        public int DeleteRows(string filterExpression)
        {
            var deleteCount = 0;
            var deleteRows = table.SelectRows(filterExpression);
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
}