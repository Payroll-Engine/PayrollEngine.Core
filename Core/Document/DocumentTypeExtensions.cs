using System;
using PayrollEngine.IO;

namespace PayrollEngine.Document;

/// <summary>Extensions for <see cref="DocumentType"/></summary>
public static class DocumentTypeExtensions
{
    /// <summary>Get the document file extension</summary>
    /// <param name="documentType">The document type</param>
    /// <returns>The file extension</returns>
    public static string GetFileExtension(this DocumentType documentType)
    {
        switch (documentType)
        {
            case DocumentType.Word:
                return FileExtensions.WordDocument;
            case DocumentType.Excel:
                return FileExtensions.ExcelDocument;
            case DocumentType.Pdf:
                return FileExtensions.PdfDocument;
            case DocumentType.Xml:
            case DocumentType.XmlRaw:
                return FileExtensions.Xml;
            default:
                throw new ArgumentOutOfRangeException(nameof(documentType), documentType, null);
        }
    }
}
