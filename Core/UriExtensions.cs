using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;

namespace PayrollEngine;

/// <summary>Extension methods for <see cref="Uri"/> </summary>
public static class UriExtensions
{
    /// <summary>Get the last uri segment as numeric value</summary>
    /// <param name="uri">The uri</param>
    /// <returns>The numeric value of the last uri segment</returns>
    public static int GetLastSegmentId(this Uri uri)
    {
        var lastSegment = uri.OriginalString.Split('/').Last();
        if (!int.TryParse(lastSegment, out var id) || id <= 0)
        {
            throw new PayrollException($"Invalid Uri {uri} for object id");
        }
        return id;
    }

    /// <summary>Ensures the URL validity</summary>
    /// <param name="uri">The base URI</param>
    /// <returns>Closed URI</returns>
    public static string EnsureClosedUri(this string uri) => uri.EnsureEnd("/");

    /// <summary>Append the given query key and value to the URI</summary>
    /// <param name="uri">The base URI</param>
    /// <param name="name">The name of the query key</param>
    /// <param name="values">The collection value</param>
    /// <returns>The combined result</returns>
    public static string AddCollectionQueryString(this string uri, string name, IEnumerable values)
    {
        if (values != null)
        {
            foreach (var value in values)
            {
                uri = uri.AddQueryString(name, value);
            }
        }
        return uri;
    }

    /// <summary>Append the given query key and value to the URI</summary>
    /// <param name="uri">The base URI</param>
    /// <param name="name">The name of the query key</param>
    /// <param name="value">The query value (enum:names, date time: UTC round trip pattern)</param>
    /// <returns>The combined result</returns>
    public static string AddQueryString(this string uri, string name, object value)
    {
        if (value == null)
        {
            return uri;
        }
        var type = value.GetType();

        string stringValue;
        if (type.IsEnum)
        {
            stringValue = Enum.GetName(type, value);
        }
        else if (value is decimal decimalValue)
        {
            return AddQueryString(uri, name, decimalValue);
        }
        else if (value is DateTime dateTimeValue)
        {
            return AddQueryString(uri, name, dateTimeValue);
        }
        else
        {
            // others
            stringValue = value.ToString();
        }
        return QueryHelpers.AddQueryString(uri, name, stringValue);
    }

    /// <summary>Append the given query key and value to the URI</summary>
    /// <param name="uri">The base URI</param>
    /// <param name="name">The name of the query key</param>
    /// <param name="value">The decimal value</param>
    /// <returns>The combined result</returns>
    public static string AddQueryString(this string uri, string name, decimal value) =>
        QueryHelpers.AddQueryString(uri, name, value.ToString(CultureInfo.InvariantCulture));

    /// <summary>Append the given query key and value to the URI</summary>
    /// <param name="uri">The base URI</param>
    /// <param name="name">The name of the query key</param>
    /// <param name="value">The date time value</param>
    /// <returns>The combined result</returns>
    public static string AddQueryString(this string uri, string name, DateTime value) =>
        QueryHelpers.AddQueryString(uri, name, value.ToString(CultureInfo.InvariantCulture));
}