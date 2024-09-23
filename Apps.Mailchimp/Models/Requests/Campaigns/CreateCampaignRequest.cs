using Apps.Mailchimp.DataSources;
using Apps.Mailchimp.DataSources.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Mailchimp.Models.Requests.Campaigns;

public class CreateCampaignRequest
{
    [Display("Campaign type", Description = "There are four types of campaigns you can create in Mailchimp. A/B Split campaigns have been deprecated and variate campaigns should be used instead. Possible values: regular, plaintext, absplit, rss, or variate."), StaticDataSource(typeof(CampaignTypeDataSource))]
    public string CampaignType { get; set; } = default!;

    [Display("Content type", Description = "How the campaign's content is put together. The old drag and drop editor uses 'template' while the new editor uses 'multichannel'. Defaults to template. Possible values: template or multichannel."), StaticDataSource(typeof(ContentTypeDataSource))]
    public string? ContentType { get; set; }
    
    // Settings

    [Display("Subject line", Description = "The subject line for the campaign.")] 
    public string? SubjectLine { get; set; }

    [Display("Preview text", Description = "The preview text for the campaign.")] 
    public string? PreviewText { get; set; }

    [Display("Title", Description = "The title of the campaign.")] 
    public string? Title { get; set; }

    [Display("From name", Description = "The 'from' name on the campaign (not an email address).")] 
    public string? FromName { get; set; }

    [Display("Reply to", Description = "The reply-to email address for the campaign. Note: while this field is not required for campaign creation, it is required for sending.")] 
    public string? ReplyTo { get; set; }

    [Display("Use conversation", Description = "Use Mailchimp Conversation feature to manage out-of-office replies.")] 
    public bool? UseConversation { get; set; }

    [Display("To name", Description = "Use Mailchimp Conversation feature to manage out-of-office replies.")] 
    public string? ToName { get; set; }

    [Display("Folder ID", Description = "If the campaign is listed in a folder, the id for that folder."), DataSource(typeof(FolderDataSource))] 
    public string? FolderId { get; set; }

    [Display("Authenticate", Description = "Whether Mailchimp authenticated the campaign. Defaults to true.")] 
    public bool? Authenticate { get; set; }

    [Display("Auto footer", Description = "Automatically append Mailchimp's default footer to the campaign.")] 
    public bool? AutoFooter { get; set; }

    [Display("Inline CSS", Description = "Automatically inline the CSS included with the campaign content.")] 
    public bool? InlineCss { get; set; }

    [Display("Auto tweet", Description = "Automatically tweet a link to the campaign archive page when the campaign is sent.")] 
    public bool? AutoTweet { get; set; }

    [Display("Facebook comments", Description = "Allows Facebook comments on the campaign (also force-enables the Campaign Archive toolbar). Defaults to true.")] 
    public bool? FbComments { get; set; }

    [Display("Timewarp", Description = "Schedule the campaign based on the recipient's timezone.")] 
    public bool? Timewarp { get; set; }

    [Display("Template ID", Description = "The id of the template to use."), DataSource(typeof(TemplateDataSource))] 
    public string? TemplateId { get; set; }

    [Display("Drag and drop", Description = "Whether to enable the drag-and-drop editor. Defaults to false.")] 
    public bool? DragAndDrop { get; set; }

    // Recipients
    
    [Display("Recipient ID", Description = "The list ID of the list to send this campaign to."), DataSource(typeof(ListDataSource))]
    public string? ListId { get; set; } = default!;
    
    // Tracking
    
    [Display("Opens", Description = "Whether to track opens. Defaults to true. Cannot be set to false for variate campaigns.")] 
    public bool? Opens { get; set; }
    
    [Display("HTML clicks", Description = "Whether to track clicks in the HTML version of the campaign. Defaults to true. Cannot be set to false for variate campaigns.")] 
    public bool? HtmlClicks { get; set; }
    
    [Display("Text clicks", Description = "Whether to track clicks in the plain-text version of the campaign. Defaults to true. Cannot be set to false for variate campaigns.")] 
    public bool? TextClicks { get; set; }
    
    [Display("Goal tracking", Description = "Deprecated.")] 
    public bool? GoalTracking { get; set; }
    
    [Display("Ecomm 360", Description = "Whether to enable e-commerce tracking.")] 
    public bool? Ecomm360 { get; set; }
    
