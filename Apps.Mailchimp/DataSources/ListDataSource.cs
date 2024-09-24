using Apps.Mailchimp.Api;
using Apps.Mailchimp.Invocables;
using Apps.Mailchimp.Models.Responses.Lists;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Mailchimp.DataSources;

public class ListDataSource(InvocationContext invocationContext) : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new ApiRequest("/lists", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ListsResponse>(request);
        
        return response.Items
            .Where(x => context.SearchString == null || x.Name.Contains(context.SearchString))
            .ToDictionary(x => x.Id, x => x.Name);
    }
}