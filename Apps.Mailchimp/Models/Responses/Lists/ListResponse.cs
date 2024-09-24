using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Lists;

public class ListResponse
{
    [Display("List ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Web ID")]
    public string WebId { get; set; } = string.Empty;
    
    [Display("Name")]
    public string Name { get; set; } = string.Empty;
}