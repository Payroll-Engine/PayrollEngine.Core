using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace PayrollEngine.Document;

/// <summary>Document merge</summary>
public interface IDocumentMerge
{
    /// <summary>Test if document type is mergeable</summary>
    /// <param name="documentType">Type of the document</param>
    bool IsMergeable(DocumentType documentType);

    /// <summary>Merge a document from a stream to a file</summary>
    /// <param name="templateStream">Name of the template file</param>
    /// <param name="dataSet">The data set</param>
    /// <param name="targetFileName">Name of the target file</param>
    /// <param name="documentType">Type of the document</param>
    /// <param name="metadata">The document metadata</param>
    Task MergeToFileAsync(Stream templateStream, DataSet dataSet,
        string targetFileName, DocumentType documentType, DocumentMetadata metadata);
    /*
    /// <summary>Merge a document</summary>
    /// <param name="templateFileName">Name of the template file</param>
    /// <param name="dataSet">The data set</param>
    /// <param name="targetFileName">Name of the target file</param>
    /// <param name="documentType">Type of the document</param>
    /// <param name="metadata">The document metadata</param>
    Task MergeAsync(string templateFileName, DataSet dataSet, string targetFileName,
        DocumentType documentType, DocumentMetadata metadata);
    */
    /// <summary>Merge a document from a stream to a stream</summary>
    /// <param name="templateStream">Name of the template file</param>
    /// <param name="dataSet">The data set</param>
    /// <param name="documentType">Type of the document</param>
    /// <param name="metadata">The document metadata</param>
    /// <returns>The merged document stream</returns>
    MemoryStream MergeToStream(Stream templateStream, DataSet dataSet, DocumentType documentType,
        DocumentMetadata metadata);

    /// <summary>Merge to excel file</summary>
    /// <param name="dataSet">The source data</param>
    /// <param name="targetFileName">The target file name</param>
    /// <param name="metadata">The document meta data</param>
    Task MergeToExcelFileAsync(DataSet dataSet, string targetFileName, DocumentMetadata metadata);

    /// <summary>Merge to excel stream</summary>
    /// <param name="dataSet">The source data</param>
    /// <param name="metadata">The document meta data</param>
    MemoryStream MergeToExcelStream(DataSet dataSet, DocumentMetadata metadata);
}