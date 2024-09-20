using Apps.Mailchimp.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Mailchimp.Invocables;

public class AppInvocable : BaseInvocable
{
    public AppInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new ApiClient(Creds);
    }
    
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected ApiClient Client { get; }
}