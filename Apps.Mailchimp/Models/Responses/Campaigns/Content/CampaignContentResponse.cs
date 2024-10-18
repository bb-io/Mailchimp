using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Campaigns.Content;

public class CampaignContentResponse
{
    [Display("Variate contents")]
    public List<VariateContentResponse> VariateContents { get; set; } = new();
  
    [Display("Plain text")]
    public string? PlainText { get; set; } = default!;

    [Display("HTML content")]
    public string? Html { get; set; } = default!;

    [Display("Archive HTML")]
    public string? ArchiveHtml { get; set; } = default!;
    
    public void InitializeFromVariateContents()
    {
        if (string.IsNullOrEmpty(PlainText) && VariateContents.Count == 0)
        {
            PlainText = VariateContents[0].PlainText;
        }
        
        if (string.IsNullOrEmpty(Html) && VariateContents.Count == 0)
        {
            Html = VariateContents[0].Html;
        }
    }
}