using System.Collections.Generic;
using System.Text.Json;
using System;

namespace PayrollEngine.Data;

// duplicated in PayrollEngine.Client.Scripting.System.Data.DataRowExtensions
/// <summary>Data row localization extension methods</summary>
public static class DataRowLocalizationExtensions
{
    /// <param name="dataRow">The data row</param>
    extension(System.Data.DataRow dataRow)
    {
        /// <summary>Get data row localized name</summary>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The localized data row name</returns>
        public string GetLocalizedName(string culture) => 
            dataRow.GetLocalizedValue("Name", culture);

        /// <summary>Get data row localized identifier</summary>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The localized data row identifier</returns>
        public string GetLocalizedIdentifier(string culture) => 
            dataRow.GetLocalizedValue("Identifier", culture);

        /// <summary>Get data row localized value using the ValueColumnLocalizations column</summary>
        /// <param name="valueColumn">The value column name</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The localized data row value</returns>
        public string GetLocalizedValue(string valueColumn, string culture) => 
            dataRow.GetLocalizedValue(valueColumn, $"{valueColumn}Localizations", culture);

        /// <summary>Get data row localized value</summary>
        /// <param name="valueColumn">The value column name</param>
        /// <param name="localizationColumn">The localization column name</param>
        /// <param name="culture">The attribute culture</param>
        /// <returns>The localized data row value</returns>
        public string GetLocalizedValue(string valueColumn, string localizationColumn,
            string culture)
        {
            ArgumentNullException.ThrowIfNull(dataRow);

            var value = dataRow.GetValue<string>(valueColumn);

            var valueLocalization = JsonSerializer.Deserialize<
                Dictionary<string, string>>(dataRow.GetValue<string>(localizationColumn));
            if (valueLocalization != null && valueLocalization.TryGetValue(culture, out var localValue))
            {
                value = localValue;
            }

            return value;
        }

        /// <summary>Get data row json value as localizations dictionary</summary>
        /// <param name="column">The column name</param>
        /// <returns>The attributes dictionary</returns>
        public Dictionary<string, string> GetLocalizations(string column) =>
            dataRow.GetDictionary<string, string>(column);

        /// <summary>Get attribute from a data row JSON value</summary>
        /// <param name="column">The column name</param>
        /// <param name="culture">The attribute culture</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The attribute value</returns>
        public string GetLocalization(string column, string culture, string defaultValue = null) =>
            culture.GetLocalization(dataRow.GetLocalizations(column), defaultValue);
    }
}