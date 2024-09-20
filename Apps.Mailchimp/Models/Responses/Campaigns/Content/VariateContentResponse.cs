namespace Apps.Mailchimp.Models.Responses.Campaigns.Content;

public class VariateContentResponse
{
    public string ContentLabel { get; set; } = default!;
    
    public string PlainText { get; set; } = default!;
    
    public string Html { get; set; } = default!;
}