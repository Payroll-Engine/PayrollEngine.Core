using System;
using System.Collections;
using System.Text.Json;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.DataTableExtensions
/// <summary>Data table item extension methods</summary>
public static class DataTableItemExtensions
{

    /// <summary>
    /// Append items to a system data table
    /// </summary>
    /// <param name="dataTable">The target table</param>
    /// <param name="items">The items to convert</param>
    /// <returns>Data table with items data</returns>
    public static void AppendItems(this System.Data.DataTable dataTable, IEnumerable items)
    {
        if (dataTable == null)
        {
            throw new ArgumentNullException(nameof(dataTable));
        }
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        foreach (var item in items)
        {
            AppendItem(dataTable, item);
        }
    }

    /// <summary>
    /// Append items to a system data table
    /// </summary>
    /// <param name="dataTable">The target table</param>
    /// <param name="item">The items to append</param>
    /// <returns>Data table with items data</returns>
    public static System.Data.DataRow AppendItem(this System.Data.DataTable dataTable, object item)
    {
        if (dataTable == null)
        {
            throw new ArgumentNullException(nameof(dataTable));
        }
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        // data set
        var properties = ObjectInfo.GetProperties(item.GetType());

        // rows
        var rowValues = new object[properties.Count];
        for (var i = 0; i < properties.Count; i++)
        {
            var value = properties[i].GetValue(item, null);
            // json value
            if (value != null && properties[i].PropertyType.IsSerializedType())
            {
                value = JsonSerializer.Serialize(value);
            }
            rowValues[i] = value;
        }

        // values row
        System.Data.DataRow dataRow = dataTable.NewRow();
        dataRow.ItemArray = rowValues;
        dataTable.Rows.Add(dataRow);

        return dataRow;
    }

}