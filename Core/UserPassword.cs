using System;
using System.Text.RegularExpressions;

namespace PayrollEngine;

/// <summary>
/// User password tool
/// </summary>
public static class UserPassword
{
    private const string expression = "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z])(?=.*[@$!%*?&]).{8,}$";

    /// <summary>
    /// Test for valid user password
    /// </summary>
    /// <remarks>password rules: 1 digit, 1 lowercase, 1 uppercase, 1 special character and minimum 8 characters</remarks>
    /// <param name="test"></param>
    /// <returns></returns>
    public static bool IsValid(string test)
    {
        if (string.IsNullOrWhiteSpace(test))
        {
            throw new ArgumentException(nameof(test));
        }

        // test .NET regex: https://regex101.com/
        var match = Regex.Match(test, expression);
        return match.Success;
    }

    /// <summary>Verify an encrypted password</summary>
    /// <param name="password">The existing password</param>
    /// <param name="hashSalt">The hash salt to use</param>
    /// <param name="verifyPassword">The password to verify</param>
    /// <returns>True for valid password</returns>
    public static bool VerifyPassword(string password, byte[] hashSalt, string verifyPassword)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException(nameof(password));
        }
        if (hashSalt == null)
        {
            throw new ArgumentNullException(nameof(hashSalt));
        }
        if (string.IsNullOrWhiteSpace(verifyPassword))
        {
            throw new ArgumentException(nameof(verifyPassword));
        }

        var existingHashSalt = new HashSalt(password, hashSalt);
        var testHashSalt = verifyPassword.ToHashSalt(hashSalt);
        return existingHashSalt.Equals(testHashSalt);
    }
}