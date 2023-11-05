using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

public class PublicKeyCredentialParameters
{
    /// <summary>
    /// This member specifies the type of credential to be created.
    /// </summary>
    [JsonPropertyName("type")]
    public required PublicKeyCredentialType Type { get; set; }

    /// <summary>
    /// This member specifies the cryptographic signature algorithm with which the newly generated credential will be used, and thus also the type of asymmetric key pair to be generated, e.g., RSA or Elliptic Curve.
    /// </summary>
    [JsonPropertyName("alg")]
    public required COSEAlgorithm Algorithm { get; set; }
}
