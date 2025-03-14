using System.Data;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
            table.PrimaryKey = [column];
        }
    }

    /// <summary>Remove the table primary key</summary>
    /// <param name="table">The table</param>
    public static void RemovePrimaryKey(this System.Data.DataTable table)
    {
        if (table != null)
        {
            table.PrimaryKey = [];
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

    /// <summary>Get data table as dictionary</summary>
    /// <param name="dataTable">The data table</param>
    /// <returns>List of row dictionaries</returns>
    public static  List<Dictionary<string, object>> AsDictionary(this System.Data.DataTable dataTable)
    {
        var values = new List<Dictionary<string, object>>();
        foreach (System.Data.DataRow row in dataTable.AsEnumerable())
        {
            values.Add(row.AsDictionary());
        }
        return values;
    }

    /// <summary>Get data table as json</summary>
    /// <param name="dataTable">The data table</param>
    /// <param name="namingPolicy">Naming policy (default: camel case)</param>
    /// <param name="ignoreNull">Ignore null values (default: true)</param>
    public static string Json(this System.Data.DataTable dataTable, JsonNamingPolicy namingPolicy = null,
        bool ignoreNull = true)
    {
        return JsonSerializer.Serialize(AsDictionary(dataTable), new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = namingPolicy ?? JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = namingPolicy ?? JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = ignoreNull ? JsonIgnoreCondition.WhenWritingNull : default
        });
    }

    #endregion

}