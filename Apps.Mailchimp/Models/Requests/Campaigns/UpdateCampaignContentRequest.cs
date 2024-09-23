using Apps.Mailchimp.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Requests.Campaigns;

public class UpdateCampaignContentRequest : CampaignIdentifier
{
    [Display("HTML content")] 
    public string Html { get; set; } = default!;
}