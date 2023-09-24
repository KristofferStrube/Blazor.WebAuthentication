using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

public class PublicKeyCredentialRpEntity : PublicKeyCredentialEntity
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
