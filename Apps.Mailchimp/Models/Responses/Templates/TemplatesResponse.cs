using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Mailchimp.Models.Responses.Templates;

public class TemplatesResponse : BaseSearchResponse<TemplateResponse>
{
    [Display("Templates"), JsonProperty("templates")]
    public override List<TemplateResponse> Items { get; set; } = new();
}