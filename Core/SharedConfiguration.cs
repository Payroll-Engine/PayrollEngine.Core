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
        /// Read a shared configuration value
        /// </summary>
        /// <param name="fieldName">The field name</param>
        /// <returns>The field value as string</returns>
        public static async Task<string> ReadValueAsync(string fieldName)
        {
            var config = await ReadAsync();

            // direct match
            if (config.TryGetValue(fieldName, out var value))
            {
                return value;
            }

            // try camel case
            fieldName = fieldName.FirstCharacterToLower();
            if (config.TryGetValue(fieldName, out value))
            {
                return value;
            }
            return null;
        }
    }
}
