﻿namespace Frends.MicrosoftDynamics365.Request.Definitions;

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
    /// <example>
    /// {
    ///     "@odata.context": "https://[domain].dynamics.com/api/data/v9.1/$metadata#accounts",
    ///     "value": [
    ///     {
    ///         "@odata.etag": "W/\"5089461\"",
    ///         "address1_composite": "6789 Edwards Ave.\r\nLynnwood, Tennessee 37010\r\nUnited States",
    ///         "websiteurl": "http://www.example.com",
    ///         "name": "Fabrikam, Inc."
    ///     },
    ///     {
    ///         "@odata.etag": "W/\"5089463\"",
    ///         "address1_composite": "789 3rd St\r\nSan Francisco, California 94158\r\nUnited States",
    ///         "websiteurl": "http://www.example.com",
    ///         "name": "Trey Research"
    ///     }
    ///     ]
    /// }
    /// </example>
    public dynamic Data { get; init; }

    /// <summary>
    /// Indicates whether the call to Dynamics 365 was successful. If the call failed and the task is configured to
    /// not throw an exception, this will be false.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; init; }

    /// <summary>
    /// Error message if the call to Dynamics 365 failed and the task is configured to not throw an exception.
    /// </summary>
    /// <example>Something went wrong.</example>
    public string ErrorMessage { get; init; }
}
