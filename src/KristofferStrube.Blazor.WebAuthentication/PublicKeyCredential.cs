using KristofferStrube.Blazor.CredentialManagement;
using KristofferStrube.Blazor.WebAuthentication.Extensions;
using KristofferStrube.Blazor.WebAuthentication.JSONRepresentations;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// The PublicKeyCredential interface inherits from <see cref="Credential"/>, and contains the attributes that are returned to the caller when a new credential is created, or a new assertion is requested.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webauthn-3/#publickeycredential">See the API definition here</see>.</remarks>
public class PublicKeyCredential : Credential
{
    protected readonly Lazy<Task<IJSObjectReference>> webAuthenticationHelperTask;

    protected internal PublicKeyCredential(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        webAuthenticationHelperTask = new(jSRuntime.GetHelperAsync);
    }

    public PublicKeyCredential(Credential credential) : this(credential.JSRuntime, credential.JSReference) { }

    /// <summary>
    /// This attribute returns the ArrayBuffer for this credential.
    /// </summary>
    public async Task<IJSObjectReference> GetRawIdAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "rawId");
    }

    public async Task<byte[]> GetRawIdAsArrayAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        IJSObjectReference arrayBuffer = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "rawId");

        IJSObjectReference webIDLHelper = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/KristofferStrube.Blazor.WebIDL/KristofferStrube.Blazor.WebIDL.js");
        IJSObjectReference uint8ArrayFromBuffer = await webIDLHelper.InvokeAsync<IJSObjectReference>("constructUint8Array", arrayBuffer);
        Uint8Array uint8Array = await Uint8Array.CreateAsync(JSRuntime, uint8ArrayFromBuffer);
        return await uint8Array.GetByteArrayAsync();
    }

    public async Task<AuthenticatorResponse> GetResponseAsync()
    {
        ValueReference responseAttribute = new(JSRuntime, JSReference, "response");
        return await AuthenticatorResponse.GetConcreteInstanceAsync(responseAttribute);
    }

    public async Task<PublicKeyCredentialJSON> ToJSONAsync()
    {
        AuthenticatorResponse response = await GetResponseAsync();
        if (response is AuthenticatorAssertionResponse authenticatorAssertion)
        {
            return new AuthenticationResponseJSON()
            {
                Id = Convert.ToBase64String(await GetRawIdAsArrayAsync()),
                RawId = Convert.ToBase64String(await GetRawIdAsArrayAsync()),
                Response = new()
                {
                    ClientDataJSON = Convert.ToBase64String(await authenticatorAssertion.GetClientDataJSONAsArrayAsync()),
                    AuthenticatorData = Convert.ToBase64String(await authenticatorAssertion.GetAuthenticatorDataAsArrayAsync()),
                    Signature = Convert.ToBase64String(await authenticatorAssertion.GetSignatureAsArrayAsync()),
                },
                ClientExtensionResults = new(),
                Type = "public-key"
            };
        }
        else if (response is AuthenticatorAttestationResponse authenticatorAttestation)
        {
            return new RegistrationResponseJSON()
            {
                Id = Convert.ToBase64String(await GetRawIdAsArrayAsync()),
                RawId = Convert.ToBase64String(await GetRawIdAsArrayAsync()),
                Response = new()
                {
                    ClientDataJSON = Convert.ToBase64String(await authenticatorAttestation.GetClientDataJSONAsArrayAsync()),
                    Transports = await authenticatorAttestation.GetTransportsAsync(),
                    AuthenticatorData = Convert.ToBase64String(await authenticatorAttestation.GetAuthenticatorDataAsArrayAsync()),
                    PublicKey = Convert.ToBase64String(await authenticatorAttestation.GetPublicKeyAsArrayAsync()),
                    PublicKeyAlgorithm = (long)await authenticatorAttestation.GetPublicKeyAlgorithmAsync(),
                    AttestationObject = Convert.ToBase64String(await authenticatorAttestation.GetAttestationObjectAsync()),
                },
                ClientExtensionResults = new(),
                Type = "public-key"
            };
        }
        return default!;
    }
}