using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Templates;

public class TemplateResponse
{
    [Display("Template ID")]
    public string Id { get; set; } = string.Empty;
    
    [Display("Name")]
    public string Name { get; set; } = string.Empty;
    
    [Display("Template type")]
    public string Type { get; set; } = string.Empty;
}