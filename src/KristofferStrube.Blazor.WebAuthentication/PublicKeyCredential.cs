using KristofferStrube.Blazor.CredentialManagement;
using KristofferStrube.Blazor.WebAuthentication.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAuthentication;

public class PublicKeyCredential : Credential
{
    protected readonly Lazy<Task<IJSObjectReference>> webAuthenticationHelperTask;

    protected internal PublicKeyCredential(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        webAuthenticationHelperTask = new(jSRuntime.GetHelperAsync);
    }

    public PublicKeyCredential(Credential credential) : this(credential.JSRuntime, credential.JSReference) { }

    public async Task<IJSObjectReference> GetRawIdAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "rawId");
    }
}