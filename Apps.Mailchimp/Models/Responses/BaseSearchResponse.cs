using Apps.Mailchimp.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Mailchimp.Models.Responses;

public class BaseSearchResponse<T>
{
    public virtual List<T> Items { get; set; } = new();
    
    [Display("Total count")]
    public int TotalItems { get; set; }

    [DefinitionIgnore, JsonProperty("_links")]
    public List<LinkDto> Links { get; set; } = new();
}