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
    /// Throw exception on error response.
    /// </summary>
    /// <example>false</example>
    [DefaultValue(false)]
    public bool ThrowExceptionOnErrorResponse { get; init; }
}