namespace Frends.MicrosoftDynamics365.Request.Tests;

using System;
using System.IO;
using dotenv.net;
using NUnit.Framework;

/// <summary>
/// Provides shared configuration for request tests.
/// </summary>
[TestFixture]
internal abstract class TestBase
{
    protected static readonly string ClientId = Environment.GetEnvironmentVariable("CLIENT_ID");
    protected static readonly string ClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
    protected static readonly string TenantId = Environment.GetEnvironmentVariable("TENANT_ID");
    protected static readonly string Dynamics365Url = Environment.GetEnvironmentVariable("DYNAMICS365_URL");

    /// <summary>
    /// Loads test environment settings before the fixture runs.
    /// </summary>
    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        // load envs
        var root = Directory.GetCurrentDirectory();
        var projDir = Directory.GetParent(root)?.Parent?.Parent?.FullName;
        DotEnv.Load(
            options: new DotEnvOptions(
                envFilePaths: new[] { $"{projDir}{Path.DirectorySeparatorChar}.env.local" }));
    }
}
