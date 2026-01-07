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

    /// <param name="password">The password to encrypt</param>
    extension(string password)
    {
        /// <summary>
        /// Encrypt password
        /// </summary>
        /// <returns></returns>
        public HashSalt ToHashSalt()
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(null, nameof(password));
            }

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
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(null, nameof(password));
            }
            ArgumentNullException.ThrowIfNull(salt);

            // generate hash
            var pbkdf2 = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA1, HashSize);
            return new() { Hash = Convert.ToBase64String(pbkdf2), Salt = salt };
        }
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

        var testEncrypted = verifyPassword.ToHashSalt(hashSalt.Salt);
        return string.Equals(testEncrypted.Hash, hashSalt.Hash);
    }
}