using System.Collections.Generic;
using System.Text.Json;
using System;

namespace PayrollEngine.Data
{
    // duplicated in PayrollEngine.Client.Scripting.System.Data.DataRowExtensions
    /// <summary>Data row localization extension methods</summary>
    public static class DataRowLocalizationExtensions
    {
        /// <summary>Get data row localized name</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="language">The language</param>
        /// <returns>The localized data row name</returns>
        public static string GetLocalizedName(this System.Data.DataRow dataRow, Language language) =>
            GetLocalizedValue(dataRow, "Name", language);

        /// <summary>Get data row localized identifier</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="language">The language</param>
        /// <returns>The localized data row identifier</returns>
        public static string GetLocalizedIdentifier(this System.Data.DataRow dataRow, Language language) =>
            GetLocalizedValue(dataRow, "Identifier", language);

        /// <summary>Get data row localized value using the ValueColumnLocalizations column</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="valueColumn">The value column name</param>
        /// <param name="language">The language</param>
        /// <returns>The localized data row value</returns>
        public static string GetLocalizedValue(this System.Data.DataRow dataRow, string valueColumn, Language language) =>
            GetLocalizedValue(dataRow, valueColumn, $"{valueColumn}Localizations", language);

        /// <summary>Get data row localized value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="valueColumn">The value column name</param>
        /// <param name="localizationColumn">The localization column name</param>
        /// <param name="language">The language</param>
        /// <returns>The localized data row value</returns>
        public static string GetLocalizedValue(this System.Data.DataRow dataRow, string valueColumn, string localizationColumn,
                Language language)
        {
            if (dataRow == null)
            {
                throw new ArgumentNullException(nameof(dataRow));
            }

            var value = dataRow.GetValue<string>(valueColumn);

            var languageName = Enum.GetName(language) ?? language.ToString();
            var valueLocalization = JsonSerializer.Deserialize<
                Dictionary<string, string>>(dataRow.GetValue<string>(localizationColumn));
            if (valueLocalization != null && valueLocalization.TryGetValue(languageName, out var localValue))
            {
                value = localValue;
            }

            return value;
        }

        /// <summary>Get data row json value as localizations dictionary</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <returns>The attributes dictionary</returns>
        public static Dictionary<string, string> GetLocalizations(this System.Data.DataRow dataRow, string column) =>
            dataRow.GetDictionary<string, string>(column);

        /// <summary>Get attribute from a data row JSON value</summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="column">The column name</param>
        /// <param name="language">The language</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The attribute value</returns>
        public static string GetLocalization(this System.Data.DataRow dataRow, string column, Language language, string defaultValue = default) =>
            language.GetLocalization(GetLocalizations(dataRow, column), defaultValue);

    }
}
