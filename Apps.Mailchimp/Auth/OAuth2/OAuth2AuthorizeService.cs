﻿using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.AspNetCore.WebUtilities;

namespace Apps.Mailchimp.Auth.OAuth2;

public class OAuth2AuthorizeService(InvocationContext invocationContext)
    : BaseInvocable(invocationContext), IOAuth2AuthorizeService
{
    public string GetAuthorizationUrl(Dictionary<string, string> values)
    {
        var bridgeOauthUrl = $"{InvocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/oauth";
        
        var parameters = new Dictionary<string, string>
        {
            { "client_id", ApplicationConstants.ClientId },
            { "redirect_uri", $"{InvocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/AuthorizationCode" },
            { "actual_redirect_uri", InvocationContext.UriInfo.AuthorizationCodeRedirectUri.ToString() },
            { "authorization_url", "https://login.mailchimp.com/oauth2/authorize"},
            { "state", values["state"] },
            { "response_type", "code" },
        };
        
        return QueryHelpers.AddQueryString(bridgeOauthUrl, parameters!);
    }
}