using Apps.Mailchimp.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Mailchimp.Actions;

[ActionList]
public class DebugActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("[DEBUG] Get credential providers", Description = "Debug action.")]
    public List<AuthenticationCredentialsProvider> GetCredentialProviders()
    {
        return Creds.ToList();
    }
}