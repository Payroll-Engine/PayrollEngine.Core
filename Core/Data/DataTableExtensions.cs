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

    /// <param name="table">The table</param>
    extension(System.Data.DataTable table)
    {
        /// <summary>Set the table primary key column</summary>
        /// <param name="columnName">Name of the column</param>
        public void SetPrimaryKey(string columnName)
        {
            if (table != null)
            {
                var column = table.Columns[columnName];
                table.PrimaryKey = [column];
            }
        }

        /// <summary>Remove the table primary key</summary>
        public void RemovePrimaryKey()
        {
            if (table != null)
            {
                table.PrimaryKey = [];
            }
        }
    }

    #endregion

    #region Value

    /// <param name="dataTable">The data table</param>
    extension(System.Data.DataTable dataTable)
    {
        /// <summary>Get data table rows value</summary>
        /// <param name="column">The column name</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The data table rows value</returns>
        public List<T> GetValues<T>(string column, T defaultValue = default) =>
            dataTable.Select().GetValues(column, defaultValue);

        /// <summary>Get data table as dictionary</summary>
        /// <returns>List of row dictionaries</returns>
        public List<Dictionary<string, object>> AsDictionary()
        {
            var values = new List<Dictionary<string, object>>();
            foreach (System.Data.DataRow row in dataTable.AsEnumerable())
            {
                values.Add(row.AsDictionary());
            }
            return values;
        }

        /// <summary>Get data table as json</summary>
        /// <param name="namingPolicy">Naming policy (default: camel case)</param>
        /// <param name="ignoreNull">Ignore null values (default: true)</param>
        public string Json(JsonNamingPolicy namingPolicy = null,
            bool ignoreNull = true)
        {
            return JsonSerializer.Serialize(dataTable.AsDictionary(), new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = namingPolicy ?? JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = namingPolicy ?? JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = ignoreNull ? JsonIgnoreCondition.WhenWritingNull : default
            });
        }
    }

    #endregion

}