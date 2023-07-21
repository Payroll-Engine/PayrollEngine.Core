using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace PayrollEngine
{
    /// <summary>
    /// Payroll engine shared configuration, <see cref="SystemSpecification.PayrollConfigurationVariable"/>
    /// </summary>
    public static class SharedConfiguration
    {
        /// <summary>
        /// Read the shared configuration
        /// </summary>
        /// <returns>Dictionary with key=field name, value=field value as string</returns>
        public static async Task<Dictionary<string, string>> ReadAsync()
        {
            var sharedConfigFileName = Environment.GetEnvironmentVariable(SystemSpecification.PayrollConfigurationVariable);
            if (string.IsNullOrWhiteSpace(sharedConfigFileName) || !File.Exists(sharedConfigFileName))
            {
                return new();
            }
            try
            {
                var json = await File.ReadAllTextAsync(sharedConfigFileName);
                return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
            catch
            {
                return new();
            }

        }

        /// <summary>
        /// Get shared configuration value
        /// </summary>
        public static string GetSharedValue(Dictionary<string, string> sharedConfiguration, string name)
        {
            // primary name
            if (sharedConfiguration.TryGetValue(name, out var value))
            {
                return value;
            }

            // alternative name
            name = name.FirstCharacterToLower();
            if (sharedConfiguration.TryGetValue(name, out value))
            {
                return value;
            }

            return null;
        }
    }
}
