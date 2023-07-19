using System;

namespace PayrollEngine;

/// <summary>
/// Password change request
/// Use cases:
/// - set initial password: new=required, existing=ignored
/// - change existing password: new=required, existing=required
/// - reset password: new=null, existing=required
/// </summary>
public class PasswordChangeRequest : IEquatable<PasswordChangeRequest>
{
    /// <summary>
    /// The new password
    /// </summary>
    public string NewPassword { get; set; }

    /// <summary>
    /// The existing password
    /// </summary>
    public string ExistingPassword { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordChangeRequest"/> class
    /// </summary>
    public PasswordChangeRequest()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordChangeRequest"/> class
    /// </summary>
    /// <param name="copySource">The copy source.</param>
    public PasswordChangeRequest(PasswordChangeRequest copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public bool Equals(PasswordChangeRequest compare) =>
        CompareTool.EqualProperties(this, compare);
}