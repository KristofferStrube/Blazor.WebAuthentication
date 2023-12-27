using KristofferStrube.Blazor.WebAuthentication.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// Authenticators respond to Relying Party requests by returning an object derived from the <see cref="AuthenticatorResponse"/> interface.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webauthn-3/#authenticatorresponse">See the API definition here</see>.</remarks>
public abstract class AuthenticatorResponse : IJSWrapper
{
    protected readonly Lazy<Task<IJSObjectReference>> webAuthenticationHelperTask;

    public IJSRuntime JSRuntime { get; set; }
    public IJSObjectReference JSReference { get; set; }

    public static async Task<AuthenticatorResponse> GetConcreteInstanceAsync(ValueReference authenticatorResponse)
    {
        authenticatorResponse.ValueMapper = new()
        {
            { "authenticatorattestationresponse", async () => await AuthenticatorAttestationResponse.CreateAsync(authenticatorResponse.JSRuntime, await authenticatorResponse.GetValueAsync<IJSObjectReference>()) },
            { "authenticatorassertionresponse", async () => await AuthenticatorAssertionResponse.CreateAsync(authenticatorResponse.JSRuntime, await authenticatorResponse.GetValueAsync<IJSObjectReference>()) }
        };

        object? result = await authenticatorResponse.GetValueAsync();
        return result is IJSObjectReference or null ? null! : (AuthenticatorResponse)result;
    }

    protected AuthenticatorResponse(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        JSRuntime = jSRuntime;
        JSReference = jSReference;
        webAuthenticationHelperTask = new(jSRuntime.GetHelperAsync);
    }
    public async Task<IJSObjectReference> GetClientDataJSONAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "clientDataJSON");
    }
    public async Task<byte[]> GetClientDataJSONAsArrayAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        IJSObjectReference arrayBuffer = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "clientDataJSON");

        IJSObjectReference webIDLHelper = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/KristofferStrube.Blazor.WebIDL/KristofferStrube.Blazor.WebIDL.js");
        IJSObjectReference uint8ArrayFromBuffer = await webIDLHelper.InvokeAsync<IJSObjectReference>("constructUint8Array", arrayBuffer);
        Uint8Array uint8Array = await Uint8Array.CreateAsync(JSRuntime, uint8ArrayFromBuffer);
        return await uint8Array.GetByteArrayAsync();
    }
}
