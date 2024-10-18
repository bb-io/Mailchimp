using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Mailchimp.DataSources.Static;

public class SortFieldDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "create_time", "Create time" },
            { "send_time", "Send time" }
        };
    }
}