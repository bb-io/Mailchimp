using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Campaigns.Content;

public class CampaignContentResponse
{
    [DefinitionIgnore]
    public List<VariateContentResponse> VariateContents { get; set; } = new();
  
    [Display("Plain text")]
    public string? PlainText { get; set; } = default!;

    [Display("HTML content")]
    public string? Html { get; set; } = default!;

    [Display("Archive HTML")]
    public string? ArchiveHtml { get; set; } = default!;
}