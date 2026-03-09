using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.Encodings.Web;
using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="string"/></summary>
public static class StringExtensions
{

    #region Namespace

    /// <summary>
    /// Ensure namespace on text list
    /// </summary>
    /// <param name="texts">Texts</param>
    /// <param name="namespace">Namespace to ensure</param>
    /// <param name="checkExisting">Check for existing namespace (default: true)</param>
    public static IList<string> EnsureNamespace(this IList<string> texts, string @namespace, bool checkExisting = true) => texts.ToList().EnsureNamespace(@namespace, checkExisting);

    /// <summary>
    /// Ensure namespace on text list
    /// </summary>
    /// <param name="texts">Texts</param>
    /// <param name="namespace">Namespace to ensure</param>
    /// <param name="checkExisting">Check for existing namespace (default: true)</param>
    public static List<string> EnsureNamespace(this List<string> texts, string @namespace, bool checkExisting = true)
    {
        if (string.IsNullOrWhiteSpace(@namespace))
        {
            return texts;
        }

        if (!@namespace.EndsWith('.'))
        {
            @namespace += '.';
        }
        if (texts == null || !texts.Any() || texts.All(x => x.HasNamespace(@namespace, checkExisting)))
        {
            return texts;
        }
        return texts.Select(text => text.EnsureNamespace(@namespace, checkExisting)).ToList();
    }

    /// <param name="text">Text</param>
    extension(string text)
    {
        /// <summary>
        /// Check for valid namespace
        /// </summary>
        public bool HasNamespace() =>
            !string.IsNullOrWhiteSpace(text) && text.Contains('.');

        /// <summary>
        /// Check for valid namespace
        /// </summary>
        /// <param name="namespace">Namespace to test</param>
        /// <param name="checkExisting">Check for existing namespace (default: true)</param>
        public bool HasNamespace(string @namespace, bool checkExisting = true)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(@namespace))
            {
                return true;
            }
            if (checkExisting && text.HasNamespace())
            {
                return true;
            }

