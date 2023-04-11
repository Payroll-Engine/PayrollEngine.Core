using System;
using System.Collections.Generic;

namespace PayrollEngine.Data;

/// <summary>
/// Represents an in-memory cache of data in tables
/// </summary>
public class DataSet : IEquatable<DataSet>
{
    /// <summary>
    /// Gets or sets the data set name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the collection of tables contained in the data set
    /// </summary>
    public List<DataTable> Tables { get; set; }

    /// <summary>
    /// Gets the collection of table relations contained in the data set
    /// </summary>
    public List<DataRelation> Relations { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataSet"/> class
    /// </summary>
    public DataSet()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataSet"/> class
    /// </summary>
    /// <param name="name">The data set name</param>
    public DataSet(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataSet"/> class from  a copy source
    /// </summary>
    /// <param name="copySource">The copy source</param>
    public DataSet(DataSet copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public bool Equals(DataSet compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="string" /> that represents this instance.</returns>
    public override string ToString() => Name;
}