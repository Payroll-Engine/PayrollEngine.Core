using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="string"/></summary>
public static class StringExtensions
{

    #region Modification

    /// <summary>Ensures first string character is lower</summary>
    /// <param name="value">The string value</param>
    /// <returns>String starting lowercase</returns>
    public static string FirstCharacterToLower(this string value) =>
        char.ToLowerInvariant(value[0]) + value.Substring(1);

    /// <summary>Ensures first string character is upper</summary>
    /// <param name="value">The string value</param>
    /// <returns>String starting uppercase</returns>
    public static string FirstCharacterToUpper(this string value) =>
        value.First().ToString().ToUpper() + value.Substring(1);

    /// <summary>Ensures a start prefix</summary>
    /// <param name="source">The source value</param>
    /// <param name="prefix">The prefix to add</param>
    /// <returns>The string with prefix</returns>
    public static string EnsureStart(this string source, string prefix)
    {
        if (!string.IsNullOrWhiteSpace(prefix))
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                source = prefix;
            }
            else if (!source.StartsWith(prefix))
            {
                source = $"{prefix}{source}";
            }
        }
        return source;
    }

    /// <summary>Ensures a start prefix</summary>
    /// <param name="source">The source value</param>
    /// <param name="prefix">The prefix to add</param>
    /// <param name="comparison">The comparison culture</param>
    /// <returns>The string with prefix</returns>
    public static string EnsureStart(this string source, string prefix, StringComparison comparison)
    {
        if (!string.IsNullOrWhiteSpace(prefix))
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                source = prefix;
            }
            else if (!source.StartsWith(prefix, comparison))
            {
                source = $"{prefix}{source}";
            }
        }
        return source;
    }

    /// <summary>Ensures an ending suffix</summary>
    /// <param name="source">The source value</param>
    /// <param name="suffix">The suffix to add</param>
    /// <returns>The string with suffix</returns>
    public static string EnsureEnd(this string source, string suffix)
    {
        if (!string.IsNullOrWhiteSpace(suffix))
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                source = suffix;
            }
            else if (!source.EndsWith(suffix))
            {
                source = $"{source}{suffix}";
            }
        }
        return source;
    }

    /// <summary>Ensures an ending suffix</summary>
    /// <param name="source">The source value</param>
    /// <param name="suffix">The suffix to add</param>
    /// <param name="comparison">The comparison culture</param>
    /// <returns>The string with suffix</returns>
    public static string EnsureEnd(this string source, string suffix, StringComparison comparison)
    {
        if (!string.IsNullOrWhiteSpace(suffix))
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                source = suffix;
            }
            else if (!source.EndsWith(suffix, comparison))
            {
                source = $"{source}{suffix}";
            }
        }
        return source;
    }

    /// <summary>Remove prefix from string</summary>
    /// <param name="source">The source value</param>
    /// <param name="prefix">The prefix to remove</param>
    /// <returns>The string without suffix</returns>
    public static string RemoveFromStart(this string source, string prefix)
    {
        if (!string.IsNullOrWhiteSpace(source) && !string.IsNullOrWhiteSpace(prefix) &&
            source.StartsWith(prefix))
        {
            source = source.Substring(prefix.Length);
        }
        return source;
    }

    /// <summary>Remove prefix from string</summary>
    /// <param name="source">The source value</param>
    /// <param name="prefix">The prefix to remove</param>
    /// <param name="comparison">The comparison culture</param>
    /// <returns>The string without the starting prefix</returns>
    public static string RemoveFromStart(this string source, string prefix, StringComparison comparison)
    {
        if (!string.IsNullOrWhiteSpace(source) && !string.IsNullOrWhiteSpace(prefix) &&
            source.StartsWith(prefix, comparison))
        {
            source = source.Substring(prefix.Length);
        }
        return source;
    }

    /// <summary>Remove suffix from string</summary>
    /// <param name="source">The source value</param>
    /// <param name="suffix">The suffix to remove</param>
    /// <returns>The string without the ending suffix</returns>
    public static string RemoveFromEnd(this string source, string suffix)
    {
        if (!string.IsNullOrWhiteSpace(source) && !string.IsNullOrWhiteSpace(suffix) &&
            source.EndsWith(suffix))
        {
            source = source.Substring(0, source.Length - suffix.Length);
        }
        return source;
    }

    /// <summary>Remove suffix from string</summary>
    /// <param name="source">The source value</param>
    /// <param name="suffix">The suffix to remove</param>
    /// <param name="comparison">The comparison culture</param>
    /// <returns>The string without the ending suffix</returns>
    public static string RemoveFromEnd(this string source, string suffix, StringComparison comparison)
    {
        if (!string.IsNullOrWhiteSpace(source) && !string.IsNullOrWhiteSpace(suffix) &&
            source.EndsWith(suffix, comparison))
        {
            source = source.Substring(0, source.Length - suffix.Length);
        }
        return source;
    }

    /// <summary>Remove all special characters</summary>
    /// <param name="source">The source value</param>
    /// <returns>The source value without special characters</returns>
    public static string RemoveSpecialCharacters(this string source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return source;
        }
        var builder = new StringBuilder();
        foreach (var c in source)
        {
            if (c is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z')
            {
                builder.Append(c);
            }
        }
        return builder.ToString();
    }

    /// <summary>Change to camel case sentence</summary>
    /// <param name="source">The source value</param>
    /// <param name="wordCase">The word start character casing</param>
    /// <param name="separator">The word separator</param>
    /// <returns>Camel case text sentence</returns>
    public static string ToCamelSentence(this string source,
        CharacterCase wordCase = CharacterCase.ToLower, string separator = " ") =>
        ToSentence(source, CharacterCase.ToLower, wordCase, separator);

    /// <summary>Change to pascal case sentence</summary>
    /// <param name="source">The source value</param>
    /// <param name="wordCase">The word start character casing</param>
    /// <param name="separator">The word separator</param>
    /// <returns>Camel case text sentence</returns>
    public static string ToPascalSentence(this string source,
        CharacterCase wordCase = CharacterCase.ToLower, string separator = " ") =>
        ToSentence(source, CharacterCase.ToUpper, wordCase, separator);

    /// <summary>Change text sentence</summary>
    /// <remarks>source: https://stackoverflow.com/a/51310790/15659039 </remarks>
    /// <param name="source">The source value</param>
    /// <param name="startCase">The first character casing</param>
    /// <param name="wordCase">The word start character casing</param>
    /// <param name="separator">The word separator</param>
    /// <returns></returns>
    public static string ToSentence(this string source,
        CharacterCase startCase = CharacterCase.Keep,
        CharacterCase wordCase = CharacterCase.Keep,
        string separator = " ")
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return source;
        }

        var buffer = new StringBuilder();
        // start with the first character -- consistent camelcase and pascal case
        buffer.Append(ChangeCase(source[0], startCase));

        // march through the rest of it
        for (var i = 1; i < source.Length; i++)
        {
            // any time we hit an uppercase OR number, it's a new word
            if (char.IsUpper(source[i]) || char.IsDigit(source[i]))
            {
                if (separator != null)
                {
                    buffer.Append(separator);
                }
                buffer.Append(ChangeCase(source[i], wordCase));
            }
            else
            {
                // add regularly
                buffer.Append(source[i]);
            }
        }

        return buffer.ToString();
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

    /// <summary>Truncate a string, preserving the sentence words</summary>
    /// <param name="source">The source value</param>
    /// <param name="length">The string length</param>
    /// <returns>Truncated case text sentence</returns>
    public static string TruncateSentence(this string source, int length) =>
        TruncateSentence(source, length, "...");

    /// <summary>Truncate a string, preserving the sentence words</summary>
    /// <param name="source">The source value</param>
    /// <param name="length">The string length</param>
    /// <param name="ellipsis">Replacement for cut text</param>
    /// <remarks>source https://stackoverflow.com/a/53843505/15659039</remarks>
    /// <returns>Truncated case text sentence</returns>
    public static string TruncateSentence(this string source, int length, string ellipsis)
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

    #endregion

    #region Html

    /// <summary>Encode string to html, single quotation marks and double quotation marks are included as \' and \"</summary>
    /// <param name="value">The string value</param>
    /// <returns>Encoded Html value</returns>
    public static string HtmlEncode(this string value) =>
        JavaScriptEncoder.Default.Encode(value);

    /// <summary>Encode string to html, single quotation marks and double quotation marks are included as \' and \"</summary>
    /// <param name="value">The string value</param>
    /// <returns>Encoded Html value</returns>
    public static string HtmlDecode(this string value) =>
        System.Net.WebUtility.HtmlDecode(value);

    #endregion

}