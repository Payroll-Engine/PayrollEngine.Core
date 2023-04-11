
namespace PayrollEngine;

/// <summary>
/// Query result
/// </summary>
public class QueryResult<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryResult{T}"/> class
    /// </summary>
    public QueryResult()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryResult{T}"/> class
    /// </summary>
    /// <param name="items">The query result items</param>
    /// <param name="count">The query result count</param>
    public QueryResult(T[] items, long count)
    {
        Items = items;
        Count = count;
    }

    /// <summary>
    /// The query result count
    /// </summary>
    public long Count { get; set; }

    /// <summary>
    /// The query result items
    /// </summary>
    public T[] Items { get; set; }

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString()
    {
        if (Count <= 0)
        {
            return "No query results";
        }
        return Count == 1 ?
            $"{Count} {typeof(T).Name}" :
            $"{Count} {typeof(T).Name}s";
    }
}