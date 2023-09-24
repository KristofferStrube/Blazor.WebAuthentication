using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

public class CredentialCreationOptions : CredentialManagement.CredentialCreationOptions
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("publicKey")]
    public PublicKeyCredentialCreationOptions? PublicKey { get; set; }
}
