using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

public class PublicKeyCredentialRequestOptions
{
    [JsonPropertyName("challenge")]
    public required byte[] Challenge { get; set; }

    [JsonPropertyName("timeout")]
    public ulong Timeout { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("rpId")]
    public string? RpId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("allowCredentials")]
    public PublicKeyCredentialDescriptor[]? AllowCredentials { get; set; }

    [JsonPropertyName("userVerfication")]
    public UserVerificationRequirement UserVerfication { get; set; } = UserVerificationRequirement.Preferred;
}
