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

    [Display("Count", Description = "The number of records to return. By default we will return all the records. Maximum value is 1000")]
    public int? Count { get; set; }
    
    [Display("Sort field"), StaticDataSource(typeof(SortFieldDataSource))] 
    public string? SortField { get; set; }
    
    [Display("Sort direction"), StaticDataSource(typeof(SortDirectionDataSource))]
    public string? SortDirection { get; set; }
}