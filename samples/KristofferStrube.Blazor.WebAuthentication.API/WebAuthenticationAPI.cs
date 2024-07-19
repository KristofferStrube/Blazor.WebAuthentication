using KristofferStrube.Blazor.WebAuthentication.JSONRepresentations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;

namespace KristofferStrube.Blazor.WebAuthentication.API;

public static class WebAuthenticationAPI
{
    public static Dictionary<string, byte[]> Challenges = [];

    public static Dictionary<string, List<byte[]>> Credentials = [];

    public static Dictionary<string, (COSEAlgorithm algorithm, byte[] key)> PublicKeys = [];

    public static IEndpointRouteBuilder MapWebAuthenticationAPI(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder
            .MapGroup("WebAuthentication");

        _ = group.MapGet("RegisterChallenge/{userName}", RegisterChallenge)
            .WithName("Register Challenge");

        _ = group.MapPost("Register/{userName}", Register)
            .WithName("Register");

        _ = group.MapGet("ValidateChallenge/{userName}", ValidateChallenge)
            .WithName("Validate Challenge");

        _ = group.MapPost("Validate/{userName}", Validate)
            .WithName("Validate");

        return builder;
    }

    public static Ok<byte[]> RegisterChallenge(string userName)
    {
        byte[] challenge = RandomNumberGenerator.GetBytes(32);
        Challenges[userName] = challenge;
        return TypedResults.Ok(challenge);
    }

    public static Ok<bool> Register(string userName, [FromBody] RegistrationResponseJSON registration)
    {
        CollectedClientData? clientData = System.Text.Json.JsonSerializer.Deserialize<CollectedClientData>(Convert.FromBase64String(registration.Response.ClientDataJSON));
        if (clientData is null)
        {
            return TypedResults.Ok(false);
        }

        if (!Challenges.TryGetValue(userName, out byte[]? originalChallenge)
            || !originalChallenge.SequenceEqual(WebEncoders.Base64UrlDecode(clientData.Challenge)))
        {
            return TypedResults.Ok(false);
        }

        if (registration.Response.PublicKey is null)
        {
            return TypedResults.Ok(false);
        }

        var attestationStatement = PackedAttestationFormat.ReadFromBase64EncodedAttestationStatement(registration.Response.AttestationObject);

        if (attestationStatement.Algorithm != (COSEAlgorithm)registration.Response.PublicKeyAlgorithm)
        {
            return TypedResults.Ok(false);
        }

        if (!VerifySignature(attestationStatement.Algorithm, Convert.FromBase64String(registration.Response.PublicKey), registration.Response.AuthenticatorData, registration.Response.ClientDataJSON, attestationStatement.Signature))
        {
            return TypedResults.Ok(false);
        }

        if (Credentials.TryGetValue(userName, out List<byte[]>? credentialList))
        {
            credentialList.Add(Convert.FromBase64String(registration.RawId));
        }
        else
        {
            Credentials[userName] = [Convert.FromBase64String(registration.RawId)];
        }
        PublicKeys[registration.RawId] = ((COSEAlgorithm)registration.Response.PublicKeyAlgorithm, Convert.FromBase64String(registration.Response.PublicKey));
        return TypedResults.Ok(true);
    }

    public static Ok<ValidateCredentials> ValidateChallenge(string userName)
    {
        if (!Credentials.TryGetValue(userName, out List<byte[]>? credentialList))
        {
            return TypedResults.Ok<ValidateCredentials>(new([], []));
        }
        byte[] challenge = RandomNumberGenerator.GetBytes(32);
        Challenges[userName] = challenge;
        return TypedResults.Ok<ValidateCredentials>(new(challenge, credentialList));
    }

    public class ValidateCredentials(byte[] challenge, List<byte[]> credentials)
    {
        public byte[] Challenge { get; set; } = challenge;
        public List<byte[]> Credentials { get; set; } = credentials;
    }

    public static Ok<bool> Validate(string userName, [FromBody] AuthenticationResponseJSON authentication)
    {
        CollectedClientData? clientData = System.Text.Json.JsonSerializer.Deserialize<CollectedClientData>(Convert.FromBase64String(authentication.Response.ClientDataJSON));
        if (clientData is null)
        {
            return TypedResults.Ok(false);
        }

        if (!Challenges.TryGetValue(userName, out byte[]? originalChallenge)
            || !originalChallenge.SequenceEqual(WebEncoders.Base64UrlDecode(clientData.Challenge)))
        {
            return TypedResults.Ok(false);
        }

        if (!PublicKeys.TryGetValue(authentication.RawId, out (COSEAlgorithm algorithm, byte[] key) publicKey))
        {
            return TypedResults.Ok(false);
        }

        return TypedResults.Ok(VerifySignature(publicKey.algorithm, publicKey.key, authentication.Response.AuthenticatorData, authentication.Response.ClientDataJSON, Convert.FromBase64String(authentication.Response.Signature)));
    }

    public static bool VerifySignature(COSEAlgorithm publicKeyAlgorithm, byte[] publicKey, string authenticatorData, string clientData, byte[] signature)
    {
        if (publicKeyAlgorithm is COSEAlgorithm.ES256)
        {
            try
            {
                var dsa = ECDsa.Create();
                dsa.ImportSubjectPublicKeyInfo(publicKey, out _);

                var Hash = SHA256.Create();

                byte[] hashedClientData = Hash.ComputeHash(Convert.FromBase64String(clientData));

                bool result = dsa.VerifyData(Convert.FromBase64String(authenticatorData).Concat(hashedClientData).ToArray(), signature, HashAlgorithmName.SHA256, DSASignatureFormat.Rfc3279DerSequence);

                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else if (publicKeyAlgorithm is COSEAlgorithm.RS256)
        {
            using var rsa = new RSACryptoServiceProvider();

            try
            {
                rsa.ImportSubjectPublicKeyInfo(publicKey, out _);

                var Hash = SHA256.Create();

                byte[] hashedClientData = Hash.ComputeHash(Convert.FromBase64String(clientData));

                bool result = rsa.VerifyData(Convert.FromBase64String(authenticatorData).Concat(hashedClientData).ToArray(), signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                return result;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }
        return false;
    }
}
