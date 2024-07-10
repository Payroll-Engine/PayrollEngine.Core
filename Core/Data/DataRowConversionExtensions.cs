using System.Collections.Generic;
using System.Text.Json;
using System;

namespace PayrollEngine.Data
{
    // duplicated in PayrollEngine.Client.Scripting.System.Data.DataRowExtensions
    /// <summary>Data row conversion extension methods</summary>
    public static class DataRowConversionExtensions
    {
        /// <summary>Transpose enumerable collection to table columns with values</summary>
        /// <remarks>Use the function return value null, to suppress further item operations</remarks>
        /// <param name="target">The target data row</param>
        /// <param name="items">The items to transpose</param>
        /// <param name="columnName">The column name function (mandatory)</param>
        /// <param name="itemValue">The item value function (mandatory)</param>
        /// <param name="columnType">The column type function, default is string</param>
        /// <param name="defaultValue">The default value function, default is none</param>
        public static void TransposeFrom<T>(this System.Data.DataRow target, IEnumerable<T> items,
            Func<T, string> columnName, Func<T, object> itemValue,
            Func<T, Type> columnType = null, Func<T, object> defaultValue = null)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(columnName);
            ArgumentNullException.ThrowIfNull(itemValue);

            foreach (var item in items)
            {
                // column name
                var name = columnName(item);
                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                //  column
                System.Data.DataColumn column;
                if (!target.Table.Columns.Contains(name))
                {
                    // new column
                    Type type = columnType?.Invoke(item) ?? typeof(T);
                    column = new(name, type);

                    // default value
                    if (defaultValue != null)
                    {
                        column.DefaultValue = defaultValue.Invoke(item);
                    }
                    target.Table.Columns.Add(column);
                }
                else
                {
                    column = target.Table.Columns[name];
                }
                if (column == null)
                {
                    continue;
                }

                // item/row value
                var value = itemValue(item);
                if (value == null)
                {
                    continue;
                }
                if (value is JsonElement jsonElement)
                {
                    value = jsonElement.GetValue();
                }
                target[name] = value;
            }
        }

        /// <summary>Transpose dictionary to table columns with values</summary>
        /// <remarks>Use the function return value null, to suppress further item operations
        /// Transpose dynamic object: row.TransposeFrom((IDictionary&lt;string, object&gt;)dynamicObject);</remarks>
        /// <param name="target">The target data row</param>
        /// <param name="items">The items to transpose</param>
        /// <param name="columnName">The column name function (mandatory)</param>
        /// <param name="itemValue">The item value function (mandatory)</param>
        /// <param name="columnType">The column type function, default is string</param>
        /// <param name="defaultValue">The default value function, default is none</param>
        public static void TransposeFrom<TKey, TValue>(this System.Data.DataRow target, IDictionary<TKey, TValue> items,
            Func<TKey, string> columnName = null, Func<TValue, object> itemValue = null,
            Func<TKey, Type> columnType = null, Func<TKey, object> defaultValue = null)
        {
            ArgumentNullException.ThrowIfNull(items);

            foreach (var item in items)
            {
                // column name
                var name = item.Key.ToString();
                if (columnName != null)
                {
                    name = columnName(item.Key);
                }
                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                //  column
                System.Data.DataColumn column;
                if (!target.Table.Columns.Contains(name))
                {
                    // new column
                    Type type = columnType?.Invoke(item.Key) ?? typeof(TValue);
                    column = new(name, type);

                    // default value
                    if (defaultValue != null)
                    {
                        column.DefaultValue = defaultValue.Invoke(item.Key);
                    }
                    target.Table.Columns.Add(column);
                }
                else
                {
                    column = target.Table.Columns[name];
                }
                if (column == null)
                {
                    continue;
                }

                // item/row value
                var value = item.Value as object;
                if (itemValue != null)
                {
                    value = itemValue(item.Value);
                }
                if (value == null)
                {
                    continue;
                }
                if (value is JsonElement jsonElement)
                {
                    value = jsonElement.GetValue();
                }
                target[name] = value;
            }
        }

        /// <summary>Transpose data row items to table columns with values</summary>
        /// <remarks>Use the function return value null, to suppress further item operations</remarks>
        /// <param name="target">The target data row</param>
        /// <param name="source">The source data row</param>
        /// <param name="columnName">The column name function, default is the source column name</param>
        /// <param name="itemValue">The item value function, default is the source column value</param>
        /// <param name="columnType">The column type function, default is the source column type</param>
        /// <param name="defaultValue">The default value function, default is none</param>
        public static void TransposeFrom(this System.Data.DataRow target, System.Data.DataRow source,
            Func<System.Data.DataColumn, string> columnName = null, Func<object, object> itemValue = null,
            Func<System.Data.DataColumn, Type> columnType = null, Func<System.Data.DataColumn, object> defaultValue = null)
        {
            ArgumentNullException.ThrowIfNull(source);

            foreach (System.Data.DataColumn sourceColumn in source.Table.Columns)
            {
                // column name
                var name = sourceColumn.ColumnName;
                if (columnName != null)
                {
                    name = columnName(sourceColumn);
                }
                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                //  new column
                System.Data.DataColumn targetColumn;
                if (!target.Table.Columns.Contains(name))
                {
                    // column type
                    Type type = sourceColumn.DataType;
                    if (columnType != null)
                    {
                        type = columnType.Invoke(sourceColumn);
                    }
                    if (columnType == null)
                    {
                        continue;
                    }

                    // create column
                    targetColumn = new(name, type);

                    // default value
                    if (defaultValue != null)
                    {
                        targetColumn.DefaultValue = defaultValue.Invoke(sourceColumn);
                    }
                    target.Table.Columns.Add(targetColumn);
                }
                else
                {
                    targetColumn = target.Table.Columns[name];
                }
                if (targetColumn == null)
                {
                    continue;
                }
                if (targetColumn.DataType != sourceColumn.DataType)
                {
                    throw new PayrollException($"Mismatching types in column {sourceColumn.ColumnName}:" +
                                               $" source: {sourceColumn.DataType.Name}, target: {targetColumn.DataType.Name}");
                }

                // item/row value
                var value = source[sourceColumn];
                if (itemValue != null)
                {
                    value = itemValue(value);
                }
                if (value == null)
                {
                    continue;
                }
                if (value is JsonElement jsonElement)
                {
                    value = jsonElement.GetValue();
                }
                target[name] = value;
            }
        }
    }
}
