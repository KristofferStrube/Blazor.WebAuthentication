using KristofferStrube.Blazor.CredentialManagement;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Security.Cryptography;
using System.Text;

namespace KristofferStrube.Blazor.WebAuthentication.WasmExample.Pages;

public partial class Index : ComponentBase
{
    private bool isSupported = false;
    private CredentialsContainer container = default!;
    private PublicKeyCredential? credential;
    private PublicKeyCredential? validatedCredential;
    private bool? successfulGettingCredential = null;
    private string? type;
    private string? id;
    private string? errorMessage;
    private byte[]? challenge;
    private byte[]? publicKey;
    private byte[]? signature;

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Inject]
    public required WebAuthenticationClient WebAuthenticationClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        isSupported = await CredentialsService.IsSupportedAsync();
        if (!isSupported) return;
        container = await CredentialsService.GetCredentialsAsync();
    }

    private async Task CreateCredential()
    {
        byte[] userId = Encoding.ASCII.GetBytes("bob");
        //challenge = await WebAuthenticationClient.Register("bob");
        challenge = RandomNumberGenerator.GetBytes(32);
        CredentialCreationOptions options = new()
        {
            PublicKey = new PublicKeyCredentialCreationOptions()
            {
                Rp = new PublicKeyCredentialRpEntity
                {
                    Name = "Kristoffer Strube Consulting"
                },
                User = new PublicKeyCredentialUserEntity()
                {
                    Name = "bob",
                    Id = userId,
                    DisplayName = "Bob"
                },
                Challenge = challenge,
                PubKeyCredParams =
                [
                    new PublicKeyCredentialParameters()
                    {
                        Type = PublicKeyCredentialType.PublicKey,
                        Alg = COSEAlgorithm.ES256
                    },
                    new PublicKeyCredentialParameters()
                    {
                        Type = PublicKeyCredentialType.PublicKey,
                        Alg = COSEAlgorithm.RS256
                    }
                ],
                Timeout = 360000,
                Hints = "client-device",
                Attestation = "none",
                AttestationFormats = ["tpm"]
            }
        };

        try
        {
            credential = await container.CreateAsync(options) is { } c ? new PublicKeyCredential(c) : null;

            AuthenticatorResponse registrationResponse = await credential.GetResponseAsync();
            if (registrationResponse is AuthenticatorAttestationResponse { } registration)
            {
                IJSObjectReference rawBuffer = await registration.GetPublicKeyAsync();
                IJSObjectReference uint8ArrayFromBuffer = await (await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/KristofferStrube.Blazor.WebIDL/KristofferStrube.Blazor.WebIDL.js")).InvokeAsync<IJSObjectReference>("constructUint8Array", rawBuffer);
                Uint8Array uint8Array = await Uint8Array.CreateAsync(JSRuntime, uint8ArrayFromBuffer);
                publicKey = await uint8Array.GetByteArrayAsync();
            }
        }
        catch (DOMException exception)
        {
            errorMessage = $"{exception.Name}: \"{exception.Message}\"";
            credential = null;
        }
        if (credential is not null)
        {
            type = await credential.GetTypeAsync();
            id = await credential.GetIdAsync();
        }
    }

    private async Task GetCredential()
    {
        byte[] challenge = RandomNumberGenerator.GetBytes(32);
        CredentialRequestOptions options = new()
        {
            PublicKey = new PublicKeyCredentialRequestOptions()
            {
                Challenge = challenge,
                Timeout = 360000,
                AllowCredentials = [
                    new PublicKeyCredentialDescriptor()
                    {
                        Type = PublicKeyCredentialType.PublicKey,
                        Id = await credential!.GetRawIdAsync()
                    }
                ]
            }
        };

        try
        {
            validatedCredential = await container.GetAsync(options) is { } c ? new PublicKeyCredential(c) : null;

            if (validatedCredential is not null)
            {
                AuthenticatorResponse registrationResponse = await validatedCredential.GetResponseAsync();
                if (registrationResponse is AuthenticatorAssertionResponse { } validation)
                {
                    IJSObjectReference rawBuffer = await validation.GetSignatureAsync();
                    IJSObjectReference uint8ArrayFromBuffer = await (await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/KristofferStrube.Blazor.WebIDL/KristofferStrube.Blazor.WebIDL.js")).InvokeAsync<IJSObjectReference>("constructUint8Array", rawBuffer);
                    Uint8Array uint8Array = await Uint8Array.CreateAsync(JSRuntime, uint8ArrayFromBuffer);
                    signature = await uint8Array.GetByteArrayAsync();
                }
            }


        }
        catch (DOMException exception)
        {
            errorMessage = $"{exception.Name}: \"{exception.Message}\"";
            validatedCredential = null;
        }

        successfulGettingCredential = validatedCredential is not null;
    }

}