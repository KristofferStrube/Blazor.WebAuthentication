using KristofferStrube.Blazor.WebAuthentication.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// This enumeration defines the valid credential types.
/// It is an extension point; values can be added to it in the future, as more credential types are defined.
/// The values of this enumeration are used for versioning the Authentication Assertion and attestation structures according to the type of the authenticator.<br />
/// Currently one credential type is defined, namely <see cref="PublicKey"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webauthn-2/#enumdef-publickeycredentialtype">See the API definition here</see>.</remarks>
[JsonConverter(typeof(PublicKeyCredentialTypeConverter))]
public enum PublicKeyCredentialType
{
    PublicKey
}
