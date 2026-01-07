namespace PayrollEngine;

/// <summary>Object with namespace identifiers</summary>
public interface INamespaceObject
{
    /// <summary>
    /// Apply namespace
    /// </summary>
    /// <param name="namespace">Namespace to apply</param>
    void ApplyNamespace(string @namespace);
}