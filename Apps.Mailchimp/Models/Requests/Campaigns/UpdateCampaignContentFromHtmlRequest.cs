using Apps.Mailchimp.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Mailchimp.Models.Requests.Campaigns;

public class UpdateCampaignContentFromHtmlRequest : CampaignIdentifier
{
    public FileReference File { get; set; } = default!;
}