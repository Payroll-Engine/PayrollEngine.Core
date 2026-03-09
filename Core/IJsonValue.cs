namespace PayrollEngine;

/// <summary>Interface for types that wrap a primitive value for JSON serialization</summary>
public interface IJsonValue
{
    /// <summary>The primitive value for JSON serialization</summary>
    object ToJsonValue();
}
