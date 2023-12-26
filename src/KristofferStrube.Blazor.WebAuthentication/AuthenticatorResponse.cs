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
        if (result is IJSObjectReference)
        {
            return null!;
        }
        return (AuthenticatorResponse)result;
    }

    protected AuthenticatorResponse(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        JSRuntime = jSRuntime;
        JSReference = jSReference;
        webAuthenticationHelperTask = new(jSRuntime.GetHelperAsync);
    }
}
