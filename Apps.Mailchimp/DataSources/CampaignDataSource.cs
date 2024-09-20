﻿using Apps.Mailchimp.Actions;
using Apps.Mailchimp.Invocables;
using Apps.Mailchimp.Models.Responses.Campaigns;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Mailchimp.DataSources;

public class CampaignDataSource(InvocationContext invocationContext) : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var actions = new CampaignActions(InvocationContext);
        var campaigns = await actions.SearchCampaignsAsync(new());
        
        return campaigns.Items
            .Where(x => context.SearchString == null || BuildReadableName(x).Contains(context.SearchString))
            .ToDictionary(x => x.Id, BuildReadableName);
    }
    
    private string BuildReadableName(CampaignResponse response) =>
        $"{response.Id} [Status: {response.Status}] [Type: {response.CampaignType}]";
}