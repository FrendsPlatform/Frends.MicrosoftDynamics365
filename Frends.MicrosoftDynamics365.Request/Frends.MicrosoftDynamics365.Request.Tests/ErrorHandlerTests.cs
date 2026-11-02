namespace Frends.MicrosoftDynamics365.Request.Tests;

using System;
using Frends.MicrosoftDynamics365.Request.Definitions;
using Frends.MicrosoftDynamics365.Request.Helpers;
using NUnit.Framework;

[TestFixture]
internal class ErrorHandlerTests
{
    /// <summary>
    /// Verifies that failures are returned with structured error details when throwing is disabled.
    /// </summary>
    [Test]
    public void Handle_ReturnsErrorDetailsWhenConfiguredNotToThrow()
    {
        var exception = new InvalidOperationException("Request failed.");
        var result = exception.Handle(new Options { ThrowExceptionOnErrorResponse = false });

        Assert.That(result.Success, Is.False);
        Assert.That(result.Data, Is.Null);
        Assert.That(result.Error.Message, Is.EqualTo("Request failed."));
        Assert.That(result.Error.AdditionalInfo, Is.SameAs(exception));
        Assert.That(result.ErrorMessage, Is.EqualTo("Error calling Dynamics 365 API: Request failed."));
    }

    /// <summary>
    /// Verifies that failures throw by default.
    /// </summary>
    [Test]
    public void Handle_ThrowsOriginalExceptionWhenConfiguredToThrow()
    {
        var exception = new InvalidOperationException("Request failed.");

        var thrown = Assert.Throws<InvalidOperationException>(() =>
            exception.Handle(new Options()));

        Assert.That(thrown, Is.SameAs(exception));
    }

    /// <summary>
    /// Verifies that the configured message is added to the structured error.
    /// </summary>
    [Test]
    public void Handle_PrependsConfiguredFailureMessage()
    {
        var result = new InvalidOperationException("Request failed.").Handle(new Options
        {
            ErrorMessageOnFailure = "Dynamics 365 request failed",
            ThrowErrorOnFailure = false,
        });

        Assert.That(result.Error.Message, Is.EqualTo("Dynamics 365 request failed: Request failed."));
    }
}
