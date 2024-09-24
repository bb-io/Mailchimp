using Apps.Mailchimp.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.Mailchimp.Utils;

public static class AuthenticationCredentialsProviderExtensions
{
    public static Uri GetUrl(this IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var serverPrefix = creds.Get(CredNames.ServerPrefix).Value;
        return new Uri($"https://{serverPrefix}.api.mailchimp.com/3.0");
    }
}