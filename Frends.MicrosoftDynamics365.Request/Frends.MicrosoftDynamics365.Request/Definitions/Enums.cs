namespace Frends.MicrosoftDynamics365.Request.Definitions;

// Self-explanatory enums
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1602 // Enumeration items should be documented
public enum Method
{
    GET,
    POST,
    PATCH,
    PUT,
    DELETE,
}

/// <summary>
/// Dynamics 365 environment type.
/// </summary>
public enum EnvironmentType
{
    /// <summary>
    /// Dataverse / CRM (uses /api/data/{version}/{path}).
    /// </summary>
    Dataverse,

    /// <summary>
    /// Finance and Operations (uses /data/{path}).
    /// </summary>
    FinanceAndOperations,
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1602 // Enumeration items should be documented