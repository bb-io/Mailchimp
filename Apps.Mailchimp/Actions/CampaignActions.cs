using Apps.Mailchimp.Api;
using Apps.Mailchimp.Invocables;
using Apps.Mailchimp.Models.Identifiers;
using Apps.Mailchimp.Models.Requests.Campaigns;
using Apps.Mailchimp.Models.Responses.Campaigns;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Mailchimp.Actions;

[ActionList]
public class CampaignActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Search campaigns", Description = "Search campaigns by specified criteria")]
    public async Task<CampaignsResponse> SearchCampaignsAsync([ActionParameter] FilterCampaignRequest filterRequest)
    {
        var requestUrl = $"/campaigns";
        requestUrl = BuildQueryString(requestUrl, filterRequest);

        var allCampaigns = new List<CampaignResponse>();
        var offset = 0;

        CampaignsResponse response;
        
        if(filterRequest.Count.HasValue)
        {
            if(filterRequest.Count > 1000)
            {
                throw new InvalidOperationException("Count cannot exceed 1000");
            }
            
            var paginatedUrl = QueryHelpers.AddQueryString(requestUrl, new Dictionary<string, string?>
            {
                { "offset", 0.ToString() },
                { "count", filterRequest.Count.ToString() }
            });

            var request = new ApiRequest(paginatedUrl, Method.Get, Creds);
            response = await Client.ExecuteWithErrorHandling<CampaignsResponse>(request);

            if (response.Items.Any())
            {
                allCampaigns.AddRange(response.Items);
            }
        }
        else
        {
            var count = 100;

            do
            {
                var paginatedUrl = QueryHelpers.AddQueryString(requestUrl, new Dictionary<string, string?>
                {
                    { "offset", offset.ToString() },
                    { "count", count.ToString() }
                });

                var request = new ApiRequest(paginatedUrl, Method.Get, Creds);
                response = await Client.ExecuteWithErrorHandling<CampaignsResponse>(request);

                if (response.Items.Any())
                {
                    allCampaigns.AddRange(response.Items);
                }

                offset += count;
            } while (response.Items.Count == count);
        }
        
        return new()
        {
            Items = allCampaigns,
            TotalItems = allCampaigns.Count
        };
    }

    [Action("Get campaign", Description = "Get campaign by specified ID")]
    public async Task<CampaignResponse> GetCampaignAsync([ActionParameter] CampaignIdentifier identifier)
    {
        var requestUrl = $"/campaigns/{identifier}";
        var request = new ApiRequest(requestUrl, Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<CampaignResponse>(request);
    }

    [Action("Create campaign", Description = "Create a new campaign")]
    public async Task<CampaignResponse> CreateCampaignAsync([ActionParameter] CreateCampaignRequest createRequest)
    {
        var requestUrl = "/campaigns";

        var requestBody = new Dictionary<string, object>
        {
            { "type", createRequest.CampaignType }
        };
        
        if(createRequest.ContentType != null)
        {
            requestBody.Add("content_type", createRequest.ContentType);
        }

        if (!string.IsNullOrEmpty(createRequest.ListId))
        {
            requestBody.Add("recipients", new Dictionary<string, object>
            {
                { "list_id", createRequest.ListId }
            });
        }

        if (createRequest.CampaignType == "variate" && createRequest.ShouldCreateVariateSettings())
        {
            var variateSettings = new Dictionary<string, object>();

            if (createRequest.TestSize.HasValue)
            {
                variateSettings.Add("test_size", createRequest.TestSize);
            }
            
            if (createRequest.WaitTime.HasValue)
            {
                variateSettings.Add("wait_time", createRequest.WaitTime);
            }

            if (createRequest.SubjectLines != null)
            {
                variateSettings.Add("subject_lines", createRequest.SubjectLines.ToList());
            }
            
            if (createRequest.SendTimes != null)
            {
                variateSettings.Add("send_times", createRequest.SendTimes);
            }
            
            if (createRequest.FromNames != null)
            {
                variateSettings.Add("from_names", createRequest.FromNames);
            }
            
            if (createRequest.ReplyToAddresses != null)
            {
                variateSettings.Add("reply_to_addresses", createRequest.ReplyToAddresses);
            }
            
            if (createRequest.WinnerCriteria != null)
            {
                variateSettings.Add("winner_criteria", createRequest.WinnerCriteria);
            }
            
            requestBody.Add("variate_settings", variateSettings);
        }

        if (createRequest.CampaignType == "rss" && createRequest.ShouldCreateRssOptions())
        {
            var rssOpts = new Dictionary<string, object>();
            
            if(createRequest.FeedUrl != null)
            {
                rssOpts.Add("feed_url", createRequest.FeedUrl);
            }
            
            if(createRequest.Frequency != null)
            {
                rssOpts.Add("frequency", createRequest.Frequency);
            }
            
            if(createRequest.ConstrainRssImage != null)
            {
                rssOpts.Add("constrain_rss_img", createRequest.ConstrainRssImage);
            }

            requestBody.Add("rss_opts", rssOpts);
        }

        if (createRequest.ShouldCreateSettings())
        {
            var settings = new Dictionary<string, object>();

            if (createRequest.SubjectLine != null)
            {
                settings.Add("subject_line", createRequest.SubjectLine);
            }

            if (createRequest.PreviewText != null)
            {
                settings.Add("preview_text", createRequest.PreviewText);
            }

            if (createRequest.Title != null)
            {
                settings.Add("title", createRequest.Title);
            }

            if (createRequest.FromName != null)
            {
                settings.Add("from_name", createRequest.FromName);
            }

            if (createRequest.ReplyTo != null)
            {
                settings.Add("reply_to", createRequest.ReplyTo);
            }

            if (createRequest.UseConversation != null)
            {
                settings.Add("use_conversation", createRequest.UseConversation);
            }

            if (createRequest.ToName != null)
            {
                settings.Add("to_name", createRequest.ToName);
            }

            if (createRequest.FolderId != null)
            {
                settings.Add("folder_id", createRequest.FolderId);
            }

            if (createRequest.Authenticate != null)
            {
                settings.Add("authenticate", createRequest.Authenticate);
            }

            if (createRequest.AutoFooter != null)
            {
                settings.Add("auto_footer", createRequest.AutoFooter);
            }

            if (createRequest.InlineCss != null)
            {
                settings.Add("inline_css", createRequest.InlineCss);
            }

            if (createRequest.AutoTweet != null)
            {
                settings.Add("auto_tweet", createRequest.AutoTweet);
            }

            if (createRequest.FbComments != null)
            {
                settings.Add("fb_comments", createRequest.FbComments);
            }

            if (createRequest.Timewarp != null)
            {
                settings.Add("timewarp", createRequest.Timewarp);
            }

            if (createRequest.TemplateId != null)
            {
                settings.Add("template_id", Convert.ToInt32(createRequest.TemplateId));
            }

            if (createRequest.DragAndDrop != null)
            {
                settings.Add("drag_and_drop", createRequest.DragAndDrop);
            }

            requestBody.Add("settings", settings);
        }

        if (createRequest.ShouldCreateTracking())
        {
            var tracking = new Dictionary<string, object>();

            if (createRequest.Opens != null)
            {
                tracking.Add("opens", createRequest.Opens);
            }

            if (createRequest.HtmlClicks != null)
            {
                tracking.Add("html_clicks", createRequest.HtmlClicks);
            }

            if (createRequest.TextClicks != null)
            {
                tracking.Add("text_clicks", createRequest.TextClicks);
            }

            if (createRequest.GoalTracking != null)
            {
                tracking.Add("goal_tracking", createRequest.GoalTracking);
            }

            if (createRequest.Ecomm360 != null)
            {
                tracking.Add("ecomm360", createRequest.Ecomm360);
            }

            if (createRequest.GoogleAnalytics != null)
            {
                tracking.Add("google_analytics", createRequest.GoogleAnalytics);
            }

            if (createRequest.Clicktale != null)
            {
                tracking.Add("clicktale", createRequest.Clicktale);
            }

            requestBody.Add("tracking", tracking);
        }

        if (createRequest.ShouldCreateSocialCard())
        {
            var socialCard = new Dictionary<string, object>();

            if (createRequest.ImageUrl != null)
            {
                socialCard.Add("image_url", createRequest.ImageUrl);
            }

            if (createRequest.Description != null)
            {
                socialCard.Add("description", createRequest.Description);
            }

            if (createRequest.SocialTitle != null)
            {
                socialCard.Add("title", createRequest.SocialTitle);
            }

            requestBody.Add("social_card", socialCard);
        }
        
        var request = new ApiRequest(requestUrl, Method.Post, Creds)
            .WithJsonBody(requestBody);

        return await Client.ExecuteWithErrorHandling<CampaignResponse>(request);
    }


    [Action("Update campaign", Description = "Update campaign by specified ID")]
    public async Task<CampaignResponse> UpdateCampaignAsync([ActionParameter] UpdateCampaignRequest updateRequest)
    {
        var requestUrl = $"/campaigns/{updateRequest.CampaignId}";

        var requestBody = new Dictionary<string, object>();
        if (updateRequest.ShouldUpdateSettings())
        {
            var settings = new Dictionary<string, object>();

            if (updateRequest.SubjectLine != null)
            {
                settings.Add("subject_line", updateRequest.SubjectLine);
            }

            if (updateRequest.PreviewText != null)
            {
                settings.Add("preview_text", updateRequest.PreviewText);
            }

            if (updateRequest.Title != null)
            {
                settings.Add("title", updateRequest.Title);
            }

            if (updateRequest.FromName != null)
            {
                settings.Add("from_name", updateRequest.FromName);
            }

            if (updateRequest.ReplyTo != null)
            {
                settings.Add("reply_to", updateRequest.ReplyTo);
            }

            if (updateRequest.UseConversation != null)
            {
                settings.Add("use_conversation", updateRequest.UseConversation);
            }

            if (updateRequest.ToName != null)
            {
                settings.Add("to_name", updateRequest.ToName);
            }

            if (updateRequest.FolderId != null)
            {
                settings.Add("folder_id", updateRequest.FolderId);
            }

            if (updateRequest.Authenticate != null)
            {
                settings.Add("authenticate", updateRequest.Authenticate);
            }

            if (updateRequest.AutoFooter != null)
            {
                settings.Add("auto_footer", updateRequest.AutoFooter);
            }

            if (updateRequest.InlineCss != null)
            {
                settings.Add("inline_css", updateRequest.InlineCss);
            }

            if (updateRequest.AutoTweet != null)
            {
                settings.Add("auto_tweet", updateRequest.AutoTweet);
            }

            if (updateRequest.FbComments != null)
            {
                settings.Add("fb_comments", updateRequest.FbComments);
            }

            if (updateRequest.Timewarp != null)
            {
                settings.Add("timewarp", updateRequest.Timewarp);
            }

            if (updateRequest.TemplateId != null)
            {
                settings.Add("template_id", Convert.ToInt32(updateRequest.TemplateId));
            }

            requestBody.Add("settings", settings);
        }

        if (updateRequest.ShouldUpdateTracking())
        {
            var tracking = new Dictionary<string, object>();

            if (updateRequest.Opens != null)
            {
                tracking.Add("opens", updateRequest.Opens);
            }

            if (updateRequest.HtmlClicks != null)
            {
                tracking.Add("html_clicks", updateRequest.HtmlClicks);
            }

            if (updateRequest.TextClicks != null)
            {
                tracking.Add("text_clicks", updateRequest.TextClicks);
            }

            if (updateRequest.GoalTracking != null)
            {
                tracking.Add("goal_tracking", updateRequest.GoalTracking);
            }

            if (updateRequest.Ecomm360 != null)
            {
                tracking.Add("ecomm360", updateRequest.Ecomm360);
            }

            if (updateRequest.GoogleAnalytics != null)
            {
                tracking.Add("google_analytics", updateRequest.GoogleAnalytics);
            }

            if (updateRequest.Clicktale != null)
            {
                tracking.Add("clicktale", updateRequest.Clicktale);
            }

            requestBody.Add("tracking", tracking);
        }

        if (requestBody.Count == 0)
        {
            throw new InvalidOperationException("You must provide at least one field to update");
        }

        var request = new ApiRequest(requestUrl, Method.Patch, Creds)
            .WithJsonBody(requestBody);

        return await Client.ExecuteWithErrorHandling<CampaignResponse>(request);
    }

    [Action("Delete campaign", Description = "Delete campaign by specified ID")]
    public async Task DeleteCampaignAsync([ActionParameter] CampaignIdentifier identifier)
    {
        var requestUrl = $"/campaigns/{identifier}";
        var request = new ApiRequest(requestUrl, Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    private string BuildQueryString(string url, FilterCampaignRequest request)
    {
        var dictionary = new Dictionary<string, string?>
        {
            { "type", request.CampaignType },
            { "status", request.Status },
            { "before_send_time", request.BeforeSendTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "since_send_time", request.SinceSendTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "before_create_time", request.BeforeCreateTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "since_create_time", request.SinceCreateTime?.ToString("yyyy-MM-ddTHH:mm:ss") },
            { "list_id", request.ListId },
            { "folder_id", request.FolderId },
            { "member_id", request.MemberId },
            { "sort_field", request.SortField }, 
            { "sort_dir", request.SortDirection }
        };

        return QueryHelpers.AddQueryString(url, dictionary);
    }
}