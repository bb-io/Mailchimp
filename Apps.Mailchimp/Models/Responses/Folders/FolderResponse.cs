using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Folders;

public class FolderResponse
{
    [Display("Folder ID")] 
    public string Id { get; set; } = default!;
    
    [Display("Folder Name")]
    public string Name { get; set; } = default!;
    
    public double Count { get; set; }
}