using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PayrollEngine.IO;

/// <summary>CSV serializer from/to file, stream and reader</summary>
/// <remarks>
/// source: https://github.com/jcoehoorn/EasyCSV
/// </remarks>
public static class CsvSerializer
{
    /// <summary>Write CSV to file</summary>
    /// <param name="fileName">The file name</param>
    /// <param name="rows">The CSV rows</param>
    /// <param name="encoding">The encoding (default: UTF8)</param>
    /// <param name="columnSeparator">The default column separator (default: ,)</param>
    public static void ToFile(string fileName, IEnumerable<IList<string>> rows,
        Encoding encoding = null, char columnSeparator = ',')
    {
        if (rows == null)
        {
            throw new ArgumentNullException(nameof(rows));
        }

        // default encoding
        encoding ??= Encoding.UTF8;

        using StreamWriter stream = new(new FileStream(fileName, FileMode.CreateNew, FileAccess.Write), encoding);
        foreach (var row in rows)
        {
            for (var column = 0; column < row.Count; column++)
            {
                if (column > 0)
                {
                    stream.Write(columnSeparator);
                }
                stream.Write(row[column]);
            }
            stream.WriteLine();
        }
    }

    /// <summary>Get CSV from file</summary>
    /// <param name="fileName">The file name</param>
    /// <param name="encoding">The encoding (default: UTF8)</param>
    /// <param name="ignoreFirstLine">Ignore the header line (default: false)</param>
    /// <param name="columnSeparator">The default column separator (default: ,)</param>
    /// <returns>The CSV string list</returns>
    public static IEnumerable<IList<string>> FromFile(string fileName, Encoding encoding = null,
        bool ignoreFirstLine = false, char columnSeparator = ',')
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }

        // default encoding
        encoding ??= Encoding.UTF8;
        using StreamReader reader = new(fileName, encoding);
        foreach (var item in FromReaderAsync(reader, ignoreFirstLine, columnSeparator))
        {
            yield return item;
        }
    }

    /// <summary>Get CSV from stream</summary>
    /// <param name="stream">The stream</param>
    /// <param name="ignoreFirstLine">Ignore the header line (default: false)</param>
    /// <param name="columnSeparator">The default column separator (default: ,)</param>
    /// <returns>The CSV string list</returns>
    public static IEnumerable<IList<string>> FromStream(Stream stream,
        bool ignoreFirstLine = false, char columnSeparator = ',')
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        using StreamReader reader = new(stream);
        foreach (var item in FromReaderAsync(reader, ignoreFirstLine, columnSeparator))
        {
            yield return item;
        }
    }

    /// <summary>Get CSV from stream</summary>
    /// <param name="reader">The reader</param>
    /// <param name="ignoreFirstLine">Ignore the header line (default: false)</param>
    /// <param name="columnSeparator">The default column separator (default: ,)</param>
    /// <returns>The CSV string list</returns>
    public static IEnumerable<IList<string>> FromReaderAsync(TextReader reader,
        bool ignoreFirstLine = false, char columnSeparator = ',')
    {
        if (reader == null)
        {
            throw new ArgumentNullException(nameof(reader));
        }
        if (ignoreFirstLine)
        {
            reader.ReadLine();
        }

        IList<string> result = new List<string>();
        StringBuilder curValue = new();
        var c = (char)reader.Read();
        while (reader.Peek() != -1)
        {
            if (c == columnSeparator)
            {
                // empty field
                result.Add(string.Empty);
                c = (char)reader.Read();
                if (c == '\r' || c == '\n')
                {
                    result.Add(string.Empty);
                }
            }
            else
            {
                switch (c)
                {
                    // qualified text
                    case '"':
                    case '\'':
                        var q = c;
                        c = (char)reader.Read();
                        var inQuotes = true;
                        while (inQuotes && reader.Peek() != -1)
                        {
                            if (c == q)
                            {
                                c = (char)reader.Read();
                                if (c != q)
                                {
                                    inQuotes = false;
                                }
                            }
                            if (inQuotes)
                            {
                                curValue.Append(c);
                                c = (char)reader.Read();
                            }
                        }
                        result.Add(curValue.ToString());
                        curValue = new();
                        if (c == columnSeparator)
                        {
                            // either ',', newline, or end of stream
                            c = (char)reader.Read();
                        }
                        break;

                    // end of the record
                    case '\n':
                    case '\r':
                        // potential error here depending on what your line breaks look like
                        // don't return empty records
                        if (result.Count > 0)
                        {
                            yield return result;
                            result = new List<string>();
                        }
                        c = (char)reader.Read();
                        break;

                    // normal unqualified text
                    default:
                        while (c != columnSeparator && c != '\r' && c != '\n')
                        {
                            curValue.Append(c);
                            // last item in last line
                            if (reader.Peek() == -1)
                            {
                                break;
                            }
                            c = (char)reader.Read();
                        }
                        result.Add(curValue.ToString());
                        curValue = new();
                        if (c == columnSeparator)
                        {
                            // either ',', newline, or end of stream
                            c = (char)reader.Read();
                            // empty last item
                            if (c == '\r' || c == '\n')
                            {
                                result.Add(string.Empty);
                            }
                        }
                        break;
                }
            }
        }

        // potential error: I don't want to skip on a empty column in the last record if a caller really expects it to be there
        if (curValue.Length > 0)
        {
            result.Add(curValue.ToString());
        }

        if (result.Count > 0)
        {
            yield return result;
        }
    }
}