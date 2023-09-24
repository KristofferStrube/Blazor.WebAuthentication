﻿using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

public class PublicKeyCredentialCreationOptions
{
    [JsonPropertyName("rp")]
    public required PublicKeyCredentialRpEntity Rp { get; set; }

    [JsonPropertyName("user")]
    public required PublicKeyCredentialUserEntity User { get; set; }

    [JsonPropertyName("challenge")]
    public required byte[] Challenge { get; set; }

    [JsonPropertyName("pubKeyCredParams")]
    public required PublicKeyCredentialParameters[] PubKeyCredParams { get; set; }

    [JsonPropertyName("timeout")]
    public ulong Timeout { get; set; }
}
