using Apps.Mailchimp.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Mailchimp.Actions;

[ActionList]
public class DebugActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("[DEBUG] Get authentication credential providers", Description = "Can be used only in development environment")]
    public List<AuthenticationCredentialsProvider> GetAuthenticationCredentialProviders()
    {
        return InvocationContext.AuthenticationCredentialsProviders.ToList();
    }
}