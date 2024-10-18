using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Campaigns;

public class SocialCardResponse
{
    [Display("Image URL")]
    public string ImageUrl { get; set; } = string.Empty;
    
    [Display("Description")]
    public string Description { get; set; } = string.Empty;
    
    [Display("Title")]
    public string Title { get; set; } = string.Empty;
}