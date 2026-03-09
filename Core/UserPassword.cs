using System;
using System.Text.RegularExpressions;

namespace PayrollEngine;

/// <summary>
/// User password tool
/// </summary>
public static class UserPassword
{
    /// <summary>Regex timeout to prevent ReDoS attacks</summary>
    private static readonly TimeSpan RegexTimeout = TimeSpan.FromSeconds(1);

    private const string Expression = "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z])(?=.*[@$!%*?&]).{8,}$";

    /// <summary>
    /// Test for valid user password
    /// </summary>
    /// <remarks>Password rules: 1 digit, 1 lowercase, 1 uppercase, 1 special character and minimum 8 characters</remarks>
    /// <param name="test">The password to validate</param>
    /// <returns>True if the password meets complexity requirements</returns>
    public static bool IsValid(string test)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(test);

        // test .NET regex: https://regex101.com/
        try
        {
            var match = Regex.Match(test, Expression, RegexOptions.None, RegexTimeout);
            return match.Success;
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    /// <summary>Verify an encrypted password</summary>
    /// <param name="password">The existing password</param>
    /// <param name="hashSalt">The hash salt to use</param>
    /// <param name="verifyPassword">The password to verify</param>
    /// <returns>True for valid password</returns>
    public static bool VerifyPassword(string password, byte[] hashSalt, string verifyPassword)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        ArgumentNullException.ThrowIfNull(hashSalt);
        ArgumentException.ThrowIfNullOrWhiteSpace(verifyPassword);

        var existingHashSalt = new HashSalt(password, hashSalt);
        var testHashSalt = verifyPassword.ToHashSalt(hashSalt);
        return existingHashSalt.Equals(testHashSalt);
    }
}