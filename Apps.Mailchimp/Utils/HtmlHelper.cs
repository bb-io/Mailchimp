﻿using Apps.Mailchimp.Models.Responses.Campaigns.Content;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;

namespace Apps.Mailchimp.Utils;

public static class HtmlHelper
{
    public static MemoryStream CampaignContentResponseToHtmlStream(CampaignContentResponse campaignContent, string campaignId)
    {
        var htmlStream = new MemoryStream();
        var writer = new StreamWriter(htmlStream);

        var metadataTag = $"<meta name=\"blackbird-campaign-id\" content=\"{campaignId}\">";
        var modifiedHtml = campaignContent.Html.Replace("</head>", $"{metadataTag}</head>");

        writer.Write(modifiedHtml);
        writer.Flush();
        htmlStream.Position = 0;

        return htmlStream;
    }
    
    public static async Task<HtmlCampaignContent> HtmlStreamToHtmlString(Stream stream)
    {
        var bytes = await stream.GetByteData();
        var html = System.Text.Encoding.UTF8.GetString(bytes);

        var campaignId = string.Empty;
        var metaTagPattern = "<meta name=\"blackbird-campaign-id\" content=\"(.*?)\">";
        var match = System.Text.RegularExpressions.Regex.Match(html, metaTagPattern);

        if (match.Success)
        {
            campaignId = match.Groups[1].Value;
        }

        return new HtmlCampaignContent(html, campaignId);
    }
}

public record HtmlCampaignContent(string Html, string? CampaignId);