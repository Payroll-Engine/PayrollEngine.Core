using System;
using System.Collections.Generic;

namespace PayrollEngine.Data;

/// <summary>
/// Represents a row of data in a table
/// </summary>
public class DataRow : IEquatable<DataRow>
{
    /// <summary>
    /// Gets or sets the row values (JSON, type matching to the column)
    /// </summary>
    /// <value>The items.</value>
    public List<string> Values { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataRow"/> class
    /// </summary>
    public DataRow()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataRow"/> class from  a copy source
    /// </summary>
    /// <param name="copySource">The copy source</param>
    public DataRow(DataRow copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public bool Equals(DataRow compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override bool Equals(object obj) =>
        obj is DataRow other && Equals(other);

    /// <inheritdoc/>
    // ReSharper disable NonReadonlyMemberInGetHashCode
    public override int GetHashCode()
    {
        if (Values == null || Values.Count == 0)
        {
            return 0;
        }
        var hash = new HashCode();
        foreach (var value in Values)
        {
            hash.Add(value);
        }
        return hash.ToHashCode();
    }
    // ReSharper restore NonReadonlyMemberInGetHashCode

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="string" /> that represents this instance.</returns>
    public override string ToString() =>
        Values != null ? string.Join(",", Values) : base.ToString();
}