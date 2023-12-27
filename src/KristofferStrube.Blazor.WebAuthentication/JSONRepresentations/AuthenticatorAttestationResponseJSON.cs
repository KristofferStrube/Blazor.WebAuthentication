using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication.JSONRepresentations;

public class AuthenticatorAttestationResponseJSON
{
    [JsonPropertyName("clientDataJSON")]
    public required string ClientDataJSON { get; set; }

    [JsonPropertyName("authenticatorData")]
    public required string AuthenticatorData { get; set; }

    [JsonPropertyName("transports")]
    public required string[] Transports { get; set; }

    /// <remarks>
    /// The publicKey field will be missing if pubKeyCredParams was used to negotiate a public-key algorithm that the user agent doesn’t understand.
    /// If using such an algorithm then the public key must be parsed directly from attestationObject or authenticatorData.
    /// </remarks>
    [JsonPropertyName("publicKey")]
    public string? PublicKey { get; set; }

    [JsonPropertyName("publicKeyAlgorithm")]
    public required long PublicKeyAlgorithm { get; set; }

    [JsonPropertyName("attestationObject")]
    public required string AttestationObject { get; set; }
}
