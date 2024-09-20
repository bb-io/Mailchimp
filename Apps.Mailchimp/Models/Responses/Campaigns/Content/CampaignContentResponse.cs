namespace Apps.Mailchimp.Models.Responses.Campaigns.Content;

public class CampaignContentResponse
{
    public List<VariateContentResponse> VariateContents { get; set; } = new();
  
    public string PlainText { get; set; } = default!;

    public string Html { get; set; } = default!;

    public string ArchiveHtml { get; set; } = default!;
}