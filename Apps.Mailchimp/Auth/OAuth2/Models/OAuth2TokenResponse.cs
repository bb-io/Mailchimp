using Newtonsoft.Json;

namespace Apps.Mailchimp.Auth.OAuth2.Models;

public class OAuth2TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = default!;
}