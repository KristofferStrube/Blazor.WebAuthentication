using KristofferStrube.Blazor.CredentialManagement.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.CredentialManagement;

[JsonConverter(typeof(CredentialMediationRequirementConverter))]
public enum CredentialMediationRequirement
{
    Silent,
    Optional,
    Required
}
