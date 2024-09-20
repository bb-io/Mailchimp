using Apps.Mailchimp.Api;
using Apps.Mailchimp.Invocables;
using Apps.Mailchimp.Models.Identifiers;
using Apps.Mailchimp.Models.Requests.Campaigns;
using Apps.Mailchimp.Models.Responses.Campaigns;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.AspNetCore.WebUtilities;
using RestSharp;

namespace Apps.Mailchimp.Actions;

[ActionList]
public class CampaignActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Search campaigns", Description = "Search campaigns by specified criteria")]
    public async Task<CampaignsResponse> SearchCampaignsAsync([ActionParameter] FilterCampaignRequest filterRequest)
    {
        var requestUrl = $"/campaigns";
        requestUrl = BuildQueryString(requestUrl, filterRequest);
        
        var allCampaigns = new List<CampaignResponse>();
        int offset = 0;
        int count = 100;

        CampaignsResponse response;
        do
        {
            var paginatedUrl = QueryHelpers.AddQueryString(requestUrl, new Dictionary<string, string?>
            {
                { "offset", offset.ToString() },
                { "count", count.ToString() }
            });

            var request = new ApiRequest(paginatedUrl, Method.Get, Creds);
            response = await Client.ExecuteWithErrorHandling<CampaignsResponse>(request);

            if (response.Items.Any())
            {
                allCampaigns.AddRange(response.Items);
            }

            offset += count;
        }
        while (response.Items.Count == count);

        return new()
        {
            Items = allCampaigns,
            TotalItems = allCampaigns.Count
        };
    }
    
    [Action("Get campaign", Description = "Get campaign by specified ID")]
    public async Task<CampaignResponse> GetCampaignAsync([ActionParameter] CampaignIdentifier identifier)
    {
        var requestUrl = $"/campaigns/{identifier}";
        var request = new ApiRequest(requestUrl, Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<CampaignResponse>(request);
    }
    
    [Action("Delete campaign", Description = "Delete campaign by specified ID")]
    public async Task DeleteCampaignAsync([ActionParameter] CampaignIdentifier identifier)
    {
        var requestUrl = $"/campaigns/{identifier}";
        var request = new ApiRequest(requestUrl, Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    private string BuildQueryString(string url, FilterCampaignRequest request)
    {
        var dictionary = new Dictionary<string, string?>
        {
            { "type", request.CampaignType },
            { "status", request.Status },
            { "before_send_time", request.BeforeSendTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "since_send_time", request.SinceSendTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "before_create_time", request.BeforeCreateTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "since_create_time", request.SinceCreateTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "list_id", request.ListId },
            { "folder_id", request.FolderId },
            { "member_id", request.MemberId }
        };
        
        return QueryHelpers.AddQueryString(url, dictionary);
    }
}