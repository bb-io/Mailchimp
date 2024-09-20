using Apps.Mailchimp.Models.Responses.Campaigns.Content;

namespace Apps.Mailchimp.Utils;

public static class HtmlHelper
{
    public static MemoryStream CampaignContentResponseToHtmlStream(CampaignContentResponse campaignContent)
    {
        var htmlStream = new MemoryStream();
        var writer = new StreamWriter(htmlStream);
        writer.Write(campaignContent.Html);
        writer.Flush();
        htmlStream.Position = 0;
        
        return htmlStream;
    }
}