using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAuthentication;

public class AuthenticatorAssertionResponse : AuthenticatorResponse, IJSCreatable<AuthenticatorAssertionResponse>
{
    public static Task<AuthenticatorAssertionResponse> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult<AuthenticatorAssertionResponse>(new(jSRuntime, jSReference));
    }

    public AuthenticatorAssertionResponse(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<IJSObjectReference> GetAuthenticatorDataAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "authenticatorData");
    }

    public async Task<byte[]> GetAuthenticatorDataAsArrayAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        IJSObjectReference arrayBuffer = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "authenticatorData");

        IJSObjectReference webIDLHelper = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/KristofferStrube.Blazor.WebIDL/KristofferStrube.Blazor.WebIDL.js");
        IJSObjectReference uint8ArrayFromBuffer = await webIDLHelper.InvokeAsync<IJSObjectReference>("constructUint8Array", arrayBuffer);
        Uint8Array uint8Array = await Uint8Array.CreateAsync(JSRuntime, uint8ArrayFromBuffer);
        return await uint8Array.GetByteArrayAsync();
    }

    public async Task<IJSObjectReference> GetSignatureAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "signature");
    }

    public async Task<byte[]> GetSignatureAsArrayAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        IJSObjectReference arrayBuffer = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "signature");

        IJSObjectReference webIDLHelper = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/KristofferStrube.Blazor.WebIDL/KristofferStrube.Blazor.WebIDL.js");
        IJSObjectReference uint8ArrayFromBuffer = await webIDLHelper.InvokeAsync<IJSObjectReference>("constructUint8Array", arrayBuffer);
        Uint8Array uint8Array = await Uint8Array.CreateAsync(JSRuntime, uint8ArrayFromBuffer);
        return await uint8Array.GetByteArrayAsync();
    }
}
