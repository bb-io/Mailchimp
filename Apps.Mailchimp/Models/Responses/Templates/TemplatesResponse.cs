using Blackbird.Applications.Sdk.Common;

namespace Apps.Mailchimp.Models.Responses.Templates;

public class TemplatesResponse : BaseSearchResponse<TemplateResponse>
{
    [Display("Templates")]
    public override List<TemplateResponse> Items { get; set; } = new();
}