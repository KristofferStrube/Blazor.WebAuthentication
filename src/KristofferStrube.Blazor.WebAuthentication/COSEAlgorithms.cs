namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// A COSEAlgorithmIdentifier's value is a number identifying a cryptographic algorithm.
/// </summary>
public enum COSEAlgorithm : long
{
    /// <summary>
    /// RSASSA-PKCS1-v1_5 using SHA-1<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8812">See the reference for RFC8812</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    RS1 = -65535,

    /// <summary>
    /// AES-CTR w/ 128-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9459">See the reference for RFC9459</see>.<br />
    /// </remarks>
    A128CTR = -65534,

    /// <summary>
    /// AES-CTR w/ 192-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9459">See the reference for RFC9459</see>.<br />
    /// </remarks>
    A192CTR = -65533,

    /// <summary>
    /// AES-CTR w/ 256-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9459">See the reference for RFC9459</see>.<br />
    /// </remarks>
    A256CTR = -65532,

    /// <summary>
    /// AES-CBC w/ 128-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9459">See the reference for RFC9459</see>.<br />
    /// </remarks>
    A128CBC = -65531,

    /// <summary>
    /// AES-CBC w/ 192-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9459">See the reference for RFC9459</see>.<br />
    /// </remarks>
    A192CBC = -65530,

    /// <summary>
    /// AES-CBC w/ 256-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9459">See the reference for RFC9459</see>.<br />
    /// </remarks>
    A256CBC = -65529,

    /// <summary>
    /// WalnutDSA signature<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9021">See the reference for RFC9021</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    WalnutDSA = -260,

    /// <summary>
    /// RSASSA-PKCS1-v1_5 using SHA-512<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8812">See the reference for RFC8812</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    RS512 = -259,

    /// <summary>
    /// RSASSA-PKCS1-v1_5 using SHA-384<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8812">See the reference for RFC8812</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    RS384 = -258,

    /// <summary>
    /// RSASSA-PKCS1-v1_5 using SHA-256<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8812">See the reference for RFC8812</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    RS256 = -257,

    /// <summary>
    /// ECDSA using secp256k1 curve and SHA-256<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8812">See the reference for RFC8812</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ES256K = -47,

    /// <summary>
    /// HSS/LMS hash-based digital signature<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8778">See the reference for RFC8778</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    HSS_LMS = -46,

    /// <summary>
    /// SHAKE-256 512-bit Hash Value<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9054">See the reference for RFC9054</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    SHAKE256 = -45,

    /// <summary>
    /// SHA-2 512-bit Hash<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9054">See the reference for RFC9054</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    SHA_512 = -44,

    /// <summary>
    /// SHA-2 384-bit Hash<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9054">See the reference for RFC9054</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    SHA_384 = -43,

    /// <summary>
    /// RSAES-OAEP w/ SHA-512<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8230">See the reference for RFC8230</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    RSAES_OAEP_SHA_512 = -42,

    /// <summary>
    /// RSAES-OAEP w/ SHA-256<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8230">See the reference for RFC8230</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    RSAES_OAEP_SHA_256 = -41,

    /// <summary>
    /// RSAES-OAEP w/ SHA-1<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8230">See the reference for RFC8230</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    RSAES_OAEP_RFC_8017_default_parameters = -40,

    /// <summary>
    /// RSASSA-PSS w/ SHA-512<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8230">See the reference for RFC8230</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    PS512 = -39,

    /// <summary>
    /// RSASSA-PSS w/ SHA-384<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8230">See the reference for RFC8230</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    PS384 = -38,

    /// <summary>
    /// RSASSA-PSS w/ SHA-256<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc8230">See the reference for RFC8230</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    PS256 = -37,

    /// <summary>
    /// ECDSA w/ SHA-512<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ES512 = -36,

    /// <summary>
    /// ECDSA w/ SHA-384<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ES384 = -35,

    /// <summary>
    /// ECDH SS w/ Concat KDF and AES Key Wrap w/ 256-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ECDH_SS_and_A256KW = -34,

    /// <summary>
    /// ECDH SS w/ Concat KDF and AES Key Wrap w/ 192-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ECDH_SS_and_A192KW = -33,

    /// <summary>
    /// ECDH SS w/ Concat KDF and AES Key Wrap w/ 128-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ECDH_SS_and_A128KW = -32,

    /// <summary>
    /// ECDH ES w/ Concat KDF and AES Key Wrap w/ 256-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ECDH_ES_and_A256KW = -31,

    /// <summary>
    /// ECDH ES w/ Concat KDF and AES Key Wrap w/ 192-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ECDH_ES_and_A192KW = -30,

    /// <summary>
    /// ECDH ES w/ Concat KDF and AES Key Wrap w/ 128-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ECDH_ES_and_A128KW = -29,

    /// <summary>
    /// ECDH SS w/ HKDF - generate key directly<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ECDH_SS_and_HKDF_512 = -28,

    /// <summary>
    /// ECDH SS w/ HKDF - generate key directly<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ECDH_SS_and_HKDF_256 = -27,

    /// <summary>
    /// ECDH ES w/ HKDF - generate key directly<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ECDH_ES_and_HKDF_512 = -26,

