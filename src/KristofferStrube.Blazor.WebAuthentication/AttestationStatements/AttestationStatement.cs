using System.Formats.Cbor;

namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// WebAuthn supports pluggable attestation statement formats.
/// Attestation statement formats are identified by a string, called an attestation statement format identifier, chosen by the author of the attestation statement format.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webauthn-3/#sctn-defined-attestation-formats">See the API definition here</see>.</remarks>
public abstract class AttestationStatement
{
    public static AttestationStatement ReadFromBase64EncodedAttestationStatement(string input)
    {
        CborReader cborReader = new(Convert.FromBase64String(input));

        CborReaderState state = cborReader.PeekState();

        if (state is not CborReaderState.StartMap)
        {
            throw new FormatException("Attestation Statement did not start with a map.");
        }

        int? mapSize = cborReader.ReadStartMap();
        if (mapSize is not 3)
        {
            throw new FormatException($"Attestation Statement had '{mapSize}' entries in its first map but '3' was expected.");
        }

        state = cborReader.PeekState();
        if (state is not CborReaderState.TextString)
        {
            throw new FormatException($"Attestation Statement's first key was of type '{state}' but '{CborReaderState.TextString}' was expected.");
        }

        string label = cborReader.ReadTextString();
        if (label is not "fmt")
        {
            throw new FormatException($"Attestation Statement's first key was '{label}' but 'fmt' was expected.");
        }

        state = cborReader.PeekState();
        if (state is not CborReaderState.TextString)
        {
            throw new FormatException($"Attestation Statement's first value was of type '{state}' but '{CborReaderState.TextString}' was expected.");
        }

        string fmt = cborReader.ReadTextString();

        return fmt switch
        {
            "packed" => PackedAttestationStatement.ReadAttestationStatement(cborReader),
            "android-safetynet" => AndroidSafetyNetAttestationStatement.ReadAttestationStatement(cborReader),
            _ => throw new FormatException($"Attestation Statement had format '{fmt}' which was not supported.")
        };
    }
}
