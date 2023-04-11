using System;

namespace PayrollEngine;

/// <summary>Extension methods for <see cref="Uri"/> </summary>
public static class QueryExtensions
{
    /// <summary>Append the given object query key and value to the URI</summary>
    /// <param name="query">The query to append</param>
    /// <param name="uri">The base URI</param>
    /// <returns>The combined result</returns>
    public static string AppendQueryString(this Query query, string uri)
    {
        uri = uri.AddQueryString(QuerySpecification.StatusOperation, query.Status);
        uri = uri.AddQueryString(QuerySpecification.OrderByOperation, query.OrderBy);
        uri = uri.AddQueryString(QuerySpecification.FilterOperation, query.Filter);
        uri = uri.AddQueryString(QuerySpecification.SelectOperation, query.Select);
        uri = uri.AddQueryString(QuerySpecification.TopOperation, query.Top);
        uri = uri.AddQueryString(QuerySpecification.SkipOperation, query.Skip);
        uri = uri.AddQueryString(QuerySpecification.ResultOperation, query.Result);
        return uri;
    }
}