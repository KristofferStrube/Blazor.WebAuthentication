using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication.JSONRepresentations;

public class AuthenticationResponseJSON : PublicKeyCredentialJSON
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("rawId")]
    public required string RawId { get; set; }

    [JsonPropertyName("response")]
    public required AuthenticatorAssertionResponseJSON Response { get; set; }

    [JsonPropertyName("authenticatorAttachment")]
    public string? AuthenticatorAttachment { get; set; }

    [JsonPropertyName("clientExtensionResults")]
    public required AuthenticationExtensionsClientOutputsJSON ClientExtensionResults { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }
}
