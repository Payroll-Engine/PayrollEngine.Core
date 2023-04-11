using Microsoft.Extensions.Configuration;

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

}