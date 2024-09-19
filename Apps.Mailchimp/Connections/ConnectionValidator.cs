﻿using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.Mailchimp.Connections;

public class ConnectionValidator: IConnectionValidator
{
    public ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult<ConnectionValidationResponse>(new()
        {
            IsValid = true
        });
    }
}