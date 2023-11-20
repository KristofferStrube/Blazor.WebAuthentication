using KristofferStrube.Blazor.CredentialManagement;
using KristofferStrube.Blazor.WebAuthentication.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAuthentication;
/// <summary>
/// The PublicKeyCredential interface inherits from <see cref="Credential"/>, and contains the attributes that are returned to the caller when a new credential is created, or a new assertion is requested.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/webauthn-2/#publickeycredential">See the API definition here</see>.</remarks>
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

    //public async Task<AuthenticatorResponse> GetResponseAsync()
    //{
    //    ValueReference responseAttribute = new ValueReference(JSRuntime, JSReference, "response");

    //    responseAttribute.ValueMapper = new()
    //    {
    //        { "AuthenticatorAttestationResponse", async () => await AuthenticatorAttestationResponse.CreateAsync(JSRuntime, await responseAttribute.GetValueAsync<IJSObjectReference>()) }
    //    };

    //    object? result = await responseAttribute.GetValueAsync();
    //    if (result is null)
    //    {
    //        throw new Exception();
    //    }

    //    return () as AuthenticatorResponse;
    //}
}