using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>Extensions for <see cref="Language"/></summary>
public static class LanguageExtensions
{
    /// <summary>The ISO 639-1 language code</summary>
    private static readonly string[] LanguageCodes = {
        // ReSharper disable CommentTypo
        "en", // English (Default)

        "af", // Afrikaans
        "ar", // Arabic
        "az", // Azerbaijani
        "be", // Belarusian
        "bg", // Bulgarian
        "bs", // Bosnian
        "cs", // Czech
        "da", // Danish
        "de", // German
        "el", // Greek
        "es", // Spanish
        "et", // Estonian
        "fa", // Persian
        "fi", // Finnish
        "fr", // French
        "ga", // Irish
        "he", // Hebrew
        "hi", // Hindi
        "hr", // Croatian
        "hu", // Hungarian
        "hy", // Armenian
        "is", // Icelandic
        "it", // Italian
        "ja", // Japanese
        "ka", // Georgian
        "ko", // Korean
        "lb", // Luxembourgish
        "lt", // Lithuanian
        "lv", // Latvian
        "mk", // Macedonian
        "nl", // Dutch
        "no", // Norwegian
        "pl", // Polish
        "pt", // Portuguese
        "ro", // Romanian
        "ru", // Russian
        "sk", // Slovak
        "sl", // Slovenian
        "sq", // Albanian
        "sr", // Serbian
        "sw", // Swedish
        "th", // Thai
        "tr", // Turkish
        "uk", // Ukrainian
        "uz", // Uzbek
        "vi", // Vietnamese
        "zh" // Chinese
        // ReSharper restore CommentTypo
    };

    /// <summary>Gets the localized text</summary>
    /// <param name="language">The language</param>
    /// <param name="localizations">The localizations</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The localized text, the default value in case of absent language</returns>
    public static string GetLocalization(this Language language, Dictionary<string, string> localizations, string defaultValue)
    {
        if (localizations != null && localizations.TryGetValue(language.LanguageCode(), out var localization))
        {
            return localization;
        }
        return defaultValue;
    }

    /// <summary>Get the ISO 639-1 language code</summary>
    /// <param name="language">The value type</param>
    /// <returns>The ISO 639-1 language code</returns>
    public static string LanguageCode(this Language language) =>
        LanguageCodes[(int)language];
}