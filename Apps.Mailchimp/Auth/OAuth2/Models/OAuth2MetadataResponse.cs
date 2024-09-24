using Newtonsoft.Json;

namespace Apps.Mailchimp.Auth.OAuth2.Models;

public class OAuth2MetadataResponse
{
    [JsonProperty("dc")]
    public string Dc { get; set; } = default!;
}