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
        ArgumentException.ThrowIfNullOrWhiteSpace(targetFileName);

        if (File.Exists(targetFileName))
        {
            File.Delete(targetFileName);
        }

        stream.Position = 0;
        await using var fileStream = File.Create(targetFileName);
        await stream.CopyToAsync(fileStream);
    }

    /// <summary>Write stream to file</summary>
    /// <param name="stream">The stream</param>
    /// <param name="targetFileName">The target file name</param>
    /// <remarks>Existing file will be deleted</remarks>
    public static async Task WriteToFile(this Stream stream, string targetFileName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(targetFileName);

        if (File.Exists(targetFileName))
        {
            File.Delete(targetFileName);
        }

        stream.Position = 0;
        await using var fileStream = File.Create(targetFileName);
        await stream.CopyToAsync(fileStream);
    }
}