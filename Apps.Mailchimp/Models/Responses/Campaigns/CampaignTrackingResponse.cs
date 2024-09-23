using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Campaigns;

public class CampaignTrackingResponse
{
    [Display("Opens")]
    public bool Opens { get; set; }
    
    [Display("HTML clicks")]
    public bool HtmlClicks { get; set; }
    
    [Display("Text clicks")]
    public bool TextClicks { get; set; }
    
    [Display("Goal tracking")]
    public bool GoalTracking { get; set; }
    
    [Display("Ecomm 360")]
    public bool Ecomm360 { get; set; }
    
    [Display("Google analytics")]
    public string GoogleAnalytics { get; set; } = default!;
    
    [Display("Clicktale")]
    public string Clicktale { get; set; } = default!;
}