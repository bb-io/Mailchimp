using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Mailchimp.Models.Responses.Campaigns;

public class CampaignResponse
{
    [Display("Campaign ID")] 
    public string Id { get; set; } = default!;
    
    [Display("Web ID")]
    public string WebId { get; set; } = default!;
    
    [Display("Parent campaign ID")]
    public string ParentCampaignId { get; set; } = default!;

    [Display("Campaign type"), JsonProperty("type")]
    public string CampaignType { get; set; } = default!;
    
    [Display("Creation time")]
    public DateTime CreateTime { get; set; }
    
    [Display("Archive URL")]
    public string ArchiveUrl { get; set; } = default!;
    
    [Display("Long archive URL")]
    public string LongArchiveUrl { get; set; } = default!;
    
    [Display("Status")]
    public string Status { get; set; } = default!;
    
    [Display("Emails sent")]
    public double EmailsSent { get; set; }
    
    [Display("Send time")]
    public DateTime SendTime { get; set; }
    
    [Display("Content type")]
    public string ContentType { get; set; } = default!;
    
    [Display("Needs block refresh")]
    public bool NeedsBlockRefresh { get; set; }
    
    [Display("Resendable")]
    public bool Resendable { get; set; }
    
    [Display("Settings")] 
    public CampaignSettingsResponse Settings { get; set; } = default!;
    
    [Display("Tracking")]
    public CampaignTrackingResponse Tracking { get; set; } = default!;
    
    [Display("Recipients")]
    public CampaignRecipientsResponse Recipients { get; set; } = default!;
}