using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

public class PublicKeyCredentialDescriptor
{
    [JsonPropertyName("type")]
    public required PublicKeyCredentialType Type { get; set; }

    [JsonPropertyName("id")]
    public required IJSObjectReference Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transports")]
    public string[]? Transports { get; set; }
}
