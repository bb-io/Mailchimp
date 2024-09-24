using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Campaigns;

public class CampaignRecipientsResponse
{
    [Display("List ID")]
    public string ListId { get; set; } = default!;

    [Display("List is active")]
    public bool ListIsActive { get; set; }

    [Display("List name")]
    public string ListName { get; set; } = default!;

    [Display("Segment text")]
    public string SegmentText { get; set; } = default!;

    [Display("Recipient count")]
    public double RecipientCount { get; set; }
}