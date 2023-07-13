using System.Collections.Generic;
using System.Data;
using System.IO;

namespace PayrollEngine.Document;

/// <summary>Data merge</summary>
public interface IDataMerge
{
    /// <summary>Test if document type is mergeable</summary>
    /// <param name="documentType">Type of the document</param>
    bool IsMergeable(DocumentType documentType);

    /// <summary>Merge a document from a stream to a stream</summary>
    /// <param name="templateStream">Name of the template file</param>
    /// <param name="dataSet">The data set</param>
    /// <param name="documentType">Type of the document</param>
    /// <param name="metadata">The document metadata</param>
    /// <param name="parameters">The merge parameters</param>
    /// <returns>The merged document stream</returns>
    MemoryStream Merge(Stream templateStream, DataSet dataSet, DocumentType documentType,
        DocumentMetadata metadata, IDictionary<string, object> parameters = null);

    /// <summary>Merge to excel stream</summary>
    /// <param name="dataSet">The source data</param>
    /// <param name="metadata">The document meta data</param>
    /// <param name="parameters">The merge parameters</param>
    MemoryStream ExcelMerge(DataSet dataSet, DocumentMetadata metadata,
        IDictionary<string, object> parameters = null);
}