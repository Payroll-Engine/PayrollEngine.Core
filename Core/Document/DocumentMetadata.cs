using System;
using System.Collections.Generic;

namespace PayrollEngine.Document;

/// <summary>Document meta data</summary>
public class DocumentMetadata
{
    /// <summary>Application name</summary>
    public string Application { get; } = SystemSpecification.ApplicationName;

    /// <summary>Author name</summary>
    public string Author { get; set; }

    /// <summary>Company name</summary>
    public string Company { get; set; }

    /// <summary>Application title</summary>
    public string Title { get; set; }

    /// <summary>Application category</summary>
    public string Category { get; set; }

    /// <summary>Application keywords</summary>
    public string Keywords { get; set; }

    /// <summary>Application comment</summary>
    public string Comment { get; set; }

    /// <summary>Application created date</summary>
    public DateTime? Created { get; set; }

    /// <summary>Application modified date</summary>
    public DateTime? Modified { get; set; }

    /// <summary>Application custom properties</summary>
    public Dictionary<string, string> CustomProperties { get; set; }
}