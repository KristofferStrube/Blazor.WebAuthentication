using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication.JSONRepresentations;

public class AuthenticatorAssertionResponseJSON
{
    [JsonPropertyName("clientDataJSON")]
    public required string ClientDataJSON { get; set; }

    [JsonPropertyName("authenticatorData")]
    public required string AuthenticatorData { get; set; }

    [JsonPropertyName("signature")]
    public required string Signature { get; set; }

    [JsonPropertyName("userHandle")]
    public string? UserHandle { get; set; }

    [JsonPropertyName("attestationObject")]
    public string? AttestationObject { get; set; }
}
