using System;
using System.Collections.Generic;
using System.Data;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.DataTableExtensions
/// <summary>Data table extension methods</summary>
public static class DataTableExtensions
{

    #region Primary key

    /// <summary>Set the table primary key column</summary>
    /// <param name="table">The table</param>
    /// <param name="columnName">Name of the column</param>
    public static void SetPrimaryKey(this System.Data.DataTable table, string columnName)
    {
        if (table != null)
        {
            var column = table.Columns[columnName];
            table.PrimaryKey = new[] { column };
        }
    }

    /// <summary>Remove the table primary key</summary>
    /// <param name="table">The table</param>
    public static void RemovePrimaryKey(this System.Data.DataTable table)
    {
        if (table != null)
        {
            table.PrimaryKey = Array.Empty<System.Data.DataColumn>();
        }
    }

    #endregion

    #region Value

    /// <summary>Get data table rows value</summary>
    /// <param name="dataTable">The data table</param>
    /// <param name="column">The column name</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The data table rows value</returns>
    public static List<T> GetValues<T>(this System.Data.DataTable dataTable, string column, T defaultValue = default) =>
        dataTable.Select().GetValues(column, defaultValue);

    /// <summary>Get data table rows JSON value as dictionary</summary>
    /// <param name="dataTable">The data table</param>
    /// <param name="column">The column name</param>
    /// <param name="keyField">The json object key field</param>
    /// <param name="valueField">The json object value field</param>
    /// <returns>The data table rows value</returns>
    public static Dictionary<string, string> GetDictionary(this System.Data.DataTable dataTable,
        string column, string keyField, string valueField)
    {
        var values = new Dictionary<string, string>();
        foreach (var row in dataTable.AsEnumerable())
        {
            var objectValues = row.GetDictionary<string, string>(column);
            if (objectValues != null && objectValues.ContainsKey(keyField) &&
                objectValues.TryGetValue(valueField, out var value))
            {
                values.Add(objectValues[keyField], value);
            }
        }
        return values;
    }

    #endregion

}