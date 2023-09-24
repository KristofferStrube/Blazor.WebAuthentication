using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

public class PublicKeyCredentialUserEntity : PublicKeyCredentialEntity
{
    [JsonPropertyName("id")]
    public required byte[] Id { get; set; }

    [JsonPropertyName("displayName")]
    public required string DisplayName { get; set; }
}
