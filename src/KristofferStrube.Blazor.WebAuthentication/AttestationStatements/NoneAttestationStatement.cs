namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// The none attestation statement format is used to replace any authenticator-provided attestation statement when a WebAuthn Relying Party indicates it does not wish to receive attestation information.
/// </summary>
/// <remarks><see href="https://w3c.github.io/webauthn/#sctn-none-attestation">See the API definition here</see>.</remarks>
public class NoneAttestationStatement : AttestationStatement
{
}
