using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Mailchimp.Models.Responses.Campaigns;

public class CampaignsResponse : BaseSearchResponse<CampaignResponse>
{
    [Display("Campaigns"), JsonProperty("campaigns")]
    public override List<CampaignResponse> Items { get; set; } = new();
}