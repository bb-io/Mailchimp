using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Mailchimp.DataSources.Static;

public class FrequencyDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "daily", "Daily" },
            { "weekly", "Weekly" },
            { "monthly", "Monthly" }
        };
    }
}