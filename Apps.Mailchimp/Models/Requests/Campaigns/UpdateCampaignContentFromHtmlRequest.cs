using Apps.Mailchimp.DataSources;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Mailchimp.Models.Requests.Campaigns;

public class UpdateCampaignContentFromHtmlRequest
{
    [Display("Campaign ID", Description = "The unique ID for the campaign."), DataSource(typeof(CampaignDataSource))]
    public string? CampaignId { get; set; } = default!;
    
    public FileReference File { get; set; } = default!;
}