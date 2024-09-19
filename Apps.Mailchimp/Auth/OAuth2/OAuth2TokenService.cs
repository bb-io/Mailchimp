using Apps.Mailchimp.Auth.OAuth2.Models;
using Apps.Mailchimp.Constants;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Mailchimp.Auth.OAuth2;

public class OAuth2TokenService(InvocationContext invocationContext)
    : BaseInvocable(invocationContext), IOAuth2TokenService
{
    public bool IsRefreshToken(Dictionary<string, string> values)
    {
        // Note: Mailchimp Marketing access tokens do not expire, so you don’t need to use a refresh_token.
        return false;
    }

    public Task<Dictionary<string, string>> RefreshToken(Dictionary<string, string> values, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Dictionary<string, string>> RequestToken(
        string state, 
        string code, 
        Dictionary<string, string> values, 
        CancellationToken cancellationToken)
    {
        var tokenUrl = "https://login.mailchimp.com/oauth2/token";
        var restRequest = new RestRequest(tokenUrl, Method.Post)
            .WithJsonBody(new
            {
                grant_type = "authorization_code",
                client_id = values["client_id"],
                client_secret = values["client_secret"],
                redirect_uri = InvocationContext.UriInfo.ImplicitGrantRedirectUri.ToString(),
                code
            });
        
        var client = new RestClient();
        var response = await client.ExecuteAsync(restRequest, cancellationToken);
        var tokenResponse = JsonConvert.DeserializeObject<OAuth2TokenResponse>(response.Content!)!;
        
        var metadataUrl = "https://login.mailchimp.com/oauth2/metadata";
        var metadataRequest = new RestRequest(metadataUrl)
            .AddHeader("Authorization", $"OAuth {tokenResponse.AccessToken}");
        
        var metadataResponse = await client.ExecuteAsync(metadataRequest, cancellationToken);
        var metadata = JsonConvert.DeserializeObject<OAuth2MetadataResponse>(metadataResponse.Content!)!;
        
        return new Dictionary<string, string> { { CredNames.AccessToken, tokenResponse.AccessToken }, { CredNames.ServerPrefix, metadata.Dc } };
    }

    public Task RevokeToken(Dictionary<string, string> values)
    {
        throw new NotImplementedException();
    }
}