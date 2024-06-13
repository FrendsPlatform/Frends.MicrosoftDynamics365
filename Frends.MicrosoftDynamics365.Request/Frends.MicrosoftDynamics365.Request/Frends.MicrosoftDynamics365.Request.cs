using System.IO.Enumeration;

namespace Frends.MicrosoftDynamics365.Request;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Threading;
using Definitions;

/// <summary>
/// Main class of the Task.
/// </summary>
public static class MicrosoftDynamics365
{
    /// <summary>
    /// This is Task.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.MicrosoftDynamics365.Request).
    /// </summary>
    /// <param name="input">What to repeat.</param>
    /// <param name="options">Define if repeated multiple times. </param>
    /// <param name="cancellationToken">Cancellation token given by Frends.</param>
    /// <returns>Object { dynamic Data, bool Success, string ErrorMessage }.</returns>
    public static async Task<Result> Request([PropertyTab] Input input, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        try
        {
            var token = await GetAccessTokenAsync(input, cancellationToken);
            var data = await ExecuteRequestAsync(input, options, token, cancellationToken);
            return new Result { Data = data, Success = true };
        }
        catch (Exception ex)
        {
            if (options.ThrowExceptionOnErrorResponse) throw;

            var errorMessage = "Error calling Dynamics 365 API: " + ex.Message;
            if (ex.InnerException != null)
                errorMessage += "\r\nInner exception: " + ex.InnerException.Message;

            return new Result
            {
                Data = null,
                ErrorMessage = errorMessage,
                Success = false,
            };
        }
    }

    private static async Task<string> GetAccessTokenAsync(Input input, CancellationToken cancellationToken)
    {
        var confidentialClient = ConfidentialClientApplicationBuilder.Create(input.ClientId)
            .WithClientSecret(input.ClientSecret)
            .WithAuthority(new Uri($"https://login.microsoftonline.com/{input.TenantId}"))
            .Build();

        var scopes = new[] { $"{input.Dynamics365Url}/.default" };

        try
        {
            var result = await confidentialClient.AcquireTokenForClient(scopes).ExecuteAsync(cancellationToken);
            return result.AccessToken;
        }
        catch (MsalServiceException ex)
        {
            throw new Exception($"Error acquiring access token.", ex);
        }
    }

    private static async Task<JToken> ExecuteRequestAsync(Input input, Options options, string token, CancellationToken cancellationToken)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new HttpRequestMessage
        {
            Method = new HttpMethod(input.Method.ToString()),
            RequestUri = new Uri($"{input.Dynamics365Url}/api/data/{options.ApiVersion}/{input.Path}"),
        };

        if (input.Method == Method.POST || input.Method == Method.PUT)
        {
            request.Content = new StringContent(input.Payload ?? string.Empty, System.Text.Encoding.UTF8, "application/json");
        }

        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error retrieving {input.Path}. Status code: " + response.StatusCode);

        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        var contacts = JToken.Parse(jsonResponse);
        return contacts;
    }
}