    /// <summary>
    /// ECDH ES w/ HKDF - generate key directly<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ECDH_ES_and_HKDF_256 = -25,

    /// <summary>
    /// SHAKE-128 256-bit Hash Value<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9054">See the reference for RFC9054</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    SHAKE128 = -18,

    /// <summary>
    /// SHA-2 512-bit Hash truncated to 256-bits<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9054">See the reference for RFC9054</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    SHA_512_truncated_to_256 = -17,

    /// <summary>
    /// SHA-2 256-bit Hash<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9054">See the reference for RFC9054</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    SHA_256 = -16,

    /// <summary>
    /// SHA-2 256-bit Hash truncated to 64-bits<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9054">See the reference for RFC9054</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    SHA_256_truncated_to_64 = -15,

    /// <summary>
    /// SHA-1 Hash<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9054">See the reference for RFC9054</see>.<br />
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    SHA_1 = -14,

    /// <summary>
    /// Shared secret w/ AES-MAC 256-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    directandHKDF_AES_256 = -13,

    /// <summary>
    /// Shared secret w/ AES-MAC 128-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    directandHKDF_AES_128 = -12,

    /// <summary>
    /// Shared secret w/ HKDF and SHA-512<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    directandHKDF_SHA_512 = -11,

    /// <summary>
    /// Shared secret w/ HKDF and SHA-256<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    directandHKDF_SHA_256 = -10,

    /// <summary>
    /// EdDSA<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    EdDSA = -8,

    /// <summary>
    /// ECDSA w/ SHA-256<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    ES256 = -7,

    /// <summary>
    /// Direct use of CEK<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    direct = -6,

    /// <summary>
    /// AES Key Wrap w/ 256-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    A256KW = -5,

    /// <summary>
    /// AES Key Wrap w/ 192-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    A192KW = -4,

    /// <summary>
    /// AES Key Wrap w/ 128-bit key<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    A128KW = -3,

    /// <summary>
    /// <br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    Reserved = 0,

    /// <summary>
    /// "AES-GCM mode w/ 128-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    A128GCM = 1,

    /// <summary>
    /// "AES-GCM mode w/ 192-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    A192GCM = 2,

    /// <summary>
    /// "AES-GCM mode w/ 256-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    A256GCM = 3,

    /// <summary>
    /// HMAC w/ SHA-256 truncated to 64 bits<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    HMAC_256_truncated_to_64 = 4,

    /// <summary>
    /// HMAC w/ SHA-256<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    HMAC_256_truncated_to_256 = 5,

    /// <summary>
    /// HMAC w/ SHA-384<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    HMAC_384_truncated_to_384 = 6,

    /// <summary>
    /// HMAC w/ SHA-512<br />
    /// This is recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    HMAC_512_truncated_to_512 = 7,

    /// <summary>
    /// "AES-CCM mode 128-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/kty">See the reference for kty</see>.<br />
    /// </remarks>
    AES_CCM_16_64_128 = 10,

    /// <summary>
    /// "AES-CCM mode 256-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/kty">See the reference for kty</see>.<br />
    /// </remarks>
    AES_CCM_16_64_256 = 11,

    /// <summary>
    /// "AES-CCM mode 128-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/kty">See the reference for kty</see>.<br />
    /// </remarks>
    AES_CCM_64_64_128 = 12,

    /// <summary>
    /// "AES-CCM mode 256-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/kty">See the reference for kty</see>.<br />
    /// </remarks>
    AES_CCM_64_64_256 = 13,

    /// <summary>
    /// "AES-MAC 128-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    AES_MAC_128_truncated_to_64 = 14,

    /// <summary>
    /// "AES-MAC 256-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    AES_MAC_256_truncated_to_64 = 15,

    /// <summary>
    /// "ChaCha20/Poly1305 w/ 256-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    ChaCha20_truncated_to_Poly1305 = 24,

    /// <summary>
    /// "AES-MAC 128-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    AES_MAC_128_truncated_to_128 = 25,

    /// <summary>
    /// "AES-MAC 256-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    AES_MAC_256_truncated_to_128 = 26,

    /// <summary>
    /// "AES-CCM mode 128-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/kty">See the reference for kty</see>.<br />
    /// </remarks>
    AES_CCM_16_128_128 = 30,

    /// <summary>
    /// "AES-CCM mode 256-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/kty">See the reference for kty</see>.<br />
    /// </remarks>
    AES_CCM_16_128_256 = 31,

    /// <summary>
    /// "AES-CCM mode 128-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/kty">See the reference for kty</see>.<br />
    /// </remarks>
    AES_CCM_64_128_128 = 32,

    /// <summary>
    /// "AES-CCM mode 256-bit key<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/kty">See the reference for kty</see>.<br />
    /// </remarks>
    AES_CCM_64_128_256 = 33,

    /// <summary>
    /// For doing IV generation for symmetric algorithms.<br />
    /// This is not recommended to use.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.iana.org/go/rfc9053">See the reference for RFC9053</see>.<br />
    /// </remarks>
    IV_GENERATION = 34,

}
