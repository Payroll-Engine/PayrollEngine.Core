using System;
using System.Linq;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.DataTableExtensions
/// <summary>Data table item extension methods</summary>
public static class DataTableItemExtensions
{
    /// <param name="dataTable">The target table</param>
    extension(System.Data.DataTable dataTable)
    {
        /// <summary>
        /// Append items to a system data table
        /// </summary>
        /// <param name="items">The items to convert</param>
        /// <param name="properties">The properties to convert int columns (default: all)</param>
        /// <remarks>Property expressions:
        /// simple property: {PropertyName}
        /// child property: {ChildName1}.{ChildNameN}.{PropertyName}
        /// dictionary property: {ChildName}.{PropertyName}.{DictionaryKey}</remarks>
        /// <returns>Data table with items data</returns>
        public void AppendItems(IEnumerable items,
            IList<string> properties = null)
        {
            ArgumentNullException.ThrowIfNull(dataTable);
            ArgumentNullException.ThrowIfNull(items);

            foreach (var item in items)
            {
                dataTable.AppendItem(item, properties);
            }
        }

        /// <summary>
        /// Append items to a system data table
        /// </summary>
        /// <param name="item">The items to append</param>
        /// <param name="properties">The properties to convert int columns (default: all)</param>
        /// <remarks>Property expressions:
        /// simple property: {PropertyName}
        /// child property: {ChildName1}.{ChildNameN}.{PropertyName}
        /// dictionary property: {ChildName}.{PropertyName}.{DictionaryKey}</remarks>
        /// <returns>Data table with items data</returns>
        public System.Data.DataRow AppendItem(object item,
            IList<string> properties = null)
        {
            ArgumentNullException.ThrowIfNull(dataTable);
            ArgumentNullException.ThrowIfNull(item);

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

            // collect row item array
            var rowItems = new object[dataTable.Columns.Count];
            foreach (var propertyValue in propertyValues)
            {
                var index = dataTable.Columns.IndexOf(propertyValue.Property.Name);
                // ignore unknown column properties
                if (index < 0)
                {
                    continue;
                }

                // value
                var value = propertyValue.Value;
                if (value != null && propertyValue.Property.PropertyType.IsSerializedType())
                {
                    value = JsonSerializer.Serialize(value);
                }
                rowItems[index] = value;
            }

            // values row
            dataRow.ItemArray = rowItems;
            dataTable.Rows.Add(dataRow);

            return dataRow;
        }
    }
}