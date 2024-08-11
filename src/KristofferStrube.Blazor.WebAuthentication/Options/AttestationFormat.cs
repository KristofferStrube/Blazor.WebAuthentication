using KristofferStrube.Blazor.WebAuthentication.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// Authenticators may implement various transports for communicating with clients.
/// This enum defines hints as to how clients might communicate with a particular authenticator in order to obtain an assertion for a specific credential.
/// </summary>
/// <remarks><see href="https://www.iana.org/assignments/webauthn/webauthn.xhtml">See the API definition here</see>.</remarks>
[JsonConverter(typeof(AttestationFormatConverter))]
public enum AttestationFormat
{
    /// <summary>
    /// The "packed" attestation statement format is a WebAuthn-optimized format for attestation. It uses a very compact but still extensible encoding method.
    /// This format is implementable by authenticators with limited resources (e.g., secure elements).	
    /// </summary>
    Packed,
    /// <summary>
    /// The TPM attestation statement format returns an attestation statement in the same format as the packed attestation statement format,
    /// although the rawData and signature fields are computed differently.	
    /// </summary>
    TPM,
    /// <summary>
    /// Platform authenticators on versions "N", and later, may provide this proprietary "hardware attestation" statement.	
    /// </summary>
    AndroidKey,
    /// <summary>
    /// Android-based platform authenticators MAY produce an attestation statement based on the Android SafetyNet API.	
    /// </summary>
    AndroidSafetyNet,
    /// <summary>
    /// Used with FIDO U2F authenticators
    /// </summary>
    FidoU2F,
    /// <summary>
    /// Used with Apple devices' platform authenticators	
    /// </summary>
    Apple,
    /// <summary>
    /// Used to replace any authenticator-provided attestation statement when a WebAuthn Relying Party indicates it does not wish to receive attestation information.	
    /// </summary>
    None,
}
