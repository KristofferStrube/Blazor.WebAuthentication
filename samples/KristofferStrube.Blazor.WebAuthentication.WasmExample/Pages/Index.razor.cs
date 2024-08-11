using KristofferStrube.Blazor.CredentialManagement;
using KristofferStrube.Blazor.WebAuthentication.JSONRepresentations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using static KristofferStrube.Blazor.WebAuthentication.WasmExample.WebAuthenticationClient;

namespace KristofferStrube.Blazor.WebAuthentication.WasmExample.Pages;

public partial class Index : ComponentBase
{
    private bool isSupported = false;
    private string username = "";
    private CredentialsContainer container = default!;
    private PublicKeyCredential? credential;
    private PublicKeyCredential? validatedCredential;
    private bool? validated = null;
    private string? errorMessage;
    private byte[]? challenge;
    private byte[]? publicKey;

    [Inject]
    public required ILogger<Index> Logger { get; set; }

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Inject]
    public required WebAuthenticationClient WebAuthenticationClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        isSupported = await CredentialsService.IsSupportedAsync();
        if (!isSupported)
        {
            return;
        }

        container = await CredentialsService.GetCredentialsAsync();
    }

    private async Task CreateCredential()
    {
        try
        {
            if (username.Length == 0)
            {
                username = "default";
            }

            byte[] userId = Encoding.ASCII.GetBytes(username);
            challenge = await WebAuthenticationClient.RegisterChallenge(username);

            if (challenge is null)
            {
                errorMessage = "Was not succesfull in registering a challenge before making credentials.";
                credential = null;
                return;
            }

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
                        Name = username,
                        Id = userId,
                        DisplayName = username
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
                    Attestation = AttestationConveyancePreference.Direct,
                    AttestationFormats = ["tpm"]
                }
            };
            credential = await container.CreateAsync(options) is { } c ? new PublicKeyCredential(c) : null;

            if (credential is not null)
            {
                PublicKeyCredentialJSON registrationResponse = await credential.ToJSONAsync();
                if (registrationResponse is RegistrationResponseJSON { } registration)
                {
                    try
                    {
                        await WebAuthenticationClient.Register(username, registration);
                        publicKey = registration.Response.PublicKey is not null ? Convert.FromBase64String(registration.Response.PublicKey) : null;
                    }
                    catch (Exception e)
                    {
                        errorMessage = $"Was not successfull in registering the credentials. {e.Message}";
                        credential = null;
                        return;
                    }
                }
            }

            errorMessage = null;
            validated = null;
        }
        catch (Exception exception)
        {
            errorMessage = $"{exception.GetType().Name}: \"{exception.Message}\"";
            credential = null;
            Logger.LogError(exception, "Error during creation of credentials.");
        }
    }

    private async Task GetCredential()
    {
        try
        {
            if (username.Length == 0)
            {
                username = "default";
            }

            ValidateCredentials? setup = await WebAuthenticationClient.ValidateChallenge(username);
            if (setup is not { Challenge: { Length: > 0 } challenge, Credentials: { Count: > 0 } credentials })
            {
                validated = null;
                errorMessage = "The user was not previously registered.";
                return;
            }
            this.challenge = challenge;

            List<PublicKeyCredentialDescriptor> allowCredentials = new(credentials.Count);
            foreach (byte[] credential in credentials)
            {
                allowCredentials.Add(new PublicKeyCredentialDescriptor()
                {
                    Type = PublicKeyCredentialType.PublicKey,
                    Id = await JSRuntime.InvokeAsync<IJSObjectReference>("buffer", credential)
                });
            }

            CredentialRequestOptions options = new()
            {
                PublicKey = new PublicKeyCredentialRequestOptions()
                {
                    Challenge = challenge,
                    Timeout = 360000,
                    AllowCredentials = allowCredentials.ToArray()
                }
            };

            validatedCredential = await container.GetAsync(options) is { } c ? new PublicKeyCredential(c) : null;

            if (validatedCredential is not null)
            {
                PublicKeyCredentialJSON authenticationResponse = await validatedCredential.ToJSONAsync();
                if (authenticationResponse is AuthenticationResponseJSON { } authentication)
                {
                    validated = await WebAuthenticationClient.Validate(username, authentication);
                }
            }

            errorMessage = null;
        }
        catch (Exception exception)
        {
            errorMessage = $"{exception.GetType().Name}: \"{exception.Message}\"";
            validatedCredential = null;
            validated = null;
            Logger.LogError(exception, "Error during validation of credentials.");
        }
    }

}