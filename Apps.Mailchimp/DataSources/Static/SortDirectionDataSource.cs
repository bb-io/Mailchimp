using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Mailchimp.DataSources.Static;

public class SortDirectionDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            { "ASC", "Ascending" },
            { "DESC", "Descending" }
        };
    }
}