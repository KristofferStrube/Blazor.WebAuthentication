using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication.Converters;

public class PublicKeyCredentialTypeConverter : JsonConverter<PublicKeyCredentialType>
{
    public override PublicKeyCredentialType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, PublicKeyCredentialType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            PublicKeyCredentialType.PublicKey => "public-key",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(PublicKeyCredentialType)}.")
        });
    }
}
