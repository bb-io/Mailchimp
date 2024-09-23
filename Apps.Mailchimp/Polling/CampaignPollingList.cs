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

    [PollingEvent("On campaigns updated", "Triggered when campaigns are updated")]
    public async Task<PollingEventResponse<UpdateMemory, CampaignsResponse>> OnCampaignsUpdated(
        PollingEventRequest<UpdateMemory> request)
    {
        try
        {
            var campaigns = await GetCampaigns(new() { Count = 20, SortField = "create_time", SortDirection = "DESC" });
            var campaignsToUpdate = await GetCampaignsToUpdate(campaigns.Items);

            await WebhookLogger.LogAsync(new
            {
                Message = "Polling campaigns to update",
                campaigns,
                campaignsToUpdate,
                request.Memory
            });

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
                .Where(x => request.Memory.Entities.Any(y =>
                    x.CampaignId == y.CampaignId))
                .Where(x => request.Memory.Entities.Any(y => x.HtmlContentHash != y.HtmlContentHash));

            var campaignsToReturn = campaigns.Items
                .Where(x => updatedCampaigns.Any(y => y.CampaignId == x.Id))
                .ToList();

            await WebhookLogger.LogAsync(new
            {
                Message = "Updated campaigns",
                updatedCampaigns,
                campaignsToReturn
            });

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
        catch (Exception e)
        {
            await WebhookLogger.LogAsync(e);
            throw;
        }
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

            entities.Add(new()
            {
                CampaignId = campaign.Id,
                HtmlContentHash = HashHelper.ConvertStringToHash(content.Html)
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