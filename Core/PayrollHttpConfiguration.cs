using System;

namespace PayrollEngine;

/// <summary>The Payroll HTTP configuration</summary>
public class PayrollHttpConfiguration
{
    /// <summary>Default constructor</summary>
    public PayrollHttpConfiguration()
    {
    }

    /// <summary>New instance of the Payroll Engine http client with unknown server certificate, timeout is 100 seconds.
    /// See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.timeout
    /// </summary>
    /// <param name="baseUrl">The Uri the request is sent to</param>
    /// <param name="port">The backend port</param>
    public PayrollHttpConfiguration(string baseUrl, int port) :
        this(baseUrl, port, TimeSpan.FromSeconds(100))
    {
    }

    /// <summary>New instance of Payroll Engine http client with unknown server certificate and request timeout</summary>
    /// <param name="baseUrl">The Uri the request is sent to</param>
    /// <param name="port">The backend port</param>
    /// <param name="requestTimeout">The request timeout</param>
    public PayrollHttpConfiguration(string baseUrl, int port, TimeSpan requestTimeout)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new ArgumentException(nameof(baseUrl));
        }
        BaseUrl = baseUrl;
        Port = port;
        Timeout = requestTimeout;
    }

    /// <summary>The default API request timeout</summary>
    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(100);

    /// <summary>The API base url</summary>
    public string BaseUrl { get; set; }

    /// <summary>The API port</summary>
    public int Port { get; set; }

    /// <summary>The API request timeout</summary>
    public TimeSpan Timeout { get; set; } = DefaultTimeout;

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    public override string ToString() =>
        $"{BaseUrl}:{Port}";
}