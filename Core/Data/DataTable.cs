using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PayrollEngine.Data;

/// <summary>
/// Represents one table of in-memory data
/// </summary>
public class DataTable : IEquatable<DataTable>
{
    /// <summary>
    /// Gets or sets the table name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the collection of columns that belong to this table
    /// </summary>
    public List<DataColumn> Columns { get; set; }

    /// <summary>
    /// Gets the collection of rows that belong to this table
    /// </summary>
    public List<DataRow> Rows { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataTable"/> class
    /// </summary>
    public DataTable()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataTable"/> class with items
    /// </summary>
    public DataTable(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataTable"/> class from  a copy source
    /// </summary>
    /// <param name="copySource">The copy source</param>
    public DataTable(DataTable copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>
    /// Gets the data row values
    /// </summary>
    /// <param name="row">The data row</param>
    /// <returns>The row raw data</returns>
    public IEnumerable<object> GetRawValues(DataRow row)
    {
        ArgumentNullException.ThrowIfNull(row);
        if (!Rows.Contains(row))
        {
            throw new ArgumentException("Invalid table row");
        }
        if (Columns == null || row.Values == null)
        {
            throw new DataException("Invalid data row size");
        }
        if (Columns.Count != row.Values.Count)
        {
            throw new DataException("Invalid data row value count");
        }

        // convert raw values to json values
        var rawValues = new List<object>();
        if (row.Values != null)
        {
            for (var i = 0; i < Columns.Count; i++)
            {
                var column = Columns[i];
                if (!string.IsNullOrWhiteSpace(column.Expression))
                {
                    continue;
                }

                var rawValue = row.Values[i];
                if (rawValue == null)
                {
                    rawValues.Add(default);
                    continue;
                }

                var type = Columns[i].GetValueType();

                object value = null;
                // string/datetime escaping
                if (type != null && (type == typeof(string) || type == typeof(DateTime)))
                {
                    value = Regex.Unescape(rawValue.Trim('"'));
                }
                else if (!string.IsNullOrWhiteSpace(rawValue))
                {
                    value = type == null ?
                        // serialize unknown types to json string
                        JsonSerializer.Serialize(rawValue) :
                        JsonSerializer.Deserialize(rawValue, type);
                }

                // enum (string to int)
                var baseType = column.GetValueBaseType();
                if (baseType != null && baseType.IsEnum)
                {
                    value = (int)Enum.Parse(baseType, rawValue.Trim('"'));
                }

                rawValues.Add(value);
            }
        }
        return rawValues;
    }

    /// <summary>Add a row with raw data</summary>
    /// <param name="rawValues">The values to set</param>
    public void AddRow(IEnumerable<object> rawValues)
    {
        ArgumentNullException.ThrowIfNull(rawValues);
        var row = new DataRow();
        Rows.Add(row);
        SetRawValues(row, rawValues);
    }

    /// <summary>Set row data</summary>
    /// <param name="row">The data row</param>
    /// <param name="rawValues">The raw values to set</param>
    public void SetRawValues(DataRow row, IEnumerable<object> rawValues)
    {
        ArgumentNullException.ThrowIfNull(row);
        ArgumentNullException.ThrowIfNull(rawValues);
        if (!Rows.Contains(row))
        {
            throw new ArgumentException("Invalid table row");
        }
        if (Columns == null || rawValues == null)
        {
            throw new DataException("Invalid data row size");
        }
        var rawValueList = rawValues.ToList();
        if (Columns.Count != rawValueList.Count)
        {
            throw new DataException("Invalid data row value count");
        }

        // convert json values to raw values
        row.Values ??= [];
        for (var i = 0; i < Columns.Count; i++)
        {
            var column = Columns[i];
            var rawValue = rawValueList[i];

            // null
            if (rawValue == null || rawValue is DBNull)
            {
                row.Values.Add(default);
                continue;
            }

            // enum (int to string)
            var baseType = column.GetValueBaseType();
            if (baseType != null && baseType.IsEnum)
            {
                rawValue = Enum.GetName(baseType, rawValue);
            }

            var type = column.GetValueType();
            var jsonValue = type == null ?
                // unknown types as json string
                JsonSerializer.Serialize(rawValue) :
                JsonSerializer.Serialize(rawValue, type);

            // string/datetime escaping
            if (!string.IsNullOrWhiteSpace(jsonValue) && type != null &&
                (type == typeof(string) || type == typeof(DateTime)))
            {
                jsonValue = Regex.Unescape(jsonValue.Trim('"'));
            }

            row.Values.Add(jsonValue);
        }
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public bool Equals(DataTable compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="string" /> that represents this instance.</returns>
    public override string ToString() => Name;
}