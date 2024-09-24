using Apps.Mailchimp.Api;
using Apps.Mailchimp.Invocables;
using Apps.Mailchimp.Models.Responses.Templates;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Mailchimp.DataSources;

public class TemplateDataSource(InvocationContext invocationContext) : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new ApiRequest("/templates", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<TemplatesResponse>(request);
        
        return response.Items
            .Where(x => context.SearchString == null || x.Name.Contains(context.SearchString))
            .ToDictionary(x => x.Id, x => x.Name);
    }
}