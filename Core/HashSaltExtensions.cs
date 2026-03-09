using System;
using System.Security.Cryptography;

namespace PayrollEngine;

/// <summary>
/// Password tool
/// </summary>
public static class HashSaltExtensions
{
    private const int SaltSize = 24; // size in bytes
    private const int HashSize = 32; // size in bytes (increased for SHA256)
    private const int Iterations = 100000; // number of pbkdf2 iterations

    /// <param name="password">The password to encrypt</param>
    extension(string password)
    {
        /// <summary>
        /// Encrypt password
        /// </summary>
        /// <returns>The hash salt</returns>
        public HashSalt ToHashSalt()
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);

            // generate salt
            var salt = RandomNumberGenerator.GetBytes(SaltSize);

            // encrypt
            return password.ToHashSalt(salt);
        }

        /// <summary>
        /// Encrypt password with salt
        /// </summary>
        /// <remarks>https://riptutorial.com/csharp/example/10258/pbkdf2-for-password-hashing</remarks>
        /// <param name="salt">The salt</param>
        /// <returns>The hash salt</returns>
        public HashSalt ToHashSalt(byte[] salt)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);
            ArgumentNullException.ThrowIfNull(salt);

            // generate hash using SHA256 (SHA1 is cryptographically weak)
            var pbkdf2 = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
            return new() { Hash = Convert.ToBase64String(pbkdf2), Salt = salt };
        }
    }

    /// <summary>
    /// Verify an encrypted password using constant-time comparison
    /// </summary>
    /// <param name="verifyPassword">The password to verify</param>
    /// <param name="hashSalt">The hash salt to use</param>
    /// <returns>True for valid password</returns>
    public static bool VerifyPassword(this HashSalt hashSalt, string verifyPassword)
    {
        ArgumentNullException.ThrowIfNull(hashSalt);
        ArgumentException.ThrowIfNullOrWhiteSpace(verifyPassword);

        var testEncrypted = verifyPassword.ToHashSalt(hashSalt.Salt);

        // constant-time comparison to prevent timing attacks
        var existingBytes = Convert.FromBase64String(hashSalt.Hash);
        var testBytes = Convert.FromBase64String(testEncrypted.Hash);
        return CryptographicOperations.FixedTimeEquals(existingBytes, testBytes);
    }
}