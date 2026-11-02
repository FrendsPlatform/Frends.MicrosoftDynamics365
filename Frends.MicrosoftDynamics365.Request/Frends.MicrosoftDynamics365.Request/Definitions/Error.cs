namespace Frends.MicrosoftDynamics365.Request.Definitions;

using System;

/// <summary>
/// Error that occurred during the task.
/// </summary>
public class Error
{
    /// <summary>
    /// Summary of the error.
    /// </summary>
    /// <example>Dynamics 365 request failed.</example>
    public string Message { get; init; }

    /// <summary>
    /// Additional details about the error.
    /// </summary>
    /// <example>Exception details for the failed Dynamics 365 request.</example>
    public Exception AdditionalInfo { get; init; }
}
