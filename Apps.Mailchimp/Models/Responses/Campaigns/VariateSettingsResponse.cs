using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Campaigns;

public class VariateSettingsResponse
{
    [Display("Winning combination ID")]
    public string WinningCombinationId { get; set; } = string.Empty;

    [Display("Winning campaign ID")]
    public string WinningCampaignId { get; set; } = string.Empty;

    [Display("Winner criteria")]
    public string WinnerCriteria { get; set; } = string.Empty;

    [Display("Wait time")]
    public int WaitTime { get; set; }

    [Display("Test size")]
    public double TestSize { get; set; }

    [Display("Subject lines")]
    public List<string> SubjectLines { get; set; } = new();

    [Display("Send times")]
    public List<string> SendTimes { get; set; } = new();

    [Display("From names")]
    public List<string> FromNames { get; set; } = new();
}
