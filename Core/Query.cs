using System.Text;

namespace PayrollEngine;

/// <summary>
/// Object query parameters
/// </summary>
public class Query
{
    /// <summary>
    /// The object status (default: all status)
    /// </summary>
    public ObjectStatus? Status { get; set; }

    /// <summary>
    /// The OData filter expression (with support for attribute fields)
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// The OData order-by expression (with support for attribute fields)
    /// </summary>
    public string OrderBy { get; set; }

    /// <summary>
    /// The OData field selection expression
    /// </summary>
    public string Select { get; set; }

    /// <summary>
    /// The number of items in the queried collection
    /// </summary>
    public long? Top { get; set; }

    /// <summary>
    /// The number of items in the queried collection that are to be skipped
    /// </summary>
    public long? Skip { get; set; }

    /// <summary>
    /// The query result type: items, count or items with count (default: result items)
    /// </summary>
    public QueryResultType? Result { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Query()
    {
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="source">The copy source</param>
    public Query(Query source)
    {
        CopyTool.CopyProperties(source, this);
    }

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="string" /> that represents this instance.</returns>
    public override string ToString()
    {
        var buffer = new StringBuilder();
        // status
        if (Status.HasValue)
        {
            buffer.Append($"Status={Status}");
        }
        // filter
        if (!string.IsNullOrWhiteSpace(Filter))
        {
            if (buffer.Length > 0)
            {
                buffer.Append(", ");
            }
            buffer.Append($"Filter={Filter}");
        }
        // order by
        if (!string.IsNullOrWhiteSpace(OrderBy))
        {
            if (buffer.Length > 0)
            {
                buffer.Append(", ");
            }
            buffer.Append($"OrderBy={OrderBy}");
        }
        // select
        if (!string.IsNullOrWhiteSpace(Select))
        {
            if (buffer.Length > 0)
            {
                buffer.Append(", ");
            }
            buffer.Append($"Select={Select}");
        }
        // top
        if (Top.HasValue)
        {
            if (buffer.Length > 0)
            {
                buffer.Append(", ");
            }
            buffer.Append($"Top={Top}");
        }
        // skip
        if (Skip.HasValue)
        {
            if (buffer.Length > 0)
            {
                buffer.Append(", ");
            }
            buffer.Append($"Skip={Skip}");
        }
        // result
        if (Result.HasValue)
        {
            if (buffer.Length > 0)
            {
                buffer.Append(", ");
            }
            buffer.Append($"Result={Result}");
        }
        return buffer.ToString();
    }
}