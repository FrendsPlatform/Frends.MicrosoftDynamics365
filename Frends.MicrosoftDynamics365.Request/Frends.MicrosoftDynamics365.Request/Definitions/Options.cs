namespace Frends.MicrosoftDynamics365.Request.Definitions;

using System.ComponentModel;

/// <summary>
/// Options class contains parameters that are optional.
/// </summary>
public class Options
{
    /// <summary>
    /// Dynamics API version to use.
    /// </summary>
    /// <example>v9.1 / v9.0 / v8.2 / other</example>
    [DefaultValue("v9.1")]
    public string ApiVersion { get; init; }

    /// <summary>
    /// Whether to throw an exception when the request fails.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; init; } = true;

    /// <summary>
    /// Overrides the error message when the request fails.
    /// </summary>
    /// <example>Dynamics 365 request failed</example>
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; init; } = string.Empty;
}
