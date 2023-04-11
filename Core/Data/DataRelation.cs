using System;

namespace PayrollEngine.Data;

/// <summary>
/// Represents a data relation in a DataTable
/// </summary>
public class DataRelation : IEquatable<DataRelation>
{
    /// <summary>
    /// Gets or sets the relation name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the relation parent table name
    /// </summary>
    public string ParentTable { get; set; }

    /// <summary>
    /// Gets or sets the relation parent column names
    /// </summary>
    public string ParentColumn { get; set; } = "Id";

    /// <summary>
    /// Gets or sets the relation child table name
    /// </summary>
    public string ChildTable { get; set; }

    /// <summary>
    /// Gets or sets the relation child column names
    /// </summary>
    public string ChildColumn { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataRelation"/> class
    /// </summary>
    public DataRelation()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataRelation"/> class from  a copy source
    /// </summary>
    /// <param name="copySource">The copy source</param>
    public DataRelation(DataRelation copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public bool Equals(DataRelation compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="string" /> that represents this instance.</returns>
    public override string ToString() =>
        $"{Name}: {ChildTable}.{ChildColumn} > {ParentTable}.{ParentColumn}";
}