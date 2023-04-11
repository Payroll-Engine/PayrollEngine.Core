using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The document type</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentType
{
    /// <summary>Word document</summary>
    Word = 0,

    /// <summary>Excel document</summary>
    Excel = 1,

    /// <summary>Pdf document</summary>
    Pdf = 2,

    /// <summary>XML document</summary>
    Xml = 3,

    /// <summary>Raw XML document</summary>
    XmlRaw = 4
}