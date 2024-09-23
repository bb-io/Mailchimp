using Apps.Mailchimp.DataSources.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Mailchimp.Models.Requests.Campaigns;

public class FilterCampaignRequest
{
    [Display("Campaign type"), StaticDataSource(typeof(CampaignTypeDataSource))]
    public string? CampaignType { get; set; }

    [Display("Status"), StaticDataSource(typeof(CampaignStatusDataSource))]
    public string? Status { get; set; }
    
    [Display("Before send time")]
    public DateTime? BeforeSendTime { get; set; }
    
    [Display("Since send time")]
    public DateTime? SinceSendTime { get; set; }
    
    [Display("Before create time")]
    public DateTime? BeforeCreateTime { get; set; }
    
    [Display("Since create time")]
    public DateTime? SinceCreateTime { get; set; }
    
    [Display("List ID")]
    public string? ListId { get; set; }
    
    [Display("Folder ID")]
    public string? FolderId { get; set; }

    [Display("Member ID")] 
    public string? MemberId { get; set; }

    [DefinitionIgnore]
    public int? Count { get; set; }
    
    [DefinitionIgnore] 
    public string? SortField { get; set; }
    
    [DefinitionIgnore]
    public string? SortDirection { get; set; }
}