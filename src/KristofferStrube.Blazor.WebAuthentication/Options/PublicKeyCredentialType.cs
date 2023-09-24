using KristofferStrube.Blazor.WebAuthentication.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

[JsonConverter(typeof(PublicKeyCredentialTypeConverter))]
public enum PublicKeyCredentialType
{
    PublicKey
}
