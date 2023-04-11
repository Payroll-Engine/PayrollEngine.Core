using System;

namespace PayrollEngine;

/// <summary>Program configuration options</summary>
[Flags]
public enum ProgramConfigurationOptions
{
    /// <summary>Default configuration</summary>
    Default = AppSettings | UserSecrets,

    /// <summary>Use json configuration file appsettings.json</summary>
    AppSettings = 0x01,

    /// <summary>Enable user secrets</summary>
    UserSecrets = 0x02
}