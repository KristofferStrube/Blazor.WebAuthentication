using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

public class PublicKeyCredentialEntity
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
