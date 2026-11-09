namespace Frends.MicrosoftDynamics365.Request.Tests;

using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.MicrosoftDynamics365.Request.Definitions;
using NUnit.Framework;

[TestFixture]
internal class UnitTests : TestBase
{
    /// <summary>
    /// Verifies that an account request returns account data.
    /// </summary>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    public async Task GetAccounts_ReturnsAccounts()
    {
        var input = GetInput("accounts");
        var options = GetOptions();

        var result = await MicrosoftDynamics365.Request(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
        Assert.That(result.ErrorMessage, Is.Null);
        Assert.That(result.Data.value.Count, Is.GreaterThan(0));
    }

    /// <summary>
    /// Verifies that a PATCH request updates an account.
    /// </summary>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    public async Task PatchAccount_UpdatesAccount()
    {
        // Get accounts and save the ID of the first one
        var input = GetInput("accounts");
        var options = GetOptions();

        var result = await MicrosoftDynamics365.Request(input, options, CancellationToken.None);
        var accountid = result.Data.value[0].accountid;

        // Update the description of the first returned account with a PATCH
        input = GetInput($"accounts({accountid})", Method.PATCH, "{\r\n\"description\": \"Updated description.\"\r\n}");
        result = await MicrosoftDynamics365.Request(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
        Assert.That(result.ErrorMessage, Is.Null);

        // Get account description and assert that it has been updated
        input = GetInput($"accounts({accountid})?$select=description");
        result = await MicrosoftDynamics365.Request(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
        Assert.That(result.ErrorMessage, Is.Null);
        Assert.AreEqual(result.Data.description.ToString(), "Updated description.");
    }

    /// <summary>
    /// Verifies that authentication failures are returned when throwing is disabled.
    /// </summary>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    public async Task BadAuthentication_ReturnsError()
    {
        var input = new Input
        {
            ClientId = "bad",
            ClientSecret = "bad",
        };
        var options = GetOptions();

        var result = await MicrosoftDynamics365.Request(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.False);
        Assert.That(result.ErrorMessage, Is.Not.Null);
        Assert.That(result.Data, Is.Null);
    }

    /// <summary>
    /// Verifies that authentication failures are thrown when configured.
    /// </summary>
    [Test]
    public void BadAuthentication_ThrowsException()
    {
        var input = new Input
        {
            ClientId = "bad",
            ClientSecret = "bad",
            TenantId = TenantId,
        };
        var options = GetOptions(true);

        var exception = Assert.ThrowsAsync<Exception>(async () =>
            await MicrosoftDynamics365.Request(input, options, CancellationToken.None));
        Assert.That(exception.Message, Does.Contain("Error acquiring access token."));
    }

    /// <summary>
    /// Verifies that failed requests return an error result.
    /// </summary>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    public async Task BadRequest_ReturnsError()
    {
        var input = GetInput("bad_request");
        var options = GetOptions(false);
        var result = await MicrosoftDynamics365.Request(input, options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(
            result.ErrorMessage,
            Does.Contain("Error calling Dynamics 365 API: Error retrieving bad_request. Status code: NotFound"));
        Assert.That(result.Data, Is.Null);
    }

    /// <summary>
    /// Verifies that failed requests throw when configured.
    /// </summary>
    [Test]
    public void BadRequest_ThrowsException()
    {
        var input = GetInput("bad_request");
        var options = GetOptions(true);

        var exception = Assert.ThrowsAsync<Exception>(async () =>
            await MicrosoftDynamics365.Request(input, options, CancellationToken.None));
        Assert.That(exception.Message, Does.Contain("Error retrieving bad_request. Status code: NotFound"));
    }

    private static Input GetInput(string path, Method method = Method.GET, string payload = null) =>
        new()
        {
            Path = path,
            Payload = payload,
            ClientId = ClientId,
            TenantId = TenantId,
            ClientSecret = ClientSecret,
            Dynamics365Url = Dynamics365Url,
            Method = method,
        };

    private static Options GetOptions(bool throwException = false) =>
        new()
        {
            ApiVersion = "v9.1",
            ThrowErrorOnFailure = throwException,
        };
}
