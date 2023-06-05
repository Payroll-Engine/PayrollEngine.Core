using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    /// <param name="properties">The properties to convert int columns (default: all)</param>
    /// <remarks>Property expressions:
    /// simple property: {PropertyName}
    /// child property: {ChildName1}.{ChildNameN}.{PropertyName}
    /// dictionary property: {ChildName}.{PropertyName}.{DictionaryKey}</remarks>
    /// <returns>Data table with items data</returns>
    public static void AppendItems(this System.Data.DataTable dataTable, IEnumerable items,
        IList<string> properties = null)
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
            AppendItem(dataTable, item, properties);
        }
    }

    /// <summary>
    /// Append items to a system data table
    /// </summary>
    /// <param name="dataTable">The target table</param>
    /// <param name="item">The items to append</param>
    /// <param name="properties">The properties to convert int columns (default: all)</param>
    /// <remarks>Property expressions:
    /// simple property: {PropertyName}
    /// child property: {ChildName1}.{ChildNameN}.{PropertyName}
    /// dictionary property: {ChildName}.{PropertyName}.{DictionaryKey}</remarks>
    /// <returns>Data table with items data</returns>
    public static System.Data.DataRow AppendItem(this System.Data.DataTable dataTable, object item,
        IList<string> properties = null)
    {
        if (dataTable == null)
        {
            throw new ArgumentNullException(nameof(dataTable));
        }
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        // properties
        var itemProperties = ObjectInfo.GetProperties(item.GetType());
        var propertyNames = properties ?? itemProperties.Select(x => x.Name).ToList();
        var propertyValues = new List<PropertyValue>();
        foreach (var propertyName in propertyNames)
        {
            var propertyValue = item.ResolvePropertyValue(propertyName);
            if (propertyValue == null)
            {
                continue;
            }
            propertyValues.Add(propertyValue);
        }

        // row
        System.Data.DataRow dataRow = dataTable.NewRow();
        if (!propertyValues.Any())
        {
            return dataRow;
        }

        // collect row data
        var rowValues = new object[propertyValues.Count];
        for (var i = 0; i < propertyValues.Count; i++)
        {
            var property = propertyValues[i].Property;
            var value = propertyValues[i].Value;

            // json value
            if (value != null && property.PropertyType.IsSerializedType())
            {
                value = JsonSerializer.Serialize(value);
            }
            rowValues[i] = value;
        }

        // values row
        dataRow.ItemArray = rowValues;
        dataTable.Rows.Add(dataRow);

        return dataRow;
    }
}