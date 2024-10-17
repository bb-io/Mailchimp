using Apps.Mailchimp.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Mailchimp.Actions;

[ActionList]
public class DebugActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("[DEBUG] Get access token", Description = "Get the access token for the Mailchimp API. Can be used to test the connection.")]
    public List<AuthenticationCredentialsProvider> GetAccessToken()
    {
        return Creds.ToList();
    }
}