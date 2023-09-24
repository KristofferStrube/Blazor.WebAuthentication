using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication.Converters;

public class UserVerificationRequirementConverter : JsonConverter<UserVerificationRequirement>
{
    public override UserVerificationRequirement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, UserVerificationRequirement value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            UserVerificationRequirement.Required => "required",
            UserVerificationRequirement.Preferred => "preferred",
            UserVerificationRequirement.Discouraged => "discouraged",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(UserVerificationRequirement)}.")
        });
    }
}
