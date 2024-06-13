using System;

namespace Frends.MicrosoftDynamics365.Request.Tests;

using Definitions;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

[TestFixture]
internal class UnitTests : TestsBase
{
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

    [Test]
    public async Task BadAuthentication_ReturnsError()
    {
        var input = new Input { ClientId = "bad", ClientSecret = "bad", };
        var options = GetOptions();

        var result = await MicrosoftDynamics365.Request(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.False);
        Assert.That(result.ErrorMessage, Is.Not.Null);
        Assert.That(result.Data, Is.Null);
    }

    [Test]
    public void BadAuthentication_ThrowsException()
    {
        var input = new Input { ClientId = "bad", ClientSecret = "bad", TenantId = TenantId };
        var options = GetOptions(true);

        var exception = Assert.ThrowsAsync<Exception>(async () =>
            await MicrosoftDynamics365.Request(input, options, CancellationToken.None));
        Assert.That(exception.Message, Does.Contain("Error acquiring access token."));
    }

    [Test]
    public async Task BadRequest_ReturnsError()
    {
        var input = GetInput("bad_request");
        var options = GetOptions(false);
        var result = await MicrosoftDynamics365.Request(input, options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(result.ErrorMessage, Does.Contain("Error calling Dynamics 365 API: Error retrieving bad_request. Status code: NotFound"));
        Assert.That(result.Data, Is.Null);
    }

    [Test]
    public void BadRequest_ThrowsException()
    {
        var input = GetInput("bad_request");
        var options = GetOptions(true);

        var exception = Assert.ThrowsAsync<Exception>(async () =>
            await MicrosoftDynamics365.Request(input, options, CancellationToken.None));
        Assert.That(exception.Message, Does.Contain("Error retrieving bad_request. Status code: NotFound"));
    }

    private Input GetInput(string path, Method method = Method.GET, string payload = null)
    {
        return new Input
        {
            Path = path,
            ClientId = ClientId,
            TenantId = TenantId,
            ClientSecret = ClientSecret,
            Dynamics365Url = Dynamics365Url,
            Method = method,
        };
    }

    private Options GetOptions(bool throwExceptionOnErrorResponse = false)
    {
        return new Options
        {
            ApiVersion = "v9.1",
            ThrowExceptionOnErrorResponse = throwExceptionOnErrorResponse,
        };
    }
}
