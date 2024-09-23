using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Mailchimp.Models.Responses.Folders;

public class FoldersResponse : BaseSearchResponse<FolderResponse>
{
    [Display("Folders"), JsonProperty("folders")]
    public override List<FolderResponse> Items { get; set; } = new();
}