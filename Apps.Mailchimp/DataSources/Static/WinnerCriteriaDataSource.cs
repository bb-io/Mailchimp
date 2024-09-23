using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Mailchimp.DataSources.Static;

public class WinnerCriteriaDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            { "opens", "Opens" },
            { "clicks", "Clicks" },
            { "manual", "Manual" },
            { "total_revenue", "Total revenue" }
        };
    }
}