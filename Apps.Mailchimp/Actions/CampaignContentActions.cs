using Apps.Mailchimp.Api;
using Apps.Mailchimp.Invocables;
using Apps.Mailchimp.Models.Identifiers;
using Apps.Mailchimp.Models.Requests.Campaigns;
using Apps.Mailchimp.Models.Responses.Campaigns.Content;
using Apps.Mailchimp.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.Mailchimp.Actions;

[ActionList]
public class CampaignContentActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Get campaign content as HTML", Description = "Get content of campaign by specified ID")]
    public async Task<FileReference> GetCampaignContentAsHtmlAsync([ActionParameter] CampaignIdentifier identifier)
    {
        var campaignContent = await GetCampaignContentAsync(identifier);
        var memoryStream = HtmlHelper.CampaignContentResponseToHtmlStream(campaignContent, identifier.CampaignId);
        return await fileManagementClient.UploadAsync(memoryStream, "text/html", $"{identifier.CampaignId}.html");
    }

    [Action("Update campaign content from HTML",
        Description = "Update content of campaign by specified ID from HTML file")]
    public async Task<CampaignContentResponse> UpdateCampaignContentFromHtmlAsync(
        [ActionParameter] UpdateCampaignContentFromHtmlRequest updateRequest)
    {
        var stream = await fileManagementClient.DownloadAsync(updateRequest.File);
        var htmlCampaignContent = await HtmlHelper.HtmlStreamToHtmlString(stream);
        var campaignId = updateRequest.CampaignId ?? htmlCampaignContent.CampaignId ??
            throw new InvalidOperationException(
                "Could not find campaign ID in HTML file and in the optional input. Specify campaign ID in the input");
        
        return await UpdateCampaignContentAsync(new UpdateCampaignContentRequest
        {
            CampaignId = campaignId,
            Html = htmlCampaignContent.Html
        });
    }

    [Action("Get campaign content", Description = "Get content of campaign by specified ID")]
    public async Task<CampaignContentResponse> GetCampaignContentAsync([ActionParameter] CampaignIdentifier identifier)
    {
        var requestUrl = $"/campaigns/{identifier.CampaignId}/content";
        var request = new ApiRequest(requestUrl, Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<CampaignContentResponse>(request);
    }

    [Action("Update campaign content", Description = "Update content of campaign by specified ID")]
    public async Task<CampaignContentResponse> UpdateCampaignContentAsync(
        [ActionParameter] UpdateCampaignContentRequest updateRequest)
    {
        var requestUrl = $"/campaigns/{updateRequest.CampaignId}/content";
        var request = new ApiRequest(requestUrl, Method.Put, Creds)
            .AddJsonBody(new { html = updateRequest.Html });
        return await Client.ExecuteWithErrorHandling<CampaignContentResponse>(request);
    }
}