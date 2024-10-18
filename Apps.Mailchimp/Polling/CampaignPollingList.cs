using Apps.Mailchimp.Api;
using Apps.Mailchimp.Invocables;
using Apps.Mailchimp.Models.Requests.Campaigns;
using Apps.Mailchimp.Models.Responses.Campaigns;
using Apps.Mailchimp.Models.Responses.Campaigns.Content;
using Apps.Mailchimp.Polling.Models;
using Apps.Mailchimp.Utils;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Microsoft.AspNetCore.WebUtilities;
using RestSharp;

namespace Apps.Mailchimp.Polling;

[PollingEventList]
public class CampaignPollingList(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [PollingEvent("On campaigns created", "Polling event. Triggered after specified time interval and returns new campaigns")]
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

        var campaigns = await GetCampaigns(new FilterCampaignRequest
            { SinceCreateTime = request.Memory.LastInteractionDate });

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

    [PollingEvent("On campaigns updated", "Polling event. Triggered after specified time interval and returns updated campaigns")]
    public async Task<PollingEventResponse<UpdateMemory, CampaignsResponse>> OnCampaignsUpdated(
        PollingEventRequest<UpdateMemory> request)
    {
        var campaigns = await GetCampaigns(new() { Count = 20, SortField = "create_time", SortDirection = "DESC" });
        var campaignsToUpdate = await GetCampaignsToUpdate(campaigns.Items);
        
        if (request.Memory is null)
        {
            return new()
            {
                FlyBird = false,
                Memory = new()
                {
                    Entities = campaignsToUpdate
                }
            };
        }

        var updatedCampaigns = campaignsToUpdate
            .Where(newCampaign => request.Memory.Entities
                .Any(oldCampaign => newCampaign.CampaignId == oldCampaign.CampaignId &&
                                    newCampaign.HtmlContentHash != oldCampaign.HtmlContentHash))
            .ToList();

        var campaignsToReturn = campaigns.Items
            .Where(campaign => updatedCampaigns.Any(updated => updated.CampaignId == campaign.Id))
            .ToList();
        
        return new()
        {
            FlyBird = campaignsToReturn.Any(),
            Result = new()
            {
                Items = campaignsToReturn,
                TotalItems = campaignsToReturn.Count
            },
            Memory = new()
            {
                Entities = campaignsToUpdate
            }
        };
    }

    private async Task<CampaignsResponse> GetCampaigns(FilterCampaignRequest filterRequest)
    {
        var requestUrl = BuildQueryString("/campaigns", filterRequest);
        var request = new ApiRequest(requestUrl, Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<CampaignsResponse>(request);
    }

    private async Task<List<CampaignUpdateEntity>> GetCampaignsToUpdate(List<CampaignResponse> campaigns)
    {
        var entities = new List<CampaignUpdateEntity>();
        foreach (var campaign in campaigns)
        {
            var requestUrl = $"/campaigns/{campaign.Id}/content";
            var request = new ApiRequest(requestUrl, Method.Get, Creds);
            var content = await Client.ExecuteWithErrorHandling<CampaignContentResponse>(request);
            content.InitializeFromVariateContents();

            var hashableContent = content.Html == null && content.PlainText == null
                ? string.Empty
                : $"{content.PlainText} {content.Html}";
            entities.Add(new()
            {
                CampaignId = campaign.Id,
                HtmlContentHash = HashHelper.ConvertStringToHash(hashableContent)
            });
        }

        return entities;
    }

    private string BuildQueryString(string url, FilterCampaignRequest request)
    {
        var dictionary = new Dictionary<string, string?>
        {
            { "before_create_time", request.BeforeCreateTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "since_create_time", request.SinceCreateTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "sort_field", request.SortField },
            { "sort_dir", request.SortDirection }
        };

        if (request.Count.HasValue)
        {
            dictionary.Add("count", request.Count.Value.ToString());
        }

        return QueryHelpers.AddQueryString(url, dictionary);
    }
}