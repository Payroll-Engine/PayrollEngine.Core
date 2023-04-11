using System;

namespace PayrollEngine
{
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
    }
}
