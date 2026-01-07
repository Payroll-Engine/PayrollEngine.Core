using System;

namespace PayrollEngine;

/// <summary>Extension methods for <see cref="Uri"/> </summary>
public static class QueryExtensions
{
    /// <param name="query">The query to append</param>
    extension(Query query)
    {
        /// <summary>Test if query contains a status</summary>
        /// <returns>True if status is defined</returns>
        public bool HasStatus() =>
            query != null && query.Status.HasValue;

        /// <summary>Test if query contains a filter</summary>
        /// <returns>True if filter is defined</returns>
        public bool HasFilter() =>
            query != null && !string.IsNullOrWhiteSpace(query.Filter);

        /// <summary>Test if query contains an order by</summary>
        /// <returns>True if order by is defined</returns>
        public bool HasOrderBy() =>
            query != null && !string.IsNullOrWhiteSpace(query.OrderBy);

        /// <summary>Test if query contains a select</summary>
        /// <returns>True if select is defined</returns>
        public bool HasSelect() =>
            query != null && !string.IsNullOrWhiteSpace(query.Select);

        /// <summary>Test if query contains a top</summary>
        /// <returns>True if top is defined</returns>
        public bool HasTop() =>
            query != null && query.Top.HasValue;

        /// <summary>Test if query contains a skip</summary>
        /// <returns>True if skip is defined</returns>
        public bool HasSkip() =>
            query != null && query.Skip.HasValue;

        /// <summary>Test if query contains a result</summary>
        /// <returns>True if result is defined</returns>
        public bool HasResult() =>
            query != null && query.Result.HasValue;

        /// <summary>Append the given object query key and value to the URI</summary>
        /// <param name="uri">The base URI</param>
        /// <returns>The combined result</returns>
        public string AppendQueryString(string uri)
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
}