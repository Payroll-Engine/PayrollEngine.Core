using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>Object containing attributes</summary>
public interface IAttributeObject
{
    /// <summary>Custom object attributes</summary>
    Dictionary<string, object> Attributes { get; set; }
}