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
    /// This member specifies the cryptographic signature algorithm with which the newly generated credential will be used, and thus also the type of asymmetric key pair to be generated, e.g., RSA or Elliptic Curve.<br />
    /// Supported values are:
    /// <list type="bullet">
    /// <item><c>-7</c> (P-256)</item>
    /// <item><c>-35</c> (P-384)</item>
    /// <item><c>-36</c> (P-521)</item>
    /// <item><c>-8</c> (Ed25519)</item>
    /// </list>
    /// </summary>
    [JsonPropertyName("alg")]
    public required long Alg { get; set; }
}
