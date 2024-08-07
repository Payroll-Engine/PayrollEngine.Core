﻿using System.Text.Json.Serialization;

namespace PayrollEngine;

/// <summary>The type of the user</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserType
{
    /// <summary>Regular user</summary>
    User = 0,

    /// <summary>User with employee self-service</summary>
    Employee = 1,

    /// <summary>User who can manage users</summary>
    Administrator = 2,

    /// <summary>User with access to any feature</summary>
    Supervisor = 3
}