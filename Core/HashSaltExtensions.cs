using System;
using System.Security.Cryptography;

namespace PayrollEngine;

/// <summary>
/// Password tool
/// </summary>
public static class HashSaltExtensions
{
    private const int SaltSize = 24; // size in bytes
    private const int HashSize = 24; // size in bytes
    private const int Iterations = 100000; // number of pbkdf2 iterations

    /// <summary>
    /// Encrypt password
    /// </summary>
    /// <param name="password">The password to encrypt</param>
    /// <returns></returns>
    public static HashSalt ToHashSalt(this string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException(null, nameof(password));
        }

        // generate salt
        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        // encrypt
        return ToHashSalt(password, salt);
    }

    /// <summary>
    /// Encrypt password with salt
    /// </summary>
    /// <remarks>https://riptutorial.com/csharp/example/10258/pbkdf2-for-password-hashing</remarks>
    /// <param name="password">The password to encrypt</param>
    /// <param name="salt">The salt</param>
    /// <returns>The hash salt</returns>
    public static HashSalt ToHashSalt(this string password, byte[] salt)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException(null, nameof(password));
        }
        ArgumentNullException.ThrowIfNull(salt);

        // generate hash
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA1);
        return new() { Hash = Convert.ToBase64String(pbkdf2.GetBytes(HashSize)), Salt = salt };
    }

    /// <summary>
    /// Verify an encrypted password
    /// </summary>
    /// <param name="verifyPassword">The password to verify</param>
    /// <param name="hashSalt">The hash salt to use</param>
    /// <returns>True for valid password</returns>
    public static bool VerifyPassword(this HashSalt hashSalt, string verifyPassword)
    {
        ArgumentNullException.ThrowIfNull(hashSalt);
        if (string.IsNullOrWhiteSpace(verifyPassword))
        {
            throw new ArgumentException(null, nameof(verifyPassword));
        }

        var testEncrypted = ToHashSalt(verifyPassword, hashSalt.Salt);
        return string.Equals(testEncrypted.Hash, hashSalt.Hash);
    }
}