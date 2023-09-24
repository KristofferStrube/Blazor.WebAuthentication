using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CredentialManagement;

public class CredentialsContainer : BaseJSWrapper
{
    protected IJSObjectReference ErrorHandlingJSReference { get; set; }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="CredentialsContainer"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="CredentialsContainer"/>.</param>
    protected internal CredentialsContainer(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        ErrorHandlingJSReference = new ErrorHandlingJSObjectReference(jSRuntime, jSReference);
    }

    public async Task<Credential?> GetAsync(CredentialRequestOptions? options = null)
    {
        IJSObjectReference? jSInstance = await ErrorHandlingJSReference.InvokeAsync<IJSObjectReference?>("get", options);
        return jSInstance is null ? null : new Credential(JSRuntime, jSInstance);
    }

    public async Task<Credential> StoreAsync(Credential credential)
    {
        IJSObjectReference jSInstance = await ErrorHandlingJSReference.InvokeAsync<IJSObjectReference>("store", credential);
        return new Credential(JSRuntime, jSInstance);
    }

    public async Task<Credential?> CreateAsync(CredentialCreationOptions? options = null)
    {
        IJSObjectReference? jSInstance = await ErrorHandlingJSReference.InvokeAsync<IJSObjectReference?>("create", options);
        return jSInstance is null ? null : new Credential(JSRuntime, jSInstance);
    }

    public async Task PreventSilentAccessAsync()
    {
        await JSReference.InvokeVoidAsync("preventSilentAccess");
    }
}
