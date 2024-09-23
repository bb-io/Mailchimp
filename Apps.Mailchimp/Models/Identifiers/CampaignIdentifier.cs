using Apps.Mailchimp.DataSources;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Mailchimp.Models.Identifiers;

public class CampaignIdentifier
{
    [Display("Campaign ID", Description = "The unique ID for the campaign."), DataSource(typeof(CampaignDataSource))]
    public string CampaignId { get; set; } = default!;
    
    public override string ToString() => CampaignId;
}