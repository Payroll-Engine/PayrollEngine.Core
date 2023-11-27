using System;

namespace PayrollEngine;

/// <summary>The script specification</summary>
public static class ScriptingSpecification
{
    /// <summary>The current scripting version</summary>
    public static readonly Version ScriptingVersion = new(1, 0, 0);

    /// <summary>The c# language version, string represents the Microsoft.CodeAnalysis.CSharp.LanguageVersion enum</summary>
    public static readonly string CSharpLanguageVersion = "CSharp12";

    /// <summary>Tags start marker</summary>
    public static readonly string TagsStartMarker = "/*";

    /// <summary>Tags end marker</summary>
    public static readonly string TagsEndMarker = "*/";

    /// <summary>Tags separator</summary>
    public static readonly char TagsSeparator = ',';

    /// <summary>Sealed (non-derivable) tag</summary>
    public static readonly string SealedTag = "#sealed";

    /// <summary>Scripting region for the function</summary>
    public static readonly string FunctionRegion = "Function";

    /// <summary>Test if the tag is known</summary>
    /// <param name="tag">The tag to test</param>
    /// <returns>True id the tag is known</returns>
    public static bool IsTag(string tag) =>
        !string.IsNullOrWhiteSpace(tag) && string.Equals(SealedTag, tag, StringComparison.InvariantCulture);
}