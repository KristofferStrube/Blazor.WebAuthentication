using System.Formats.Cbor;

namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// This attestation statement format is generally used by authenticators that use a Trusted Platform Module as their cryptographic engine.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webauthn-3/#sctn-tpm-attestation">See the API definition here</see>.</remarks>
public class TPMAttestationStatement : AttestationStatement
{
    /// <summary>
    /// The version of the TPM specification to which the signature conforms.
    /// </summary>
    public required string Version { get; set; }

    /// <summary>
    /// The algorithm used to generate the attestation signature.
    /// </summary>
    public required COSEAlgorithm Algorithm { get; set; }

    /// <summary>
    /// The AIK certificate used for the attestation, in X.509 encoding. Followed by its certificate chain, in X.509 encoding.
    /// </summary>
    public byte[][]? X5c { get; set; }

    /// <summary>
    /// The attestation signature, in the form of a TPMT_SIGNATURE structure as specified in <see href="https://trustedcomputinggroup.org/wp-content/uploads/TPM-Rev-2.0-Part-2-Structures-01.38.pdf">TPMv2-Part2 section 11.3.4</see>.
    /// </summary>
    public required byte[] Signature { get; set; }

    /// <summary>
    /// The TPMS_ATTEST structure over which the above signature was computed, as specified in <see href="https://trustedcomputinggroup.org/wp-content/uploads/TPM-Rev-2.0-Part-2-Structures-01.38.pdf">TPMv2-Part2 section 10.12.8</see>.
    /// </summary>
    public required byte[] CertificateInformation { get; set; }

    /// <summary>
    /// The TPMT_PUBLIC structure (see <see href="https://trustedcomputinggroup.org/wp-content/uploads/TPM-Rev-2.0-Part-2-Structures-01.38.pdf">TPMv2-Part2 section 12.2.4</see>) used by the TPM to represent the credential public key.
    /// </summary>
    public required byte[] PublicArea { get; set; }

    internal static TPMAttestationStatement ReadAttestationStatement(CborReader cborReader)
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
        if (mapSize is not 2 or 3)
        {
            throw new FormatException($"Attestation Statement's packed format had '{mapSize}' entries but '2' or '3' was expected.");
        }

        state = cborReader.PeekState();
        if (state is not CborReaderState.TextString)
        {
            throw new FormatException($"Attestation Statement's packed format's first key was of type '{state}' but '{CborReaderState.TextString}' was expected.");
        }

        label = cborReader.ReadTextString();
        if (label is not "alg")
        {
            throw new FormatException($"Attestation Statement's packed format's first key was '{label}' but 'alg' was expected.");
        }

        state = cborReader.PeekState();
        if (state is not CborReaderState.NegativeInteger)
        {
            throw new FormatException($"Attestation Statement's packed format's 'alg' was of type '{state}' but '{CborReaderState.NegativeInteger}' was expected.");
        }

        ulong negativeAlg = cborReader.ReadCborNegativeIntegerRepresentation();

        state = cborReader.PeekState();
        if (state is not CborReaderState.TextString)
        {
            throw new FormatException($"Attestation Statement's packed format's second key was of type '{state}' but '{CborReaderState.TextString}' was expected.");
        }

        label = cborReader.ReadTextString();
        if (label is not "sig")
        {
            throw new FormatException($"Attestation Statement's packed format's second key was '{label}' but 'sig' was expected.");
        }

        state = cborReader.PeekState();
        if (state is not CborReaderState.ByteString)
        {
            throw new FormatException($"Attestation Statement's packed format's 'sig' was of type '{state}' but '{CborReaderState.ByteString}' was expected.");
        }

        byte[] signature = cborReader.ReadByteString();

        //if (mapSize is 2)
        //{
        //    return new()
        //    {
        //        Algorithm = (COSEAlgorithm)(-(long)negativeAlg - 1),
        //        Signature = signature,
        //    };
        //}

        throw new NotSupportedException("Reading x5c is not yet supported.");
    }
}
