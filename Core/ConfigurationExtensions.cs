using System;
using Microsoft.Extensions.Configuration;

namespace PayrollEngine;

/// <summary>Configuration helper</summary>
public static class ConfigurationExtensions
{
    /// <param name="configuration">The assembly</param>
    extension(IConfiguration configuration)
    {
        /// <summary>Get configuration object by type name</summary>
        /// <returns>The configuration object</returns>
        public T GetConfiguration<T>() where T : class => 
            configuration.GetConfiguration<T>(typeof(T).Name);

        /// <summary>Get configuration object by type name</summary>
        /// <param name="name">Configuration name</param>
        /// <returns>The configuration object</returns>
        public T GetConfiguration<T>(string name) where T : class
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            IConfigurationSection configurationSection = configuration.GetSection(name);
            return configurationSection.Get<T>();
        }

        /// <summary>Get the database connection string</summary>
        public string GetDatabaseConnectionString()
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
}