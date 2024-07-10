using System;
using System.IO;
using System.Threading.Tasks;

namespace PayrollEngine;

/// <summary>Stream extension methods</summary>
public static class StreamExtensions
{

    /// <summary>Get configuration object by type name</summary>
    /// <param name="stream">The memory stream</param>
    /// <param name="targetFileName">The target file name</param>
    /// <remarks>Existing file will be deleted</remarks>
    /// <returns>The configuration object</returns>
    public static async Task WriteToFile(this MemoryStream stream, string targetFileName)
    {
        if (string.IsNullOrWhiteSpace(targetFileName))
        {
            throw new ArgumentException(null, nameof(targetFileName));
        }

        if (File.Exists(targetFileName))
        {
            File.Delete(targetFileName);
        }

        await using var fileStream = File.Create(targetFileName);
        await fileStream.CopyToAsync(stream);
        await stream.CopyToAsync(fileStream);
    }
}