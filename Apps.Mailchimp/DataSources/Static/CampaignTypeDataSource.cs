﻿using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Mailchimp.DataSources.Static;

public class CampaignTypeDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "regular", "Regular" },
            { "plaintext", "Plain Text" },
            { "rss", "RSS" },
            { "variate", "Variate" }
        };
    }
}