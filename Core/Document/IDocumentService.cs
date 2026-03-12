using System.IO;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PayrollEngine.Document;

/// <summary>Document service</summary>
public interface IDocumentService
{
    /// <summary>Test if document type is supported</summary>
    /// <param name="documentType">Type of the document</param>
    bool IsSupported(DocumentType documentType);

    /// <summary>Test if document type requires a template for merge (Excel or Pdf)</summary>
    /// <param name="documentType">Type of the document</param>
    bool IsMergeable(DocumentType documentType);

    /// <summary>Merge a template stream with data into a document stream</summary>
    /// <param name="templateStream">The template stream</param>
    /// <param name="dataSet">The data set</param>
    /// <param name="documentType">Type of the document</param>
    /// <param name="metadata">The document metadata</param>
    /// <param name="parameters">The merge parameters</param>
    /// <returns>The merged document stream</returns>
    Task<Stream> MergeAsync(Stream templateStream, DataSet dataSet, DocumentType documentType,
        DocumentMetadata metadata, IDictionary<string, object> parameters = null);

    /// <summary>Merge data into an Excel stream</summary>
    /// <param name="dataSet">The source data</param>
    /// <param name="metadata">The document metadata</param>
    /// <param name="parameters">The merge parameters</param>
    /// <returns>The Excel document stream</returns>
    Task<Stream> ExcelMergeAsync(DataSet dataSet, DocumentMetadata metadata,
        IDictionary<string, object> parameters = null);

    /// <summary>
    /// Generate a schema document from a DataSet.
    /// Without <paramref name="templateStream"/>, generates a new skeleton with an empty ReportPage.
    /// With <paramref name="templateStream"/>, updates the schema section of the existing template
    /// preserving all layout elements.
    /// </summary>
    /// <param name="dataSet">The source data</param>
    /// <param name="templateStream">Existing template stream for schema update (optional, CI mode)</param>
    /// <returns>Stream containing the generated document content</returns>
    Task<Stream> GenerateAsync(DataSet dataSet, Stream templateStream = null);
}
