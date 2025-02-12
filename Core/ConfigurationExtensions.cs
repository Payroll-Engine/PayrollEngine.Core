using System;
using Microsoft.Extensions.Configuration;

namespace PayrollEngine;

/// <summary>Configuration helper</summary>
public static class ConfigurationExtensions
{
    /// <summary>Get configuration object by type name</summary>
    /// <param name="configuration">The assembly</param>
    /// <returns>The configuration object</returns>
    public static T GetConfiguration<T>(this IConfiguration configuration) where T : class =>
        GetConfiguration<T>(configuration, typeof(T).Name);

    /// <summary>Get configuration object by type name</summary>
    /// <param name="configuration">The assembly</param>
    /// <param name="name">Configuration name</param>
    /// <returns>The configuration object</returns>
    public static T GetConfiguration<T>(this IConfiguration configuration, string name) where T : class
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }
        IConfigurationSection configurationSection = configuration.GetSection(name);
        return configurationSection.Get<T>();
    }

    /// <summary>Get the database connection string</summary>
    /// <param name="configuration">The assembly</param>
    public static string GetDatabaseConnectionString(this IConfiguration configuration)
    {
        // priority 1: from the environment variable
        var connectionString = Environment.GetEnvironmentVariable(SystemSpecification.PayrollDatabaseConnection);
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            Log.Trace($"Database connection string source: environment variable {SystemSpecification.PayrollDatabaseConnection}.");
            return connectionString;
        }

        // priority 2: from the application configuration file appsettings.json (section connection strings)
        connectionString = configuration.GetConnectionString(SystemSpecification.PayrollDatabaseConnection);
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            Log.Trace("Database connection string source: application configuration (section PayrollDatabaseConnection).");
            return connectionString;
        }

        return null;
    }
}