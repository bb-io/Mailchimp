using Apps.Mailchimp.Constants;
using Apps.Mailchimp.Models.Dtos;
using Apps.Mailchimp.Utils;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Mailchimp.Api;

public class ApiClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialProviders)
    : BlackBirdRestClient(new RestClientOptions { BaseUrl = authenticationCredentialProviders.GetUrl(), ThrowOnAnyError = false })
{
    protected override JsonSerializerSettings JsonSettings => JsonConfig.JsonSettings;
    
    protected override Exception ConfigureErrorException(RestResponse response)
    {
        var error = JsonConvert.DeserializeObject<ErrorDto>(response.Content!)!;
        return new PluginApplicationException(error.ToString());
    }
}