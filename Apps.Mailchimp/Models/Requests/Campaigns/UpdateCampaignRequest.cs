using Apps.Mailchimp.DataSources;
using Apps.Mailchimp.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Mailchimp.Models.Requests.Campaigns;

public class UpdateCampaignRequest : CampaignIdentifier
{
    // Settings: 
    
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

    [Display("To name", Description = "The campaign's custom 'To' name. Typically the first name audience field.")] 
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
    
    // Tracking: 

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
    
    public bool ShouldUpdateSettings()
    {
        return SubjectLine != null || PreviewText != null || Title != null || FromName != null || ReplyTo != null ||
               UseConversation != null || ToName != null || FolderId != null || Authenticate != null ||
               AutoFooter != null || InlineCss != null || AutoTweet != null || FbComments != null || Timewarp != null ||
               TemplateId != null;
    }
    
    public bool ShouldUpdateTracking()
    {
        return Opens != null || HtmlClicks != null || TextClicks != null || GoalTracking != null || Ecomm360 != null ||
               GoogleAnalytics != null || Clicktale != null;
    }
}