using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The payroll languages</summary>
/// <remarks>Do not change the language order</remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Language
{
    // ReSharper disable IdentifierTypo
    // ReSharper disable CommentTypo

    /// <summary>English (Default)</summary>
    English,

    /// <summary>Afrikaans</summary>
    Afrikaans,
    /// <summary>Arabic</summary>
    Arabic,
    /// <summary>Azerbaijani</summary>
    Azerbaijani,
    /// <summary>Belarusian</summary>
    Belarusian,
    /// <summary>Bulgarian</summary>
    Bulgarian,
    /// <summary>Bosnian</summary>
    Bosnian,
    /// <summary>Czech</summary>
    Czech,
    /// <summary>Danish</summary>
    Danish,
    /// <summary>German</summary>
    German,
    /// <summary>Greek</summary>
    Greek,
    /// <summary>Spanish</summary>
    Spanish,
    /// <summary>Estonian</summary>
    Estonian,
    /// <summary>Persian</summary>
    Persian,
    /// <summary>Finnish</summary>
    Finnish,
    /// <summary>French</summary>
    French,
    /// <summary>Irish</summary>
    Irish,
    /// <summary>Hebrew</summary>
    Hebrew,
    /// <summary>Hindi</summary>
    Hindi,
    /// <summary>Croatian</summary>
    Croatian,
    /// <summary>Hungarian</summary>
    Hungarian,
    /// <summary>Armenian</summary>
    Armenian,
    /// <summary>Icelandic</summary>
    Icelandic,
    /// <summary>Italian</summary>
    Italian,
    /// <summary>Japanese</summary>
    Japanese,
    /// <summary>Georgian</summary>
    Georgian,
    /// <summary>Korean</summary>
    Korean,
    /// <summary>Luxembourgish</summary>
    Luxembourgish,
    /// <summary>Lithuanian</summary>
    Lithuanian,
    /// <summary>Latvian</summary>
    Latvian,
    /// <summary>Macedonian</summary>
    Macedonian,
    /// <summary>Dutch</summary>
    Dutch,
    /// <summary>Norwegian</summary>
    Norwegian,
    /// <summary>Polish</summary>
    Polish,
    /// <summary>Portuguese</summary>
    Portuguese,
    /// <summary>Romanian</summary>
    Romanian,
    /// <summary>Russian</summary>
    Russian,
    /// <summary>Slovak</summary>
    Slovak,
    /// <summary>Slovenian</summary>
    Slovenian,
    /// <summary>Albanian</summary>
    Albanian,
    /// <summary>Serbian</summary>
    Serbian,
    /// <summary>Swedish</summary>
    Swedish,
    /// <summary>Thai</summary>
    Thai,
    /// <summary>Turkish</summary>
    Turkish,
    /// <summary>Ukrainian</summary>
    Ukrainian,
    /// <summary>Uzbek</summary>
    Uzbek,
    /// <summary>Vietnamese</summary>
    Vietnamese,
    /// <summary>Chinese</summary>
    Chinese

    // ReSharper restore CommentTypo
    // ReSharper restore IdentifierTypo
}