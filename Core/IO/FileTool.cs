using System;
using System.IO;

namespace PayrollEngine.IO;

/// <summary>File tools</summary>
public static class FileTool
{
    /// <summary>Get the current local file time stamp name</summary>
    /// <returns>The file name</returns>
    public static string CurrentTimeStamp() =>
        TimeStamp(DateTime.Now);

    /// <summary>Get the file time stamp name</summary>
    /// <param name="moment">The time moment</param>
    /// <returns>The file name</returns>
    public static string TimeStamp(DateTime moment) =>
        $"{moment:yyyyMMdd_HHmm}";

    /// <summary>Ensure valid file name</summary>
    /// <param name="fileName">The file name </param>
    /// <param name="replacement">Invalid character replacement</param>
    /// <returns>The file name</returns>
    public static string ToValidFileName(string fileName, char replacement = '_')
    {
        if (!string.IsNullOrWhiteSpace(fileName))
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, replacement);
            }
        }
        return fileName;
    }
}