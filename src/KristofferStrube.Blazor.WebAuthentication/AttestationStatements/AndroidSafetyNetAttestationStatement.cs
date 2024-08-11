using System.Formats.Cbor;

namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// When the authenticator is a platform authenticator on certain Android platforms, the attestation statement may be based on the SafetyNet API.
/// In this case the authenticator data is completely controlled by the caller of the SafetyNet API (typically an application running on the Android platform)
/// and the attestation statement provides some statements about the health of the platform and the identity of the calling application
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webauthn-3/#sctn-android-safetynet-attestation">See the API definition here</see>.</remarks>
public class AndroidSafetyNetAttestationStatement : AttestationStatement
{
    /// <summary>
    /// The algorithm used to generate the attestation signature.
    /// </summary>
    public required string Version { get; set; }

    /// <summary>
    /// The UTF-8 encoded result of the <c>getJwsResult()</c> call of the SafetyNet API.
    /// This value is a JWS object in Compact Serialization.
    /// </summary>
    public required byte[] Response { get; set; }

    internal static AndroidSafetyNetAttestationStatement ReadAttestationStatement(CborReader cborReader)
    {
        CborReaderState state = cborReader.PeekState();
        if (state is not CborReaderState.TextString)
        {
            throw new FormatException($"Attestation Statement's second key was of type '{state}' but '{CborReaderState.TextString}' was expected.");
        }

        string label = cborReader.ReadTextString();
        if (label is not "attStmt")
        {
            throw new FormatException($"Attestation Statement's second key was '{label}' but 'attStmt' was expected.");
        }

        state = cborReader.PeekState();
        if (state is not CborReaderState.StartMap)
        {
            throw new FormatException($"Attestation Statement's 'attStmt' was of type '{state}' but '{CborReaderState.StartMap}' was expected.");
        }

        int? mapSize = cborReader.ReadStartMap();
        if (mapSize is not 2)
        {
            throw new FormatException($"Attestation Statement's packed format had '{mapSize}' entries but '2' was expected.");
        }

        state = cborReader.PeekState();
        if (state is not CborReaderState.TextString)
        {
            throw new FormatException($"Attestation Statement's packed format's first key was of type '{state}' but '{CborReaderState.TextString}' was expected.");
        }

        label = cborReader.ReadTextString();
        if (label is not "ver")
        {
            throw new FormatException($"Attestation Statement's packed format's first key was '{label}' but 'ver' was expected.");
        }

        state = cborReader.PeekState();
        if (state is not CborReaderState.NegativeInteger)
        {
            throw new FormatException($"Attestation Statement's packed format's 'ver' was of type '{state}' but '{CborReaderState.TextString}' was expected.");
        }

        string version = cborReader.ReadTextString();

        state = cborReader.PeekState();
        if (state is not CborReaderState.TextString)
        {
            throw new FormatException($"Attestation Statement's packed format's second key was of type '{state}' but '{CborReaderState.TextString}' was expected.");
        }

        label = cborReader.ReadTextString();
        if (label is not "response")
        {
            throw new FormatException($"Attestation Statement's packed format's second key was '{label}' but 'response' was expected.");
        }

        state = cborReader.PeekState();
        if (state is not CborReaderState.ByteString)
        {
            throw new FormatException($"Attestation Statement's packed format's 'response' was of type '{state}' but '{CborReaderState.ByteString}' was expected.");
        }

        byte[] response = cborReader.ReadByteString();

        return new()
        {
            Version = version,
            Response = response
        };
    }
}
