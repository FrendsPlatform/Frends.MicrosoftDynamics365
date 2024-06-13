namespace Frends.MicrosoftDynamics365.Request.Definitions;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Input class contains parameters that are required.
/// </summary>
public class Input
{
    /// <summary>
    /// Azure AD app registration client ID.
    /// </summary>
    /// <example>9136e6a2-83e5-46ba-b104-d988a169dc24</example>
    public string ClientId { get; init; }

    /// <summary>
    /// Azure AD tenant ID.
    /// </summary>
    /// <example>9136e6a2-83e5-46ba-b104-d988a169dc24</example>
    public string TenantId { get; init; }

    /// <summary>
    /// App registration client secret.
    /// </summary>
    /// <example>my_secret</example>
    [PasswordPropertyText]
    public string ClientSecret { get; init; }

    /// <summary>
    /// Dynamics 365 URL.
    /// </summary>
    /// <example>https://my-org.crm4.dynamics.com</example>
    [DefaultValue("https://[MY_ORG].crm4.dynamics.com")]
    public string Dynamics365Url { get; init; }

    /// <summary>
    /// Dynamics 365 URL.
    /// </summary>
    /// <example>GET</example>
    [DefaultValue(Method.GET)]
    public Method Method { get; init; }

    /// <summary>
    /// Request path for the API call.
    /// </summary>
    /// <example>
    /// accounts
    /// accounts({{account_id}})
    /// accounts?$select=name,revenue
    /// accounts?$filter=statecode eq 0
    /// </example>
    public string Path { get; init; }

    /// <summary>
    /// Request payload.
    /// </summary>
    /// <example>{
    ///     "firstname": "John",
    ///     "lastname": "Doe",
    ///     "emailaddress1": "john.doe@example.com",
    ///     "telephone1": "123-456-7890"
    /// }</example>
    [DefaultValue("")]
    [UIHint(nameof(Method), "", Method.POST, Method.PUT, Method.PATCH)]
    public string Payload { get; init; }
}