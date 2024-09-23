using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Campaigns;

public class CampaignSettingsResponse
{
    [Display("Subject line")]
    public string SubjectLine { get; set; } = default!;
    
    [Display("Preview text")]
    public string PreviewText { get; set; } = default!;
    
    [Display("Title")]
    public string Title { get; set; } = default!;
    
    [Display("From name")]
    public string FromName { get; set; } = default!;
    
    [Display("Reply to")]
    public string ReplyTo { get; set; } = default!;
    
    [Display("Use conversation")]
    public bool UseConversation { get; set; }
    
    [Display("To name")]
    public string ToName { get; set; } = default!;
    
    [Display("Folder ID")]
    public string FolderId { get; set; } = default!;
    
    [Display("Authenticate")]
    public bool Authenticate { get; set; }
    
    [Display("Auto footer")]
    public bool AutoFooter { get; set; }
    
    [Display("Inline CSS")]
    public bool InlineCss { get; set; }
    
    [Display("Auto tweet")]
    public bool AutoTweet { get; set; }
    
    [Display("Facebook comments")]
    public bool FbComments { get; set; }
    
    [Display("Timewarp")]
    public bool Timewarp { get; set; }
    
    [Display("Template ID")]
    public string TemplateId { get; set; } = default!;
    
    [Display("Drag and drop")]
    public bool DragAndDrop { get; set; }
}