using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.DataTableExtensions
/// <summary>Data table conversion extension methods</summary>
public static class DataTableConversionExtensions
{

    /// <summary>
    /// Convert an object list to a payroll data table
    /// </summary>
    /// <param name="items">The items to convert</param>
    /// <param name="tableName">The table name, default is the type name</param>
    /// <returns>Data table with items data</returns>
    public static DataTable ToPayrollDataTable(this IEnumerable items, string tableName = null)
    {
        // empty collection
        if (items == null)
        {
            return new(tableName);
        }

        // table
        DataTable dataTable = null;
        IList<PropertyInfo> properties = null;

        foreach (var item in items)
        {
            // table setup
            if (dataTable == null)
            {
                var name = string.IsNullOrWhiteSpace(tableName) ? item.GetType().Name : tableName;
                dataTable = new(name);

                // columns
                dataTable.Columns ??= [];
                properties = ObjectInfo.GetProperties(item.GetType());
                foreach (var property in properties)
                {
                    var column = new DataColumn(property.Name,
                        Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                    dataTable.Columns.Add(column);
                }

                // rows
                dataTable.Rows ??= [];
            }

            var rowValues = new object[properties.Count];
            for (var i = 0; i < properties.Count; i++)
            {
                rowValues[i] = properties[i].GetValue(item, null);
            }
            dataTable.AddRow(rowValues);
        }

        return dataTable ?? new DataTable(tableName);
    }

    /// <summary>
    /// Convert a system data table to a payroll data table
    /// </summary>
    /// <param name="dataTable">The system data table to convert</param>
    /// <returns>The payroll data table</returns>
    public static DataTable ToPayrollDataTable(this System.Data.DataTable dataTable)
    {
        if (dataTable == null)
        {
            return null;
        }

        // table
        var payrollTable = new DataTable
        {
            Name = dataTable.TableName
        };

        // columns
        payrollTable.Columns ??= [];
        foreach (System.Data.DataColumn column in dataTable.Columns)
        {
            Type columnType = column.DataType;

            // enum
            if (columnType.IsEnum)
            {
                columnType = typeof(string);
            }

            var payrollColumn = new DataColumn
            {
                Name = column.ColumnName,
                Expression = string.IsNullOrWhiteSpace(column.Expression) ? null : column.Expression,
                ValueType = columnType.FullName,
                ValueBaseType = column.DataType.FullName
            };
            payrollTable.Columns.Add(payrollColumn);
        }

        // rows
        payrollTable.Rows ??= [];
        foreach (var row in dataTable.AsEnumerable())
        {
            if (row.ItemArray.Length != dataTable.Columns.Count)
            {
                throw new PayrollException($"Row value count ({row.ItemArray.Length} is not matching the column count ({dataTable.Columns.Count})");
            }
            payrollTable.AddRow(row.ItemArray);
        }

        return payrollTable;
    }

    /// <summary>
    /// Convert items to a system data set
    /// </summary>
    /// <param name="items">The items to convert</param>
    /// <param name="tableName">The table name, default is the type name</param>
    /// <param name="includeRows">Include table rows</param>
    /// <param name="primaryKey">The primary key column name</param>
    /// <param name="properties">The properties to convert int columns (default: all)</param>
    /// <remarks>Property expressions:
    /// simple property: {PropertyName}
    /// child property: {ChildName1}.{ChildNameN}.{PropertyName}
    /// dictionary property: {ChildName}.{PropertyName}.{DictionaryKey}</remarks>
    /// <returns>Data table with items data</returns>
    public static System.Data.DataTable ToSystemDataTable(this IEnumerable items, string tableName = null,
        bool includeRows = false, string primaryKey = null, IList<string> properties = null)
    {
        if (items == null)
        {
            return new(tableName);
        }

        // data set
        System.Data.DataTable dataTable = null;
        foreach (var item in items)
        {
            // table
            if (dataTable == null)
            {
                var name = string.IsNullOrWhiteSpace(tableName) ? item.GetType().Name : tableName;
                dataTable = new(name);

                var itemProperties = ObjectInfo.GetProperties(item.GetType());
                var convertPropertyNames = properties ?? itemProperties.Select(x => x.Name).ToList();
                foreach (var convertPropertyName in convertPropertyNames)
                {
                    var resolvedProperty = item.ResolvePropertyValue(convertPropertyName);
                    if (resolvedProperty == null)
                    {
                        continue;
                    }
                    var property = resolvedProperty.Property;

                    // add column
                    var nullableType = Nullable.GetUnderlyingType(property.PropertyType);
                    var columnType = nullableType ?? property.PropertyType;

                    // json column
                    if (columnType.IsSerializedType())
                    {
                        columnType = typeof(string);
                    }

                    var propertyName = resolvedProperty.DictionaryKey ?? property.Name;
                    var dataColumn = new System.Data.DataColumn(propertyName, columnType);
                    if (nullableType != null)
                    {
                        dataColumn.AllowDBNull = true;
                    }
                    dataTable.Columns.Add(dataColumn);

                    // primary key
                    if (primaryKey != null && string.Equals(property.Name, primaryKey))
                    {
                        dataTable.PrimaryKey = [dataColumn];
                    }
                }
            }

            // rows
            if (includeRows)
            {
                dataTable.AppendItem(item, properties);
            }
        }

        return dataTable ?? new System.Data.DataTable(tableName);
    }

    /// <summary>
    /// Convert a payroll data table to a system data table
    /// </summary>
    /// <param name="dataTable">The payroll data table to convert</param>
    /// <returns>The system data table</returns>
    public static System.Data.DataTable ToSystemDataTable(this DataTable dataTable)
    {
        if (dataTable == null)
        {
            return null;
        }

        // table
        System.Data.DataTable systemTable = new(dataTable.Name);

        // columns
        if (!dataTable.Columns.Any())
        {
            return systemTable;
        }
        foreach (var column in dataTable.Columns)
        {
            // treat unknown types as json string
            var columnType = Type.GetType(column.ValueType) ?? typeof(string);
            var systemColumn = new System.Data.DataColumn(column.Name, columnType)
            {
                Expression = column.Expression
            };
            systemTable.Columns.Add(systemColumn);
        }

        // rows
        foreach (var row in dataTable.Rows)
        {
            if (row.Values.Count != dataTable.Columns.Count)
            {
                throw new PayrollException($"Row value count ({row.Values.Count} is not matching the column count ({dataTable.Columns.Count})");
            }
            System.Data.DataRow dataRow = systemTable.NewRow();
            systemTable.Rows.Add(dataRow);

            // values
            dataRow.ItemArray = dataTable.GetRawValues(row).ToArray();
        }

        return systemTable;
    }
}