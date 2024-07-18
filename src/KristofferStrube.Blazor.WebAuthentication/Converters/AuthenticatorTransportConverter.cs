using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication.Converters;

public class AuthenticatorTransportConverter : JsonConverter<AuthenticatorTransport>
{
    public override AuthenticatorTransport Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "usb" => AuthenticatorTransport.Usb,
            "nfc" => AuthenticatorTransport.Nfc,
            "ble" => AuthenticatorTransport.Ble,
            "internal" => AuthenticatorTransport.Internal,
            var value => throw new ArgumentException($"Value '{value}' was not a valid {nameof(PublicKeyCredentialType)}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, AuthenticatorTransport value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            AuthenticatorTransport.Usb => "usb",
            AuthenticatorTransport.Nfc => "nfc",
            AuthenticatorTransport.Ble => "ble",
            AuthenticatorTransport.Internal => "internal",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(PublicKeyCredentialType)}.")
        });
    }
}
