using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

public class CredentialRequestOptions : CredentialManagement.CredentialRequestOptions
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("publicKey")]
    public PublicKeyCredentialRequestOptions? PublicKey { get; set; }
}