    [Display("Google analytics", Description = "The custom slug for Google Analytics tracking (max of 50 bytes).")] 
    public string? GoogleAnalytics { get; set; }
    
    [Display("Clicktale", Description = "The custom slug for ClickTale tracking (max of 50 bytes).")] 
    public string? Clicktale { get; set; }
    
    // Variate settings
    
    [Display("Wait time", Description = "The number of minutes to wait before choosing the winning campaign. The value of wait_time must be greater than 0 and in whole hours, specified in minutes.")]
    public int? WaitTime { get; set; }
    
    [Display("Test size", Description = "The percentage of recipients to send the test combinations to, must be a value between 10 and 100.")]
    public int? TestSize { get; set; }
    
    [Display("Subject lines", Description = "The possible subject lines to test. If no subject lines are provided, settings.subject_line will be used.")]
    public Dictionary<string, string>? SubjectLines { get; set; }
    
    [Display("Send times", Description = "The possible send times to test. The times provided should be in the format YYYY-MM-DD HH:MM:SS. If send_times are provided to test, the test_size will be set to 100% and winner_criteria will be ignored.")]
    public IEnumerable<DateTime>? SendTimes { get; set; }
    
    [Display("From names", Description = "The possible from names. The number of from_names provided must match the number of reply_to_addresses. If no from_names are provided, settings.from_name will be used.")]
    public IEnumerable<string>? FromNames { get; set; }
    
    [Display("Reply to addresses", Description = "The possible reply-to addresses. The number of reply_to_addresses provided must match the number of from_names. If no reply_to_addresses are provided, settings.reply_to will be used.")]
    public IEnumerable<string>? ReplyToAddresses { get; set; }
    
    [Display("Winner criteria", Description = "The combination that performs the best. This may be determined automatically by click rate, open rate, or total revenue -- or you may choose manually based on the reporting data you find the most valuable. For Multivariate Campaigns testing send_time, winner_criteria is ignored. For Multivariate Campaigns with 'manual' as the winner_criteria, the winner must be chosen in the Mailchimp web application. Possible values: opens, clicks, manual, or total_revenue."), StaticDataSource(typeof(WinnerCriteriaDataSource))]
    public string? WinnerCriteria { get; set; }
    
    // Social card

    [Display("Image URL", Description = "The url for the header image for the card.")]
    public string? ImageUrl { get; set; }
    
    [Display("Description", Description = "The description for the card.")]
    public string? Description { get; set; }
    
    [Display("Social card title", Description = "The title for the card.")]
    public string? SocialTitle { get; set; }
    
    // Rss options
    
    [Display("Feed URL", Description = "The URL for the RSS feed.")]
    public string? FeedUrl { get; set; }
    
    [Display("Frequency", Description = "How often the campaign should run. Possible values: daily, weekly, monthly."), StaticDataSource(typeof(FrequencyDataSource))]
    public string? Frequency { get; set; }
    
    [Display("Constrain RSS image", Description = "Whether to add CSS to images in the RSS feed to constrain their width in campaigns.")]
    public bool? ConstrainRssImage { get; set; }
    
    public bool ShouldCreateSettings()
    {
        return SubjectLine != null || PreviewText != null || Title != null || FromName != null || ReplyTo != null ||
               UseConversation != null || ToName != null || FolderId != null || Authenticate != null ||
               AutoFooter != null || InlineCss != null || AutoTweet != null || FbComments != null || Timewarp != null ||
               TemplateId != null || DragAndDrop != null;    
    }
    
    public bool ShouldCreateRecipients()
    {
        return ListId != null;
    }
    
    public bool ShouldCreateTracking()
    {
        return Opens != null || HtmlClicks != null || TextClicks != null || GoalTracking != null || Ecomm360 != null ||
               GoogleAnalytics != null || Clicktale != null;
    }
    
    public bool ShouldCreateVariateSettings()
    {
        return WaitTime != null || TestSize != null || SubjectLines != null || SendTimes != null || FromNames != null ||
               ReplyToAddresses != null || WinnerCriteria != null;
    }
    
    public bool ShouldCreateSocialCard()
    {
        return ImageUrl != null || Description != null || SocialTitle != null;
    }
    
    public bool ShouldCreateRssOptions()
    {
        return FeedUrl != null || Frequency != null || ConstrainRssImage != null;
    }
}