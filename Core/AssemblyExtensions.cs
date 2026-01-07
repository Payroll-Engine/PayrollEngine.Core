using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace PayrollEngine;

/// <summary>Helper to access code from an embedded script resources</summary>
public static class AssemblyExtensions
{
    /// <param name="assembly">The assembly</param>
    extension(Assembly assembly)
    {
        /// <summary>Get code from embedded resource file</summary>
        /// <param name="resourceName">The code resource name</param>
        /// <param name="allowEmpty">Allow empty resource content</param>
        /// <returns>The resource code</returns>
        public string GetEmbeddedFile(string resourceName, bool allowEmpty = true)
        {
            var name = EnsureResourceName(assembly, resourceName);
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new PayrollException($"Unknown embedded resource {resourceName}.");
            }

            using Stream resourceStream = assembly.GetManifestResourceStream(resourceName);
            if (resourceStream == null)
            {
                throw new PayrollException($"Error reading embedded Resource {resourceName}.");
            }

            using StreamReader reader = new(resourceStream);
            var content = reader.ReadToEnd();

            if (!allowEmpty && string.IsNullOrWhiteSpace(content))
            {
                throw new PayrollException($"Empty embedded resource {resourceName}.");
            }

            return content;
        }

        /// <summary>Get all code from embedded resource files</summary>
        /// <returns>The resource codes</returns>
        public IEnumerable<string> GetEmbeddedFiles() => 
            assembly.GetEmbeddedFiles(assembly.GetManifestResourceNames());

        /// <summary>Get the code from multiple embedded resources</summary>
        /// <param name="resourceNames">The code resource names</param>
        /// <returns>The resource codes</returns>
        public IEnumerable<string> GetEmbeddedFiles(IEnumerable<string> resourceNames)
        {
            ArgumentNullException.ThrowIfNull(resourceNames);
            var codes = new List<string>();
            foreach (var resourceName in resourceNames)
            {
                var code = assembly.GetEmbeddedFile(resourceName, allowEmpty: false);
                codes.Add(code);
            }
            return codes;
        }
    }

    private static string EnsureResourceName(Assembly assembly, string resourceName)
    {
        if (string.IsNullOrWhiteSpace(resourceName))
        {
            return null;
        }

        // all resource names
        var resourceNames = assembly.GetManifestResourceNames();

        // valid resource name
        if (resourceNames.Contains(resourceName))
        {
            return resourceName;
        }

        // non-windows resource in sub path
        var pathResourceName = resourceName.Replace("\\", "/");
        if (resourceNames.Contains(pathResourceName))
        {
            return pathResourceName;
        }

        return null;
    }
}