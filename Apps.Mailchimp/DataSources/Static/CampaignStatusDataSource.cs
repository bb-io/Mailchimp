using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Mailchimp.DataSources.Static;

public class CampaignStatusDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "save", "Save" },
            { "paused", "Paused" },
            { "schedule", "Schedule" },
            { "sending", "Sending" },
            { "send", "Send" }
        };
    }
}