using Apps.Mailchimp.Constants;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.AspNetCore.WebUtilities;

namespace Apps.Mailchimp.Auth.OAuth2;

public class OAuth2AuthorizeService(InvocationContext invocationContext)
    : BaseInvocable(invocationContext), IOAuth2AuthorizeService
{
    public string GetAuthorizationUrl(Dictionary<string, string> values)
    {
        const string oauthUrl = "https://login.mailchimp.com/oauth2/authorize";
        var parameters = new Dictionary<string, string>
        {
            { "response_type", "code" },
            { "client_id", CredNames.ClientId },
            { "redirect_uri", InvocationContext.UriInfo.ImplicitGrantRedirectUri.ToString() },
            { "state", values["state"] }
        };
        
        return QueryHelpers.AddQueryString(oauthUrl, parameters!);
    }
}