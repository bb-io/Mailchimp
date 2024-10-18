using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Campaigns;

public class RssOptsResponse
{
    [Display("Feed URL")]
    public string FeedUrl { get; set; } = string.Empty;

    [Display("Frequency")]
    public string Frequency { get; set; } = string.Empty;

    [Display("Schedule")]
    public RssScheduleResponse Schedule { get; set; } = new();

    [Display("Last sent")]
    public string LastSent { get; set; } = string.Empty;

    [Display("Constrain RSS image")]
    public bool ConstrainRssImg { get; set; }
}

public class RssScheduleResponse
{
    [Display("Hour")]
    public int Hour { get; set; }

    [Display("Weekly send day")]
    public string WeeklySendDay { get; set; } = string.Empty;

    [Display("Monthly send date")]
    public int MonthlySendDate { get; set; }
}