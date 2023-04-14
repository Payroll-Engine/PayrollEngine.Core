using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.DataTableExtensions
/// <summary>Data table attribute extension methods</summary>
public static class DataTableAttributeExtensions
{
    /// <summary>
    /// Get attribute data table
    /// </summary>
    /// <param name="dataTable">The source table</param>
    /// <param name="attributesTableName">The attributes data table name</param>
    /// <param name="relationSourceColumn">The relation source column name</param>
    /// <param name="relationTargetColumn">The relation target column name</param>
    /// <returns>Data table with attributes, any attribute is a table column</returns>
    public static System.Data.DataTable GetAttributeTable(this System.Data.DataTable dataTable, string attributesTableName,
        string relationSourceColumn, string relationTargetColumn)
    {
        if (dataTable == null)
        {
            throw new ArgumentNullException(nameof(dataTable));
        }
        if (string.IsNullOrWhiteSpace(attributesTableName))
        {
            throw new ArgumentException(nameof(attributesTableName));
        }
        if (relationSourceColumn == null)
        {
            throw new ArgumentNullException(nameof(relationSourceColumn));
        }
        if (relationTargetColumn == null)
        {
            throw new ArgumentNullException(nameof(relationTargetColumn));
        }

        // column
        System.Data.DataTable attributesTable = new(attributesTableName);
        var attributeColumn = dataTable.Columns[nameof(IAttributeObject.Attributes)];
        if (attributeColumn == null)
        {
            return attributesTable;
        }

        // relation column
        attributesTable.Columns.Add(relationSourceColumn, typeof(int));
        foreach (var dataRow in dataTable.AsEnumerable())
        {
            var attributes = dataRow[attributeColumn];
            // json dictionary
            if (attributes is string stringValue && !string.IsNullOrWhiteSpace(stringValue))
            {
                attributes = JsonSerializer.Deserialize<Dictionary<string, object>>(stringValue);
            }
            if (attributes is Dictionary<string, object> attributeDictionary && attributeDictionary.Any())
            {
                // attribute column
                AddAttributeColumn(attributeDictionary, attributesTable);
                // attribute row
                AddAttributeRow(attributeDictionary, attributesTable, dataRow, relationSourceColumn, relationTargetColumn);
            }
        }

        return attributesTable;
    }

    private static void AddAttributeColumn(Dictionary<string, object> attributes, System.Data.DataTable attributesTable)
    {
        foreach (var attribute in attributes)
        {
            // existing
            if (attributesTable.Columns.Contains(attribute.Key))
            {
                continue;
            }

            var value = attribute.Value;
            // ignore attributes without value
            if (value == null)
            {
                continue;
            }

            // value
            var valueType = value.GetType();
            if (value is JsonElement jsonElement)
            {
                valueType = jsonElement.ValueKind.GetSystemType(value);
            }

            // add column
            var attributeValueColumn = attributesTable.Columns[attribute.Key];
            if (attributeValueColumn == null)
            {
                attributesTable.Columns.Add(attribute.Key, valueType);
            }
            else if (attributeValueColumn.DataType != valueType)
            {
                // check for compatible column type
                throw new PayrollException(
                    $"Mismatching attribute column type {attributeValueColumn.DataType} conflicts with {valueType}");
            }
        }
    }

    private static void AddAttributeRow(Dictionary<string, object> attributes, System.Data.DataTable attributesTable,
        System.Data.DataRow dataRow, string relationSourceColumn, string relationTargetColumn)
    {
        // new row
        System.Data.DataRow attributeRow = attributesTable.NewRow();

        // relation
        var relationTarget = dataRow[relationTargetColumn];
        if (relationTarget.GetType() != typeof(int))
        {
            throw new PayrollException($"Invalid attribute relation target value {relationTarget}");
        }

        attributeRow[relationSourceColumn] = relationTarget;

        // attribute row item values
        foreach (var attribute in attributes)
        {
            // ignore attributes without value
            var value = attribute.Value;
            if (value != null)
            {
                // value
                if (value is JsonElement jsonElement)
                {
                    value = jsonElement.GetValue();
                }

                // set attribute values
                if (value != null)
                {
                    attributeRow[attribute.Key] = value;
                }
            }
        }

        // add attributes row
        if (attributeRow.ItemArray.Any())
        {
            attributesTable.Rows.Add(attributeRow);
        }
    }

}