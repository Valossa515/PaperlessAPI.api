using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaperlessAPI.api.Shared.Helpers
{
    public class TrimmingJsonConverterHelper : JsonConverter<string>
    {
        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        {
            if (value == null) return;

            writer.WriteStringValue(value.Trim());
        }

        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
