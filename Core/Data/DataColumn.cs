using System;

namespace PayrollEngine.Data;

/// <summary>
/// Represents a column in a DataTable
/// </summary>
public class DataColumn : IEquatable<DataColumn>
{
    /// <summary>
    /// Gets or sets the column name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Column expression used to filter rows
    /// </summary>
    public string Expression { get; set; }

    /// <summary>
    /// Gets or sets the type of data stored in the column
    /// </summary>
    public string ValueType { get; set; }

    /// <summary>
    /// Gets or sets the base type of data stored in the column
    /// </summary>
    public string ValueBaseType { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataColumn"/> class
    /// </summary>
    public DataColumn()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataColumn"/> class with data
    /// </summary>
    /// <param name="name">The data column name</param>
    /// <param name="type">The system type</param>
    public DataColumn(string name, Type type)
    {
        Name = name;
        SetValueType(type);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataColumn"/> class with data
    /// </summary>
    /// <param name="name">The data column name</param>
    /// <param name="type">The system type</param>
    /// <param name="baseType">The system base type</param>
    public DataColumn(string name, Type type, Type baseType)
    {
        Name = name;
        SetValueType(type);
        SetValueBaseType(baseType);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataColumn"/> class with data
    /// </summary>
    /// <param name="name">The data column name</param>
    /// <param name="valueType">The value type</param>
    /// <param name="valueBaseType">The base value type</param>
    public DataColumn(string name, string valueType, string valueBaseType)
    {
        Name = name;
        ValueType = valueType;
        ValueBaseType = valueBaseType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataColumn"/> class from  a copy source
    /// </summary>
    /// <param name="copySource">The copy source</param>
    public DataColumn(DataColumn copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>
    /// Gets the system value type
    /// </summary>
    public Type GetValueType() =>
        string.IsNullOrWhiteSpace(ValueType) ? null : Type.GetType(ValueType);

    /// <summary>
    /// Gets the system value type
    /// </summary>
    public void SetValueType(Type type) =>
        ValueType = type?.FullName;

    /// <summary>
    /// Gets the system value base type
    /// </summary>
    public Type GetValueBaseType() =>
        string.IsNullOrWhiteSpace(ValueBaseType) ? null : Type.GetType(ValueBaseType);

    /// <summary>
    /// Gets the system value base type
    /// </summary>
    public void SetValueBaseType(Type type) =>
        ValueBaseType = type?.FullName;

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public bool Equals(DataColumn compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="string" /> that represents this instance.</returns>
    public override string ToString() => $"{Name} ({ValueType})";
}