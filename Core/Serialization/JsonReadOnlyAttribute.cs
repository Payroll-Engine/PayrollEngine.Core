using System;

namespace PayrollEngine.Serialization;

/// <summary>
/// JSON attribute for read-only attributes
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class JsonReadOnlyAttribute : Attribute;
