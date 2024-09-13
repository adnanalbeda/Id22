using System.Text.Json;
using System.Text.Json.Serialization;

namespace System;

[JsonConverter(typeof(Id22Converters.JsonConverter))]
public partial record struct Id22;

public partial class Id22Converters
{
    public sealed class JsonConverter : JsonConverter<Id22>
    {
        public override Id22 Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType is JsonTokenType.Null)
                return default;

            var value = JsonSerializer.Deserialize<string>(ref reader, options);
            return Id22.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, Id22 value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.ToString(), options);
        }
    }

    public sealed class NullableJsonConverter : JsonConverter<Id22?>
    {
        public override Id22? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType is JsonTokenType.Null)
                return null;

            var value = JsonSerializer.Deserialize<string>(ref reader, options);
            return Id22.Parse(value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Id22? value,
            JsonSerializerOptions options
        )
        {
            if (value is null)
                writer.WriteNullValue();
            else
                JsonSerializer.Serialize(writer, value.Value.ToString(), options);
        }
    }
}
