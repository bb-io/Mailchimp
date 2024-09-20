using Apps.Mailchimp.Api;
using Apps.Mailchimp.Invocables;
using Apps.Mailchimp.Models.Identifiers;
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
public class CampaignContentActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    [Action("Get campaign content as HTML", Description = "Get content of campaign by specified ID")]
    public async Task<FileReference> GetCampaignContentAsync([ActionParameter] CampaignIdentifier identifier)
    {
        var requestUrl = $"/campaigns/{identifier.CampaignId}/content";
        var request = new ApiRequest(requestUrl, Method.Get, Creds);
        var campaignContent = await Client.ExecuteWithErrorHandling<CampaignContentResponse>(request);
        var memoryStream = HtmlHelper.CampaignContentResponseToHtmlStream(campaignContent);
        return await fileManagementClient.UploadAsync(memoryStream, "text/html", $"{identifier.CampaignId}.html");
    }
}