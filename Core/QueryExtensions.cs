using System;

namespace PayrollEngine;

/// <summary>Extension methods for <see cref="Uri"/> </summary>
public static class QueryExtensions
{
    /// <summary>Test if query contains a status</summary>
    /// <param name="query">The query to append</param>
    /// <returns>True if status is defined</returns>
    public static bool HasStatus(this Query query) =>
        query != null && query.Status.HasValue;

    /// <summary>Test if query contains a filter</summary>
    /// <param name="query">The query to append</param>
    /// <returns>True if filter is defined</returns>
    public static bool HasFilter(this Query query) =>
        query != null && !string.IsNullOrWhiteSpace(query.Filter);

    /// <summary>Test if query contains an order by</summary>
    /// <param name="query">The query to append</param>
    /// <returns>True if order by is defined</returns>
    public static bool HasOrderBy(this Query query) =>
        query != null && !string.IsNullOrWhiteSpace(query.OrderBy);

    /// <summary>Test if query contains a select</summary>
    /// <param name="query">The query to append</param>
    /// <returns>True if select is defined</returns>
    public static bool HasSelect(this Query query) =>
        query != null && !string.IsNullOrWhiteSpace(query.Select);

    /// <summary>Test if query contains a top</summary>
    /// <param name="query">The query to append</param>
    /// <returns>True if top is defined</returns>
    public static bool HasTop(this Query query) =>
        query != null && query.Top.HasValue;

    /// <summary>Test if query contains a skip</summary>
    /// <param name="query">The query to append</param>
    /// <returns>True if skip is defined</returns>
    public static bool HasSkip(this Query query) =>
        query != null && query.Skip.HasValue;

    /// <summary>Test if query contains a result</summary>
    /// <param name="query">The query to append</param>
    /// <returns>True if result is defined</returns>
    public static bool HasResult(this Query query) =>
        query != null && query.Result.HasValue;

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