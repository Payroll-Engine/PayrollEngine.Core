
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PayrollEngine;

/// <summary>
/// Factory for queries
/// </summary>
public static class QueryFactory
{
    /// <summary>
    /// Active objects query
    /// </summary>
    public static Query Active => new() { Status = ObjectStatus.Active };

    /// <summary>
    /// Inactive objects query
    /// </summary>
    public static Query Inactive => new() { Status = ObjectStatus.Inactive };

    /// <summary>
    /// New filter query with one equals condition
    /// </summary>
    /// <param name="column">The column name</param>
    /// <param name="value">The filter value</param>
    /// <returns>New equals filter query</returns>
    public static Query NewEqualFilterQuery(string column, object value) =>
        NewEqualFilterQuery(new Dictionary<string, object>
        {
            {column, value}
        });

    /// <summary>
    /// New filter query with two equals conditions
    /// </summary>
    /// <param name="column1">The first column name</param>
    /// <param name="value1">The first filter value</param>
    /// <param name="column2">The second column name</param>
    /// <param name="value2">The second filter value</param>
    /// <returns>New equals filter query</returns>
    public static Query NewEqualsFilterQuery(string column1, object value1, string column2, object value2) =>
        NewEqualFilterQuery(new Dictionary<string, object>
        {
            {column1, value1},
            {column2, value2}
        });

    /// <summary>
    /// New filter query with multiple equals conditions
    /// </summary>
    /// <param name="values">The values by column name</param>
    /// <returns>New equals filter query</returns>
    public static Query NewEqualFilterQuery(IDictionary<string, object> values)
    {
        if (values == null)
        {
            throw new ArgumentNullException(nameof(values));
        }
        if (!values.Any())
        {
            throw new ArgumentException("Missing filter conditions", nameof(values));
        }

        var buffer = new StringBuilder();
        foreach (var value in values)
        {
            if (string.IsNullOrWhiteSpace(value.Key))
            {
                throw new PayrollException($"Missing filter column name for value {value.Value}");
            }

            // following condition
            if (buffer.Length > 0)
            {
                buffer.Append(" and ");
            }
            buffer.Append($"{value.Key} eq {GetFilterText(value.Value)}");
        }
        return new()
        {
            Filter = buffer.ToString()
        };
    }

    /// <summary>
    /// New query width the object id filter
    /// </summary>
    /// <param name="id">The object id</param>
    /// <returns>New identifier query</returns>
    public static Query NewIdQuery(int id) =>
        NewEqualFilterQuery("id", id);

    /// <summary>
    /// New query width an identifier filter
    /// </summary>
    /// <param name="identifier">The identifier</param>
    /// <returns>New identifier query</returns>
    public static Query NewIdentifierQuery(string identifier) =>
        NewEqualFilterQuery("identifier", identifier);

    /// <summary>
    /// New query width a name filter
    /// </summary>
    /// <param name="name">The identifier</param>
    /// <returns>New name query</returns>
    public static Query NewNameQuery(string name) =>
        NewEqualFilterQuery("name", name);

    private static string GetFilterText(object value)
    {
        if (value == null)
        {
            return "null";
        }
        if (value is string)
        {
            return $"'{value}'";
        }
        if (value is DateTime dateTime)
        {
            return dateTime.ToUtc().ToString(CultureInfo.InvariantCulture);
        }
        return value.ToString();
    }
}