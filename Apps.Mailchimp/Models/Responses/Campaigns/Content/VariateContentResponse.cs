using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Campaigns.Content;

public class VariateContentResponse
{
    [Display("Content label")]
    public string ContentLabel { get; set; } = default!;
    
    [Display("Plain text")]
    public string PlainText { get; set; } = default!;
    
    [Display("HTML content")]
    public string Html { get; set; } = default!;
}