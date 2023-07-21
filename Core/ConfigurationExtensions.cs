using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PayrollEngine;

/// <summary>Configuration helper</summary>
public static class ConfigurationExtensions
{
    /// <summary>Get configuration object by type name</summary>
    /// <param name="configuration">The assembly</param>
    /// <returns>The configuration object</returns>
    public static T GetConfiguration<T>(this IConfiguration configuration) where T : class
    {
        IConfigurationSection configurationSection = configuration.GetSection(typeof(T).Name);
        return configurationSection.Get<T>();
    }

    /// <summary>Get the database connection string</summary>
    public static async Task<string> GetSharedConnectionStringAsync(this IConfiguration configuration)
    {
        string connectionString;

        // priority 1: shared configuration
        var sharedConfigFileName = Environment.GetEnvironmentVariable(SystemSpecification.PayrollConfigurationVariable);
        if (!string.IsNullOrWhiteSpace(sharedConfigFileName) && File.Exists(sharedConfigFileName))
        {
            var sharedConfiguration = await SharedConfiguration.ReadAsync();
            connectionString = SharedConfiguration.GetSharedValue(sharedConfiguration, SystemSpecification.SharedDatabaseConnectionString);
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                return connectionString;
            }
        }

        // priority 2: application configuration
        connectionString = configuration.GetConnectionString(SystemSpecification.ConfigurationDatabaseConnectionString);
        return connectionString;
    }
}