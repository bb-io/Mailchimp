using Apps.Mailchimp.Api;
using Apps.Mailchimp.Invocables;
using Apps.Mailchimp.Models.Requests.Campaigns;
using Apps.Mailchimp.Models.Responses.Campaigns;
using Apps.Mailchimp.Polling.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Microsoft.AspNetCore.WebUtilities;
using RestSharp;

namespace Apps.Mailchimp.Polling;

[PollingEventList]
public class CampaignPollingList(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [PollingEvent("On campaigns created", "Triggered when new campaigns are created")]
    public async Task<PollingEventResponse<DateMemory, CampaignsResponse>> OnCampaignsCreated(
        PollingEventRequest<DateMemory> request)
    {
        if (request.Memory is null)
        {
            return new()
            {
                FlyBird = false,
                Memory = new()
                {
                    LastInteractionDate = DateTime.UtcNow
                }
            };
        }
        
        var campaigns = await GetCampaigns(new FilterCampaignRequest { SinceCreateTime = request.Memory.LastInteractionDate });
        return new()
        {
            FlyBird = campaigns.Items.Any(),
            Result = campaigns,
            Memory = new()
            {
                LastInteractionDate = DateTime.UtcNow
            }
        };
    }
    
    private async Task<CampaignsResponse> GetCampaigns(FilterCampaignRequest filterRequest)
    {
        var requestUrl = BuildQueryString("/campaigns", filterRequest);
        var request = new ApiRequest(requestUrl, Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<CampaignsResponse>(request);
    }
    
    private string BuildQueryString(string url, FilterCampaignRequest request)
    {
        var dictionary = new Dictionary<string, string?>
        {
            { "before_create_time", request.BeforeCreateTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "since_create_time", request.SinceCreateTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
        };

        return QueryHelpers.AddQueryString(url, dictionary);
    }
}