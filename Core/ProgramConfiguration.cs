using System;
using Microsoft.Extensions.Configuration;

namespace PayrollEngine;

/// <summary>Access to the program configuration</summary>
public class ProgramConfiguration<TApp> where TApp : class
{
    /// <summary>Gets the configuration root</summary>
    public IConfigurationRoot Configuration { get; }

    /// <summary>Initializes a new instance of the <see cref="ProgramConfiguration{TApp}"/> class</summary>
    public ProgramConfiguration(ProgramConfigurationOptions options = ProgramConfigurationOptions.Default)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

        // app settings: json file or log
        if (options.HasFlag(ProgramConfigurationOptions.AppSettings))
        {
            builder.AddJsonFile("appsettings.json");
        }

        // user secrets
        if (options.HasFlag(ProgramConfigurationOptions.UserSecrets))
        {
            builder.AddUserSecrets<TApp>();
        }

        Configuration = builder.Build();
    }

    /// <summary>Gets the application setting by key</summary>
    /// <param name="key">The configuration key</param>
    /// <returns>The configuration value</returns>
    public string Get(string key) =>
        Configuration[key];

    /// <summary>Gets the application setting by key</summary>
    /// <param name="key">The configuration key</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The configuration value</returns>
    public string Get(string key, string defaultValue)
    {
        var value = Get(key);
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }

    /// <summary>Gets the application setting object by type name</summary>
    /// <returns>The configuration object</returns>
    public T GetConfiguration<T>() =>
        GetConfiguration<T>(typeof(T).Name);

    /// <summary>Gets the application setting object by key</summary>
    /// <param name="key">The object key name</param>
    /// <returns>The configuration object</returns>
    public T GetConfiguration<T>(string key)
    {
        var section = Configuration.GetSection(key);
        if (section == null)
        {
            throw new ArgumentException($"Unknown configuration section {key}");
        }
        return section.Get<T>();
    }

    /// <summary>Get connection string</summary>
    /// <param name="name">The connection string key</param>
    /// <returns>The connection string</returns>
    public string GetConnectionString(string name) =>
        Configuration.GetConnectionString(name);
}