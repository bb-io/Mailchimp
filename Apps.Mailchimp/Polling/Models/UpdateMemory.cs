namespace Apps.Mailchimp.Polling.Models;

public class UpdateMemory
{
    public List<CampaignUpdateEntity> Entities { get; set; } = new();
}

public class CampaignUpdateEntity
{
    public string CampaignId { get; set; } = default!;

    public string HtmlContentHash { get; set; } = default!;
}