using KristofferStrube.Blazor.WebAuthentication.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

[JsonConverter(typeof(UserVerificationRequirementConverter))]
public enum UserVerificationRequirement
{
    Required,
    Preferred,
    Discouraged,
}
