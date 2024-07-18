using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAuthentication;

public class AuthenticatorAttestationResponse : AuthenticatorResponse, IJSCreatable<AuthenticatorAttestationResponse>
{
    public static Task<AuthenticatorAttestationResponse> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult<AuthenticatorAttestationResponse>(new(jSRuntime, jSReference));
    }

    public AuthenticatorAttestationResponse(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<byte[]> GetAttestationObjectAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        IJSObjectReference arrayBuffer = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "attestationObject");

        IJSObjectReference webIDLHelper = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/KristofferStrube.Blazor.WebIDL/KristofferStrube.Blazor.WebIDL.js");
        IJSObjectReference uint8ArrayFromBuffer = await webIDLHelper.InvokeAsync<IJSObjectReference>("constructUint8Array", arrayBuffer);
        Uint8Array uint8Array = await Uint8Array.CreateAsync(JSRuntime, uint8ArrayFromBuffer);
        return await uint8Array.GetByteArrayAsync();
    }

    /// <summary>
    /// These values are the transports that the authenticator is believed to support, or an empty sequence if the information is unavailable.
    /// </summary>
    public async Task<AuthenticatorTransport[]> GetTransportsAsync()
    {
        return await JSReference.InvokeAsync<AuthenticatorTransport[]>("getTransports");
    }

    public async Task<IJSObjectReference> GetAuthenticatorDataAsync()
    {
        return await JSReference.InvokeAsync<IJSObjectReference>("getAuthenticatorData");
    }

    public async Task<byte[]> GetAuthenticatorDataAsArrayAsync()
    {
        IJSObjectReference arrayBuffer = await JSReference.InvokeAsync<IJSObjectReference>("getAuthenticatorData");

        IJSObjectReference webIDLHelper = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/KristofferStrube.Blazor.WebIDL/KristofferStrube.Blazor.WebIDL.js");
        IJSObjectReference uint8ArrayFromBuffer = await webIDLHelper.InvokeAsync<IJSObjectReference>("constructUint8Array", arrayBuffer);
        Uint8Array uint8Array = await Uint8Array.CreateAsync(JSRuntime, uint8ArrayFromBuffer);
        return await uint8Array.GetByteArrayAsync();
    }

    public async Task<IJSObjectReference> GetPublicKeyAsync()
    {
        return await JSReference.InvokeAsync<IJSObjectReference>("getPublicKey");
    }

    public async Task<byte[]> GetPublicKeyAsArrayAsync()
    {
        IJSObjectReference arrayBuffer = await JSReference.InvokeAsync<IJSObjectReference>("getPublicKey");

        IJSObjectReference webIDLHelper = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/KristofferStrube.Blazor.WebIDL/KristofferStrube.Blazor.WebIDL.js");
        IJSObjectReference uint8ArrayFromBuffer = await webIDLHelper.InvokeAsync<IJSObjectReference>("constructUint8Array", arrayBuffer);
        Uint8Array uint8Array = await Uint8Array.CreateAsync(JSRuntime, uint8ArrayFromBuffer);
        return await uint8Array.GetByteArrayAsync();
    }

    public async Task<COSEAlgorithm> GetPublicKeyAlgorithmAsync()
    {
        return await JSReference.InvokeAsync<COSEAlgorithm>("getPublicKeyAlgorithm");
    }
}
