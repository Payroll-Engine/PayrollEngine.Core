using System;

namespace PayrollEngine;

/// <summary>
/// The encryption hash salt
/// </summary>
public class HashSalt : IEquatable<HashSalt>
{
    /// <summary>
    /// default constructor
    /// </summary>
    public HashSalt()
    {
    }

    /// <summary>
    /// Value constructor
    /// </summary>
    /// <param name="hash">The hash</param>
    /// <param name="salt">The salt</param>
    public HashSalt(string hash, byte[] salt)
    {
        Hash = hash;
        Salt = salt;
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public HashSalt(HashSalt copySource)
    {
        Hash = copySource.Hash;
        Salt = copySource.Salt;
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public bool Equals(HashSalt compare) =>
        compare != null &&
        string.Equals(Hash, compare.Hash) &&
        CompareTool.EqualLists(Salt, compare.Salt);

    /// <summary>
    /// The hash
    /// </summary>
    public string Hash { get; set; }

    /// <summary>
    /// THe salt
    /// </summary>
    public byte[] Salt { get; set; }
}