            if (!@namespace.EndsWith('.'))
            {
                @namespace += '.';
            }
            return text.StartsWith(@namespace);
        }

        /// <summary>
        /// Ensure namespace on text
        /// </summary>
        /// <param name="namespace">Namespace to ensure</param>
        /// <param name="checkExisting">Check for existing namespace (default: false)</param>
        public string EnsureNamespace(string @namespace, bool checkExisting = false)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(@namespace))
            {
                return text;
            }
            if (!@namespace.EndsWith('.'))
            {
                @namespace += '.';
            }
            return text.HasNamespace(@namespace, checkExisting) ? text : text.EnsureStart(@namespace);
        }
    }

    #endregion

    #region Localization

    /// <summary>Gets the localized text</summary>
    /// <param name="culture">The culture name</param>
    /// <param name="localizations">The localizations</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The localized text, the default value in case of absent language</returns>
    public static string GetLocalization(this string culture, Dictionary<string, string> localizations, string defaultValue = null)
    {
        if (localizations == null)
        {
            return defaultValue;
        }

        // culture
        culture ??= Thread.CurrentThread.CurrentUICulture.Name;
        culture = culture.Trim();

        // specific language localization (e.g. en-US)
        var cultureName = localizations.Keys.FirstOrDefault(
            x => string.Equals(x, culture, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(cultureName))
        {
            var localizationValue = localizations[cultureName];
            if (localizationValue == null)
            {
                return defaultValue;
            }
            return localizationValue;
        }

        // neutral language
        var index = culture.IndexOf('-');
        if (index < 0 && culture.Length == 2)
        {
            // search for first country specific language
            var specificCulture = localizations.Keys.FirstOrDefault(
                x => x.StartsWith(culture, StringComparison.OrdinalIgnoreCase));
            if (specificCulture == null)
            {
                return defaultValue;
            }
            var localizationValue = localizations[specificCulture];
            if (localizationValue == null)
            {
                return defaultValue;
            }
            return localizationValue;
        }

        // neutral language localization (e.g. en, de)
        var neutralCulture = culture.Substring(0, index);
        cultureName = localizations.Keys.FirstOrDefault(
            x => string.Equals(x, neutralCulture, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(cultureName))
        {
            var localizationValue = localizations[cultureName];
            if (localizationValue == null)
            {
                return defaultValue;
            }
            return localizationValue;
        }

        return defaultValue;
    }

    #endregion

    #region Modification

    /// <param name="value">The string value</param>
    extension(string value)
    {
        /// <summary>Ensures first string character is lower</summary>
        /// <returns>String starting lowercase</returns>
        public string FirstCharacterToLower() =>
            char.ToLowerInvariant(value[0]) + value.Substring(1);

        /// <summary>Ensures first string character is upper</summary>
        /// <returns>String starting uppercase</returns>
        public string FirstCharacterToUpper() =>
            value[0].ToString().ToUpper() + value.Substring(1);

        /// <summary>Ensures a start prefix</summary>
        /// <param name="prefix">The prefix to add</param>
        /// <returns>The string with prefix</returns>
        public string EnsureStart(string prefix)
        {
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = prefix;
                }
                else if (!value.StartsWith(prefix))
                {
                    value = $"{prefix}{value}";
                }
            }
            return value;
        }

        /// <summary>Ensures a start prefix</summary>
        /// <param name="prefix">The prefix to add</param>
        /// <param name="comparison">The comparison culture</param>
        /// <returns>The string with prefix</returns>
        public string EnsureStart(string prefix, StringComparison comparison)
        {
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = prefix;
                }
                else if (!value.StartsWith(prefix, comparison))
                {
                    value = $"{prefix}{value}";
                }
            }
            return value;
        }

        /// <summary>Ensures an ending suffix</summary>
        /// <param name="suffix">The suffix to add</param>
        /// <returns>The string with suffix</returns>
        public string EnsureEnd(string suffix)
        {
            if (!string.IsNullOrWhiteSpace(suffix))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = suffix;
                }
                else if (!value.EndsWith(suffix))
                {
                    value = $"{value}{suffix}";
                }
            }
            return value;
        }

        /// <summary>Ensures an ending suffix</summary>
        /// <param name="suffix">The suffix to add</param>
        /// <param name="comparison">The comparison culture</param>
        /// <returns>The string with suffix</returns>
        public string EnsureEnd(string suffix, StringComparison comparison)
        {
            if (!string.IsNullOrWhiteSpace(suffix))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = suffix;
                }
                else if (!value.EndsWith(suffix, comparison))
                {
                    value = $"{value}{suffix}";
                }
            }
            return value;
        }

        /// <summary>Remove prefix from string</summary>
        /// <param name="prefix">The prefix to remove</param>
        /// <returns>The string without suffix</returns>
        public string RemoveFromStart(string prefix)
        {
            if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(prefix) &&
                value.StartsWith(prefix))
            {
                value = value.Substring(prefix.Length);
            }
            return value;
        }

        /// <summary>Remove prefix from string</summary>
        /// <param name="prefix">The prefix to remove</param>
        /// <param name="comparison">The comparison culture</param>
        /// <returns>The string without the starting prefix</returns>
        public string RemoveFromStart(string prefix, StringComparison comparison)
        {
            if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(prefix) &&
                value.StartsWith(prefix, comparison))
            {
                value = value.Substring(prefix.Length);
            }
            return value;
        }

        /// <summary>Remove suffix from string</summary>
        /// <param name="suffix">The suffix to remove</param>
        /// <returns>The string without the ending suffix</returns>
        public string RemoveFromEnd(string suffix)
        {
            if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(suffix) &&
                value.EndsWith(suffix))
            {
                value = value.Substring(0, value.Length - suffix.Length);
            }
            return value;
        }

        /// <summary>Remove suffix from string</summary>
        /// <param name="suffix">The suffix to remove</param>
        /// <param name="comparison">The comparison culture</param>
        /// <returns>The string without the ending suffix</returns>
        public string RemoveFromEnd(string suffix, StringComparison comparison)
        {
            if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(suffix) &&
                value.EndsWith(suffix, comparison))
            {
                value = value.Substring(0, value.Length - suffix.Length);
            }
            return value;
        }

        /// <summary>Remove all special characters</summary>
        /// <returns>The source value without special characters</returns>
        public string RemoveSpecialCharacters()
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            var builder = new StringBuilder();
            foreach (var c in value)
            {
                if (c is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z')
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

        /// <summary>Change to camel case sentence</summary>
        /// <param name="wordCase">The word start character casing</param>
        /// <param name="separator">The word separator</param>
        /// <returns>Camel case text sentence</returns>
        public string ToCamelSentence(CharacterCase wordCase = CharacterCase.ToLower, char separator = ' ') =>
            value.ToSentence(CharacterCase.ToLower, wordCase, separator);

        /// <summary>Change to pascal case sentence</summary>
        /// <param name="wordCase">The word start character casing</param>
        /// <param name="separator">The word separator</param>
        /// <returns>Camel case text sentence</returns>
        public string ToPascalSentence(CharacterCase wordCase = CharacterCase.ToLower, char separator = ' ') =>
            value.ToSentence(CharacterCase.ToUpper, wordCase, separator);

        /// <summary>Change text sentence</summary>
        /// <remarks>source: https://stackoverflow.com/a/51310790/15659039 </remarks>
        /// <param name="startCase">The first character casing</param>
        /// <param name="wordCase">The word start character casing</param>
        /// <param name="separator">The word separator</param>
        /// <returns></returns>
        public string ToSentence(CharacterCase startCase = CharacterCase.Keep,
            CharacterCase wordCase = CharacterCase.Keep,
            char separator = ' ')
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            var buffer = new StringBuilder();
            // start with the first character -- consistent camelcase and pascal case
            buffer.Append(ChangeCase(value[0], startCase));

            // march through the rest of it
            var newWord = false;
            for (var i = 1; i < value.Length; i++)
            {
                var c = value[i];
                // new word
                if (newWord)
                {
                    buffer.Append(ChangeCase(c, wordCase));
                }
                // any time we hit an uppercase OR number, it's a new word
                else if (char.IsUpper(c) || char.IsDigit(c))
                {
                    buffer.Append(separator);
                    buffer.Append(ChangeCase(c, wordCase));
                }
                // word separator
                else if (c == separator)
                {
                    newWord = true;
                    buffer.Append(c);
                    continue;
                }
                else
                {
                    // add regularly
                    buffer.Append(c);
                }
                newWord = false;
            }

            return buffer.ToString();
        }
    }

    private static char ChangeCase(char input, CharacterCase startCase)
    {
        switch (startCase)
        {
            case CharacterCase.ToUpper:
                return char.ToUpper(input);
            case CharacterCase.ToLower:
                return char.ToLower(input);
            default:
                return input;
        }
    }

    /// <param name="source">The source value</param>
    extension(string source)
    {
        /// <summary>Truncate a string, preserving the sentence words</summary>
        /// <param name="length">The string length</param>
        /// <returns>Truncated case text sentence</returns>
        public string TruncateSentence(int length) => 
            source.TruncateSentence(length, "...");

        /// <summary>Truncate a string, preserving the sentence words</summary>
        /// <param name="length">The string length</param>
        /// <param name="ellipsis">Replacement for cut text</param>
        /// <remarks>source https://stackoverflow.com/a/53843505/15659039</remarks>
        /// <returns>Truncated case text sentence</returns>
        public string TruncateSentence(int length, string ellipsis)
        {
            if (string.IsNullOrWhiteSpace(source) || source.Length < length)
            {
                return source;
            }

            var truncate = source;
            if (source.Length > length)
            {
                var truncateRaw = source.Substring(0, length);
                // preserve words
                var lastWordIndex = truncateRaw.LastIndexOf(' ');
                if (lastWordIndex > 0)
                {
                    truncate = truncateRaw.Substring(0, lastWordIndex) + ellipsis;
                }
            }
            return truncate;
        }
    }

    #endregion

    #region Html

    /// <param name="value">The string value</param>
    extension(string value)
    {
        /// <summary>Encode string to html, single quotation marks and double quotation marks are included as ' and "</summary>
        /// <returns>Encoded Html value</returns>
        public string HtmlEncode() =>
            JavaScriptEncoder.Default.Encode(value);

        /// <summary>Encode string to html, single quotation marks and double quotation marks are included as ' and "</summary>
        /// <returns>Encoded Html value</returns>
        public string HtmlDecode() =>
            System.Net.WebUtility.HtmlDecode(value);
    }

    #endregion

}