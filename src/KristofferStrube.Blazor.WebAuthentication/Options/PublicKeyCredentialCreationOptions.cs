using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// The options specific to the public key of <see cref="CredentialCreationOptions"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webauthn-3/#dictdef-publickeycredentialcreationoptions">See the API definition here</see>.</remarks>
public class PublicKeyCredentialCreationOptions
{
    [JsonPropertyName("rp")]
    public required PublicKeyCredentialRpEntity Rp { get; set; }

    [JsonPropertyName("user")]
    public required PublicKeyCredentialUserEntity User { get; set; }

    [JsonPropertyName("challenge")]
    public required byte[] Challenge { get; set; }

    [JsonPropertyName("pubKeyCredParams")]
    public required PublicKeyCredentialParameters[] PubKeyCredParams { get; set; }

    [JsonPropertyName("timeout")]
    public ulong Timeout { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("hints")]
    public string? Hints { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("attestation")]
    public AttestationConveyancePreference Attestation { get; set; }

    /// <summary>
    /// The Relying Party can use this optional member to specify a preference regarding the attestation statement format used by the authenticator.
    /// Values are ordered from most preferable to least preferable.
    /// This parameter is advisory and the authenticator and could use an attestation statement not enumerated in this parameter.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("attestationFormats")]
    public AttestationFormat[]? AttestationFormats { get; set; }
}
