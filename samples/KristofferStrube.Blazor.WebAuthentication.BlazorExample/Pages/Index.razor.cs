using KristofferStrube.Blazor.CredentialManagement;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.AspNetCore.Components;
using System.Security.Cryptography;
using System.Text;

namespace KristofferStrube.Blazor.WebAuthentication.BlazorExample.Pages;

public partial class Index : ComponentBase
{
    private CredentialsContainer container = default!;
    private PublicKeyCredential? credential;
    private PublicKeyCredential? validatedCredential;
    private bool? successfulGettingCredential = null;
    private string? type;
    private string? id;
    private string? errorMessage;

    protected override async Task OnInitializedAsync()
    {
        container = await CredentialsService.GetCredentialsAsync();
    }

    private async Task CreateCredential()
    {
        byte[] challenge = RandomNumberGenerator.GetBytes(32);
        byte[] userId = Encoding.ASCII.GetBytes("bob");
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
                        Alg = -257
                    }
                ],
                Timeout = 360000,
            }
        };

        try
        {
            credential = await container.CreateAsync(options) is { } c ? new PublicKeyCredential(c) : null;
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
        }
        catch (DOMException exception)
        {
            errorMessage = $"{exception.Name}: \"{exception.Message}\"";
            validatedCredential = null;
        }

        successfulGettingCredential = validatedCredential is not null;
    }
}