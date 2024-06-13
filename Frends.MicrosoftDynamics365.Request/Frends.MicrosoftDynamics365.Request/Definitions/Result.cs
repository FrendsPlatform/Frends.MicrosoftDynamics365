namespace Frends.MicrosoftDynamics365.Request.Definitions;

/// <summary>
/// Contains properties of the return object.
/// </summary>
public class Result
{
    internal Result()
    {
    }

    /// <summary>
    /// Contains the input repeated the specified number of times.
    /// </summary>
    /// <example>Example of the output</example>
    public dynamic Data { get; init; }

    /// <summary>
    /// Indicates whether the call to Dynamics 365 was successful. If the call failed and the task is configured to
    /// not throw an exception, this will be false.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Error message if the call to Dynamics 365 failed and the task is configured to not throw an exception.
    /// </summary>
    /// <example>Something went wrong.</example>
    public string ErrorMessage { get; set; }
}
