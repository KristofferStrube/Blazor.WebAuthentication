﻿using KristofferStrube.Blazor.CredentialManagement.Converters;
using KristofferStrube.Blazor.DOM;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.CredentialManagement;

public class CredentialRequestOptions
{
    [JsonPropertyName("mediation")]
    public CredentialMediationRequirement Mediation { get; set; } = CredentialMediationRequirement.Optional;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(IJSWrapperConverter<AbortSignal<Event>>))]
    [JsonPropertyName("signal")]
    public AbortSignal<Event>? Signal { get; set; }
}
