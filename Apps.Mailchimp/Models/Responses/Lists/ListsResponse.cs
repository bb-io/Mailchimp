using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Mailchimp.Models.Responses.Lists;

public class ListsResponse : BaseSearchResponse<ListResponse>
{
    [Display("Lists"), JsonProperty("lists")]
    public override List<ListResponse> Items { get; set; } = new();
}