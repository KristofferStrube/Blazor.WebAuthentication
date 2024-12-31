using System.Formats.Cbor;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

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
        if (mapSize is null)
        {
            throw new FormatException($"Attestation Statement's format had no keys.");
        }

        string? version = null;
        ulong? alg = null;
        byte[][]? x5c = null;
        byte[]? sig = null;
        byte[]? certInfo = null;
        byte[]? pubArea = null;

        for (int i = 0; i < mapSize; i++)
        {
            state = cborReader.PeekState();
            if (state is not CborReaderState.TextString)
            {
                throw new FormatException($"Attestation Statement's format's key number '{i + 1}' was of type '{state}' but '{CborReaderState.TextString}' was expected.");
            }

            label = cborReader.ReadTextString();
            switch (label)
            {
                case "ver":
                    state = cborReader.PeekState();
                    if (state is not CborReaderState.TextString)
                    {
                        throw new FormatException($"Attestation Statement's format's 'ver' was of type '{state}' but '{CborReaderState.TextString}' was expected.");
                    }
                    version = cborReader.ReadTextString();
                    break;
                case "alg":
                    state = cborReader.PeekState();
                    if (state is not CborReaderState.NegativeInteger)
                    {
                        throw new FormatException($"Attestation Statement's format's 'alg' was of type '{state}' but '{CborReaderState.NegativeInteger}' was expected.");
                    }
                    alg = cborReader.ReadCborNegativeIntegerRepresentation();
                    break;
                case "x5c":
                    state = cborReader.PeekState();
                    if (state is not CborReaderState.StartArray)
                    {
                        throw new FormatException($"Attestation Statement's format's 'x5c' started with a '{state}' but '{CborReaderState.StartArray}' was expected.");
                    }
                    int? x5cLength = cborReader.ReadStartArray();
                    if (x5cLength is null)
                    {
                        throw new FormatException($"Attestation Statement's format's 'x5c' was empty, but a length of atleast 2 was expected.");
                    }

                    x5c = new byte[x5cLength.Value][];
                    for (int j = 0; j < x5cLength; j++)
                    {
                        x5c[j] = cborReader.ReadByteString();
                    }
                    cborReader.ReadEndArray();
                    break;
                case "sig":
                    state = cborReader.PeekState();
                    if (state is not CborReaderState.ByteString)
                    {
                        throw new FormatException($"Attestation Statement's format's 'sig' was of type '{state}' but '{CborReaderState.ByteString}' was expected.");
                    }
                    sig = cborReader.ReadByteString();
                    break;
                case "certInfo":
                    state = cborReader.PeekState();
                    if (state is not CborReaderState.ByteString)
                    {
                        throw new FormatException($"Attestation Statement's format's 'certInfo' was of type '{state}' but '{CborReaderState.ByteString}' was expected.");
                    }
                    certInfo = cborReader.ReadByteString();
                    break;
                case "pubArea":
                    state = cborReader.PeekState();
                    if (state is not CborReaderState.ByteString)
                    {
                        throw new FormatException($"Attestation Statement's format's 'pubArea' was of type '{state}' but '{CborReaderState.ByteString}' was expected.");
                    }
                    pubArea = cborReader.ReadByteString();
                    break;
                default:
                    throw new FormatException($"Unsupported key '{label}' found in Attestation Statement's format.");
            }
        }

        if (version is null)
        {
            throw new FormatException("Expected a 'ver' in Attestation Statement's format, but key was not present.");
        }
        if (alg is null)
        {
            throw new FormatException("Expected a 'alg' in Attestation Statement's format, but key was not present.");
        }
        if (sig is null)
        {
            throw new FormatException("Expected a 'sig' in Attestation Statement's format, but key was not present.");
        }
        if (certInfo is null)
        {
            throw new FormatException("Expected a 'certInfo' in Attestation Statement's format, but key was not present.");
        }
        if (pubArea is null)
        {
            throw new FormatException("Expected a 'pubArea' in Attestation Statement's format, but key was not present.");
        }

        return new()
        {
            Version = version,
            Algorithm = (COSEAlgorithm)(-(long)alg - 1),
            X5c = x5c,
            Signature = sig,
            CertificateInformation = certInfo,
            PublicArea = pubArea,
        };
    }

    public bool Verify(byte[] authenticatorData, byte[] clientDataHash)
    {
        byte[] attToBeSigned = authenticatorData.Concat(clientDataHash).ToArray();

        byte[] magic = CertificateInformation[..4];
        if (magic is not [0xff, 0x54, 0x43, 0x47]) // TPM_GENERATED_VALUE
        {
            return false;
        }

        byte[] type = CertificateInformation[4..6];
        if (type is not [0x80, 0x17]) // TPM_ST_ATTEST_CERTIFY
        {
            return false;
        }

        byte[] qualifiedSignerSizeBuffer = CertificateInformation[6..8];
        UInt16 qualifiedSignerSize = (UInt16)(qualifiedSignerSizeBuffer[0] * 256 + qualifiedSignerSizeBuffer[1]);

        byte[] extraDataSizeBuffer = CertificateInformation[(8 + qualifiedSignerSize)..(8 + qualifiedSignerSize + 2)];
        UInt16 extraDataSize = (UInt16)(extraDataSizeBuffer[0] * 256 + extraDataSizeBuffer[1]);
        byte[] extraData = CertificateInformation[(8 + qualifiedSignerSize + 2)..(8 + qualifiedSignerSize + 2 + extraDataSize)];

        var hasher = SHA1.Create();
        byte[] hash = hasher.ComputeHash(attToBeSigned);

        if (!extraData.SequenceEqual(hash))
        {
            return false;
        }

        int clockInfoSize = 8 + 4 + 4 + 1;

        int firmwareVersionSize = 8;

        byte[] attestedBuffer = CertificateInformation[(8 + qualifiedSignerSize + 2 + extraDataSize + clockInfoSize + firmwareVersionSize)..];

        byte[] tpmsCertifyInfoNameSizeBuffer = attestedBuffer[..2];
        UInt16 tpmsCertifyInfoNameSize = (UInt16)(tpmsCertifyInfoNameSizeBuffer[0] * 256 + tpmsCertifyInfoNameSizeBuffer[1]);
        byte[] tpmsCertifyInfoName = attestedBuffer[2..(2 + tpmsCertifyInfoNameSize)];

        byte[] tpmsCertifyInfoQualifiedNameSizeBuffer = attestedBuffer[(2 + tpmsCertifyInfoNameSize)..(2 + tpmsCertifyInfoNameSize + 2)];
        UInt16 tpmsCertifyInfoQualifiedNameSize = (UInt16)(tpmsCertifyInfoQualifiedNameSizeBuffer[0] * 256 + tpmsCertifyInfoQualifiedNameSizeBuffer[1]);

        byte[] pubAreaNameAlg = PublicArea[2..4];

        byte[]? name;
        switch (pubAreaNameAlg)
        {
            case [0x0, 0xB]:
                name = pubAreaNameAlg.Concat(SHA256.HashData(PublicArea)).ToArray();
                break;
            default:
                return false;
        }

        return name.SequenceEqual(tpmsCertifyInfoName);
    }

    //TPMI_ST_ATTEST type = typeBytes switch
    //{
    //[0x80, 0x14] => TPMI_ST_ATTEST.TPM_ST_ATTEST_NV,
    //[0x80, 0x15] => TPMI_ST_ATTEST.TPM_ST_ATTEST_COMMAND_AUDIT,
    //[0x80, 0x16] => TPMI_ST_ATTEST.TPM_ST_ATTEST_SESSION_AUDIT,
    //[0x80, 0x17] => TPMI_ST_ATTEST.TPM_ST_ATTEST_CERTIFY,
    //[0x80, 0x18] => TPMI_ST_ATTEST.TPM_ST_ATTEST_QUOTE,
    //[0x80, 0x19] => TPMI_ST_ATTEST.TPM_ST_ATTEST_TIME,
    //[0x80, 0x1a] => TPMI_ST_ATTEST.TPM_ST_ATTEST_CREATION,
    //    _ => TPMI_ST_ATTEST.Invalid,
    //};
    /// <remarks>
    /// From <see href="https://trustedcomputinggroup.org/wp-content/uploads/TPM-Rev-2.0-Part-2-Structures-01.38.pdf">[TPMv2-Part2]</see> section 10.12.6. 
    /// </remarks>
    //internal enum TPMI_ST_ATTEST
    //{
    //    /// <summary>
    //    /// Generated by TPM2_Certify()
    //    /// </summary>
    //    TPM_ST_ATTEST_CERTIFY,

    //    /// <summary>
    //    /// Generated by TPM2_Quote()
    //    /// </summary>
    //    TPM_ST_ATTEST_QUOTE,

    //    /// <summary>
    //    /// Generated by TPM2_GetSessionAuditDigest()
    //    /// </summary>
    //    TPM_ST_ATTEST_SESSION_AUDIT,

    //    /// <summary>
    //    /// Generated by TPM2_GetCommandAuditDigest()
    //    /// </summary>
    //    TPM_ST_ATTEST_COMMAND_AUDIT,

    //    /// <summary>
    //    /// Generated by TPM2_GetTime()
    //    /// </summary>
    //    TPM_ST_ATTEST_TIME,

    //    /// <summary>
    //    /// Generated by TPM2_CertifyCreation()
    //    /// </summary>
    //    TPM_ST_ATTEST_CREATION,

    //    /// <summary>
    //    /// Generated by TPM2_NV_Certify()
    //    /// </summary>
    //    TPM_ST_ATTEST_NV,

    //    /// <summary>
    //    /// Invalid
    //    /// </summary>
    //    Invalid,
    //}
}
