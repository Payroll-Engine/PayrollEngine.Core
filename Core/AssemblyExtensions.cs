﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PayrollEngine;

/// <summary>Helper to access code from an embedded script resources</summary>
public static class AssemblyExtensions
{
    /// <summary>Get code from embedded resource file</summary>
    /// <param name="assembly">The assembly</param>
    /// <param name="resourceName">The code resource name</param>
    /// <returns>The resource code</returns>
    public static string GetEmbeddedFile(this Assembly assembly, string resourceName)
    {
        if (string.IsNullOrWhiteSpace(resourceName))
        {
            throw new ArgumentException(null, nameof(resourceName));
        }

        using Stream resourceStream = assembly.GetManifestResourceStream(resourceName);
        if (resourceStream == null)
        {
            throw new PayrollException($"Resource {resourceName} is not available");
        }

        using StreamReader reader = new(resourceStream);
        return reader.ReadToEnd();
    }

    /// <summary>Get all code from embedded resource files</summary>
    /// <param name="assembly">The assembly</param>
    /// <returns>The resource codes</returns>
    public static IEnumerable<string> GetEmbeddedFiles(this Assembly assembly) =>
        GetEmbeddedFiles(assembly, assembly.GetManifestResourceNames());

    /// <summary>Get the code from multiple embedded resources</summary>
    /// <param name="assembly">The assembly</param>
    /// <param name="resourceNames">The code resource names</param>
    /// <returns>The resource codes</returns>
    public static IEnumerable<string> GetEmbeddedFiles(this Assembly assembly, IEnumerable<string> resourceNames)
    {
        ArgumentNullException.ThrowIfNull(resourceNames);

        var codes = new List<string>();
        foreach (var resourceName in resourceNames)
        {
            using Stream resourceStream = assembly.GetManifestResourceStream(resourceName);
            if (resourceStream == null)
            {
                throw new PayrollException($"Resource {resourceName} is not available");
            }

            using StreamReader reader = new(resourceStream);
            var code = reader.ReadToEnd();
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new PayrollException($"Empty code in embedded resource {resourceName}");
            }
            codes.Add(code);
        }
        return codes;
    }
}