using System;
using System.Data;
using System.Linq;

namespace PayrollEngine.Data;

/// <summary>Data set extension methods</summary>
public static class DataSetExtensions
{
    /// <summary>
    /// Test for payroll data set rows
    /// </summary>
    /// <param name="dataSet">The payroll data set to convert</param>
    /// <returns>True if any row is available</returns>
    public static bool HasRows(this DataSet dataSet)
    {
        if (dataSet?.Tables == null || !dataSet.Tables.Any())
        {
            return false;
        }
        return dataSet.Tables.Any(table => table.Rows.Any());
    }

    /// <summary>
    /// Test for  data set rows
    /// </summary>
    /// <param name="dataSet">The system data set to convert</param>
    /// <returns>True if any row is available</returns>
    public static bool HasRows(this System.Data.DataSet dataSet)
    {
        if (dataSet?.Tables == null || dataSet.Tables.Count == 0)
        {
            return false;
        }
        return dataSet.Tables.Cast<System.Data.DataTable>().Any(table => table.Rows.Count > 0);
    }

    /// <summary>
    /// Convert a payroll data set to a system data set
    /// </summary>
    /// <param name="dataSet">The payroll data set to convert</param>
    /// <returns>The system data set</returns>
    public static System.Data.DataSet ToSystemDataSet(this DataSet dataSet)
    {
        if (dataSet == null)
        {
            return null;
        }

        // data set
        System.Data.DataSet systemDataSet = new(dataSet.Name);

        // tables
        if (dataSet.Tables == null || !dataSet.Tables.Any())
        {
            return systemDataSet;
        }
        foreach (var table in dataSet.Tables)
        {
            // table
            System.Data.DataTable systemTable = table.ToSystemDataTable();
            systemDataSet.Tables.Add(systemTable);
        }

        // relations
        if (dataSet.Relations != null && dataSet.Relations.Any())
        {
            foreach (var relation in dataSet.Relations)
            {
                var parentTable = systemDataSet.Tables[relation.ParentTable];
                if (parentTable == null)
                {
                    throw new PayrollException($"Missing relation parent table {relation.ParentTable}");
                }
                var parentColumn = parentTable.Columns[relation.ParentColumn];
                if (parentColumn == null)
                {
                    throw new PayrollException($"Missing relation parent column {relation.ParentTable}.{relation.ParentColumn}");
                }
                var childTable = systemDataSet.Tables[relation.ChildTable];
                if (childTable == null)
                {
                    throw new PayrollException($"Missing relation child table {relation.ChildTable}");
                }
                var childColumn = childTable.Columns[relation.ChildColumn];
                if (childColumn == null)
                {
                    throw new PayrollException($"Missing relation parent column {relation.ChildTable}.{relation.ChildColumn}");
                }
                systemDataSet.Relations.Add(relation.Name, parentColumn, childColumn);
            }
        }

        return systemDataSet;
    }

    /// <summary>
    /// Convert a system data set to a payroll data set
    /// </summary>
    /// <param name="dataSet">The system data set to convert</param>
    /// <returns>The payroll data set</returns>
    public static DataSet ToPayrollDataSet(this System.Data.DataSet dataSet)
    {
        if (dataSet == null)
        {
            return null;
        }

        // data set
        var payrollDataSet = new DataSet
        {
            Name = dataSet.DataSetName
        };

        // tables
        payrollDataSet.Tables ??= [];
        foreach (System.Data.DataTable table in dataSet.Tables)
        {
            // table
            var payrollTable = new DataTable
            {
                Name = table.TableName
            };
            payrollDataSet.Tables.Add(payrollTable);

            // columns
            payrollTable.Columns ??= [];
            foreach (System.Data.DataColumn column in table.Columns)
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
            foreach (var row in table.AsEnumerable())
            {
                if (row.ItemArray.Length != table.Columns.Count)
                {
                    throw new PayrollException($"Row value count ({row.ItemArray.Length} is not matching the column count ({table.Columns.Count})");
                }
                payrollTable.AddRow(row.ItemArray);
            }
        }

        // relations
        AddPayrollRelations(dataSet, payrollDataSet);

        return payrollDataSet;
    }

    private static void AddPayrollRelations(System.Data.DataSet dataSet, DataSet payrollDataSet)
    {
        if (dataSet.Relations.Count > 0)
        {
            payrollDataSet.Relations ??= [];
            foreach (System.Data.DataRelation relation in dataSet.Relations)
            {
                var payrollRelation = new DataRelation
                {
                    Name = relation.RelationName,
                    ParentTable = relation.ParentTable.TableName,
                    ParentColumn = relation.ParentColumns.First().ColumnName,
                    ChildTable = relation.ChildTable.TableName,
                    ChildColumn = relation.ChildColumns.First().ColumnName
                };
                payrollDataSet.Relations.Add(payrollRelation);
            }
        }
    }
}